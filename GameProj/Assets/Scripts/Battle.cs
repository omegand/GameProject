using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public enum BattleState { START, PTURN, ETURN, WON, LOST }
public class Battle : MonoBehaviour
{
    private GameObject player;
    private Transform playerStation;
    private Transform enemyStation;
    private Stats enemyS;
    private Stats playerS;
    private GameObject canvas;
    private TextMeshProUGUI ScreenText;
    private TrackSwitcher tracks;
    private ParticleSystem DPart;
    private BattleState state;
    private Object[] prefabenemies;
    private List<GameObject> loadedenemies = new List<GameObject>();
    private Random rand;
    private bool waitingforclick = false;
    public int enemyCount;

    private Animator playeranim;
    private void Awake()
    {
        enemyCount = PassingValues.enemycount;
        rand = new Random();
        enemyStation = GameObject.Find("EnemyStation").GetComponent<Transform>();
        prefabenemies = Resources.LoadAll("Enemies", typeof(GameObject));
        Vector3 pos = enemyStation.position;
        pos.x -= enemyCount;
        for (int i = 0; i < enemyCount; i++)
        {
            loadedenemies.Add((GameObject)Instantiate(prefabenemies[rand.Next(0, prefabenemies.Length)], pos, Quaternion.identity));
            pos.x += enemyCount;
        }

    }
    void Start()
    {
        state = BattleState.START;

        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Movement>().SetIdle();
        playeranim = player.GetComponent<Animator>();
        tracks = GameObject.Find("Camera").GetComponent<TrackSwitcher>();


        canvas = GameObject.FindGameObjectWithTag("Actions");
        canvas.SetActive(false);

        playerStation = GameObject.Find("PlayerStation").GetComponent<Transform>();
        tracks.ChangeLookAt(playerStation);
        player.transform.position = playerStation.position;
        player.transform.rotation = Quaternion.identity;
        playerS = player.GetComponent<Stats>();

        DPart = Resources.Load<ParticleSystem>("Particles/Explosion");
        ScreenText = GameObject.FindGameObjectWithTag("Screentext").GetComponent<TextMeshProUGUI>();

        StartCoroutine(FirstTurn());

    }

     IEnumerator FirstTurn()
    {

        if (PassingValues.first)
        {
            ScreenText.text = "You hit the enemy, first turn is yours!";
            yield return new WaitForSeconds(1.5f);
            state = BattleState.PTURN;
            StartCoroutine(PlayerTurn());
        }
        else
        {
            ScreenText.text = "Enemy got the sneak attack on you...";
            yield return new WaitForSeconds(1.5f);
            state = BattleState.ETURN;
            StartCoroutine(EnemyTurn());
        }
    }

    void Update()
    {
        if (waitingforclick && Input.GetMouseButtonDown(0))
        {
            GetMouseInfo();
        }

    }
    IEnumerator PlayerTurn()
    {
        yield return new WaitForSeconds(1f);
        tracks.ChangeLookAt(playerStation);
        ScreenText.text = "Choose an action.";
        canvas.SetActive(true);

    }

    IEnumerator EndBattle()
    {

        if (state == BattleState.WON)
        {
            ScreenText.text = "You won!";
            yield return new WaitForSeconds(1f);
            foreach (var item in SceneManager.GetSceneByBuildIndex(PassingValues.sceneindex).GetRootGameObjects())
            {
                item.SetActive(true);
            }
            player.transform.position = PassingValues.savedpos;
            PassingValues.WonBattle = 1;
            player.GetComponent<Movement>().enabled = true;
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Combat"));

        }
        if (state == BattleState.LOST)
        {
            Instantiate(DPart, playerStation.position, Quaternion.identity);
            ScreenText.text = "You lost.";
            yield return new WaitForSeconds(1f);
        }

    }
    IEnumerator EnemyTurn()
    {
        foreach (var item in loadedenemies)
        {
            enemyS = item.GetComponent<Stats>();
            double damage = DamageModifier(enemyS.dmg);
            ScreenText.text = $"Enemy attacks...";
            tracks.ChangeLookAt(enemyStation);
            yield return new WaitForSeconds(2f);
            if (playerS.defending)
            {
                playerS.defending = false;
                damage = damage / rand.Next(5, 10);
            }
            ScreenText.text = $"Took {damage:0.0} damage.";
            bool dead = playerS.Damage((float)damage);
            if (dead)
            {
                state = BattleState.LOST;
                StartCoroutine(EndBattle());
                yield break;
            }
            yield return new WaitForSeconds(1f);
        }
        state = BattleState.PTURN;
        StartCoroutine(PlayerTurn());
    }

    public void AttackButton()
    {
        canvas.SetActive(false);
        ScreenText.text = $"Choose the enemy to attack";
        waitingforclick = true;
    }
    public void DefendButton()
    {
        canvas.SetActive(false);
        StartCoroutine(Defend(playerS));
    }
    IEnumerator AttackIE(GameObject enemy)
    {
        double damage = DamageModifier(playerS.dmg);
        ScreenText.text = $"Attacking for {damage:0.0} damage";
        playeranim.Play("Attack");
        yield return new WaitForSeconds(0.8f);
        enemyS = enemy.GetComponent<Stats>();
        bool dead = enemyS.Damage((float)damage);
        if (dead)
        {
            enemyCount -= 1;
            loadedenemies.Remove(enemy);
            Instantiate(DPart, enemy.transform.position, Quaternion.identity);
            Destroy(enemy);
            ScreenText.text = $"Enemy is dead...";
            yield return new WaitForSeconds(1f);
            if (enemyCount == 0)
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
            }
            else
            {
                state = BattleState.ETURN;
                StartCoroutine(EnemyTurn());
            }

        }
        else
        {
            state = BattleState.ETURN;
            StartCoroutine(EnemyTurn());
        }

    }
    IEnumerator Defend(Stats stats)
    {
        stats.defending = true;
        ScreenText.text = $"{stats.objectname} is blocking...";
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemyTurn());
    }


    private double DamageModifier(int basedmg)
    {
        int increase = rand.Next(0, 2);
        int chance = rand.Next(1, 25);
        double damage;
        if (increase == 1) damage = basedmg * (1 + (double)chance / 100);
        else damage = basedmg * (1 - (double)chance / 100);
        return damage;
    }
    public LayerMask layers;
    public void GetMouseInfo()
    {
        RaycastHit hit;
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f, layers))
        {
            waitingforclick = false;
            StartCoroutine(AttackIE(hit.transform.gameObject));
        }

    }
}

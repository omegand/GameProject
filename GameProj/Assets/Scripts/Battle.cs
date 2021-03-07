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
    GameObject selectedEnemy;
    private GameObject player;
    private Vector3 savedPos;
    private Transform playerStation;
    private Transform enemyStation;
    private Stats enemyS;
    private Stats playerS;
    private GameObject canvas;
    private TextMeshProUGUI ScreenText;
    private TrackSwitcher tracks;
    private ParticleSystem DPart;
    private BattleState state;
    private Object[] enemies;
    bool waitingforclick = false;
    public int enemyCount;
    Random rand;
    private void Awake()
    {
        rand = new Random();
        enemyStation = GameObject.Find("EnemyStation").GetComponent<Transform>();
        enemies = Resources.LoadAll("Enemies", typeof(GameObject));
        Vector3 pos = enemyStation.position;
        pos.x -= enemyCount;
        for (int i = 0; i < enemyCount; i++)
        {

            Instantiate(enemies[rand.Next(0, enemies.Length)], pos, Quaternion.identity);
            pos.x += enemyCount;
        }
    }
    void Start()
    {
        startingAct();

    }
    void startingAct()
    {
        state = BattleState.START;

        player = GameObject.FindGameObjectWithTag("Player");
        savedPos = player.transform.position;
        player.GetComponent<Movement>().SetIdle();
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
        ScreenText.text = "The battle has started.";
        state = BattleState.PTURN;
        StartCoroutine(PlayerTurn());
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
        yield return new WaitForSeconds(2f);
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
            foreach (var item in SceneManager.GetSceneByBuildIndex(0).GetRootGameObjects())
            {
                item.SetActive(true);
            }
            player.transform.position = savedPos;
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
        }
        else
        {
            state = BattleState.PTURN;
            StartCoroutine(PlayerTurn());
        }
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
        yield return new WaitForSeconds(2f);
        enemyS = enemy.GetComponent<Stats>();
        bool dead = enemyS.Damage((float)damage);
        if (dead)
        {
            enemyCount -= 1;
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
            StartCoroutine(AttackIE(hit.transform.gameObject));
            print($"o kurva {hit.transform.gameObject.name}");
        }

    }
}

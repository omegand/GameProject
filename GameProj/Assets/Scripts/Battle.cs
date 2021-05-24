using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool waitingforclick = false;
    public int enemyCount;
    private Animator playeranim;
    private void Awake()
    {
        enemyCount = PassingValues.enemycount;
        enemyStation = GameObject.Find("EnemyStation").GetComponent<Transform>();
        prefabenemies = Resources.LoadAll("Enemies", typeof(GameObject));
        Vector3 pos = enemyStation.position;
        int seperation = 4;
        pos.x -= seperation * enemyCount / 2;
        for (int i = 0; i < enemyCount; i++)
        {
            var enem = (GameObject)prefabenemies[Random.Range(0, prefabenemies.Length)];
            loadedenemies.Add(Instantiate(enem, pos, enem.transform.rotation));
            pos.x += seperation;
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
        playerS = player.GetComponent<Stats>();

        DPart = Resources.Load<ParticleSystem>("Particles/Explosion");
        ScreenText = GameObject.FindGameObjectWithTag("Screentext").GetComponent<TextMeshProUGUI>();

        StartCoroutine(FirstTurn());
        AudioM.PlaySound(Resources.Load<AudioClip>("Sounds/mindwarp"), true);
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
        Destroy(GameObject.Find("Directional Light"));
        yield return new WaitForSeconds(0.1f);
        if (state == BattleState.WON)
        {
            AudioM.NewMethod();
            ScreenText.text = "You won!";
            yield return new WaitForSeconds(0.7f);
            ScreenText.text = $"You gain {PassingValues.xp} experience.";
            yield return new WaitForSeconds(0.7f);
            foreach (var item in SceneManager.GetSceneByBuildIndex(PassingValues.sceneindex).GetRootGameObjects())
            {
                item.SetActive(true);
            }
            player.transform.position = PassingValues.savedpos;
            PassingValues.WonBattle = 1;
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Combat"));
            playerS.GainXp(PassingValues.xp);
            PassingValues.xp = 0;
            player.GetComponent<Movement>().enabled = true;

        }
        if (state == BattleState.LOST)
        {
            Instantiate(DPart, playerStation.position, Quaternion.identity);
            AudioM.PlaySound(Resources.Load<AudioClip>("Sounds/oof"), false);
            Death.Dead();
            GameObject.Find("Model").SetActive(false);
            yield return new WaitForSeconds(1f);
        }

    }
    IEnumerator EnemyTurn()
    {
        foreach (var item in loadedenemies)
        {
            tracks.ChangeLookAt(enemyStation);
            enemyS = item.GetComponent<Stats>();
            if (!enemyS.stunned)
            {
                double damage = DamageModifier(enemyS.dmg);
                ScreenText.text = $"Enemy attacks...";
                var anim = item.GetComponent<Animator>();
                anim.SetTrigger("attack");
                yield return new WaitForSeconds(2f);
                if (playerS.defending)
                {
                    playerS.defending = false;
                    damage = damage / Random.Range(5, 10);
                }
                AudioM.PlaySound(Resources.Load<AudioClip>("Sounds/punch"), false);
                ScreenText.text = $"Took {damage:0.} damage.";
                bool dead = playerS.Damage((float)damage);
                if (dead)
                {
                    state = BattleState.LOST;
                    StartCoroutine(EndBattle());
                    yield break;
                }
                yield return new WaitForSeconds(0.3f);
            }
            else {
                ScreenText.text = $"Stunned...";
                yield return new WaitForSeconds(0.3f);
                enemyS.stunned = false;
            }

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
        float damage = DamageModifier(playerS.dmg);
        ScreenText.text = $"Attacking for {damage:0.} damage";
        playeranim.Play("Attack");
        AudioM.PlaySound(Resources.Load<AudioClip>("Sounds/swordhit"), false);
        yield return new WaitForSeconds(0.8f);
        enemyS = enemy.GetComponent<Stats>();
        bool dead = enemyS.Damage((float)damage);
        if (dead)
        {
            enemyCount -= 1;
            loadedenemies.Remove(enemy);
            Instantiate(DPart, enemy.transform.position, Quaternion.identity);
            AudioM.PlaySound(Resources.Load<AudioClip>("Sounds/oof"), false);

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
    public void Skill1Button()
    {
        canvas.SetActive(false);
        ScreenText.text = $"Casting Skill...";
        StartCoroutine(Implosion());

    }
    public void Skill2Button()
    {
        canvas.SetActive(false);
        ScreenText.text = $"Casting Skill...";
        StartCoroutine(Meteor());

    }
    public void Skill3Button()
    {
        canvas.SetActive(false);
        ScreenText.text = $"Casting Skill...";
        StartCoroutine(Debil());
    }
    public void Skill4Button()
    {
        canvas.SetActive(false);
        ScreenText.text = $"Casting Skill...";
        StartCoroutine(Thunder());
    }
    IEnumerator Defend(Stats stats)
    {
        stats.defending = true;
        ScreenText.text = $"{stats.objectname} is blocking...";
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemyTurn());
    }
    IEnumerator Meteor()
    {
        var particles = Resources.Load<ParticleSystem>("Particles/Meteorite");

        for (int i = 0; i < 10; i++)
        {
            Vector3 rand = Random.insideUnitSphere * 10;
            Instantiate(particles, rand, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1f);
        int count = loadedenemies.Count;
        int index = 0;
        for (int i = 0; i < count; i++)
        {
            var enemy = loadedenemies[index];
            float damage = DamageModifier(playerS.dmg * 4f);
            enemyS = enemy.GetComponent<Stats>();
            bool dead = enemyS.Damage((float)damage);
            if (dead)
            {
                enemyCount -= 1;
                Destroy(enemy);
                loadedenemies.RemoveAt(index);
                Instantiate(DPart, enemy.transform.position, Quaternion.identity);
                AudioM.PlaySound(Resources.Load<AudioClip>("Sounds/oof"), false);
            }
            else
            {
                ScreenText.text = $"Enemy took {damage:0.} damage and survived...";
                index++;
            }
            yield return new WaitForSeconds(0.5f);
        }
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
    IEnumerator Implosion()
    {
        Vector3 pos = enemyStation.position;
        pos.x -= 10;
        var particles = Resources.Load<ParticleSystem>("Particles/Energy");
        for (int i = 0; i < 100; i++)
        {
            Instantiate(particles, pos, Quaternion.identity);
            yield return new WaitForSeconds(0.02f);
            pos.x += 0.2f;
        }
        int count = loadedenemies.Count;
        int index = 0;
        for (int i = 0; i < count; i++)
        {
            var enemy = loadedenemies[index];
            float damage = DamageModifier(playerS.dmg * 4f);
            enemyS = enemy.GetComponent<Stats>();
            bool dead = enemyS.Damage((float)damage);
            if (dead)
            {
                enemyCount -= 1;
                Destroy(enemy);
                loadedenemies.RemoveAt(index);
                Instantiate(DPart, enemy.transform.position, Quaternion.identity);
                AudioM.PlaySound(Resources.Load<AudioClip>("Sounds/oof"), false);
            }
            else
            {
                ScreenText.text = $"Enemy took {damage:0.} damage and survived...";
                index++;
            }
            yield return new WaitForSeconds(0.5f);
        }
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
    IEnumerator Debil()
    {
        Vector3 pos = enemyStation.position;
        var particles = Resources.Load<ParticleSystem>("Particles/debil");
        Instantiate(particles, pos, Quaternion.identity);
        foreach (var item in loadedenemies)
        {
            enemyS = item.GetComponent<Stats>();
            enemyS.dmg = enemyS.dmg / 10;
        }
        ScreenText.text = $"Enemy damage drastically reduced.";
        yield return new WaitForSeconds(1.3f);
        state = BattleState.ETURN;
        StartCoroutine(EnemyTurn());

    }
    IEnumerator Thunder()
    {
        Vector3 pos = enemyStation.position;
        pos.y += 15;
        var particles = Resources.Load<ParticleSystem>("Particles/thunder");
        Instantiate(particles, pos, particles.transform.rotation);
        foreach (var item in loadedenemies)
        {
            enemyS = item.GetComponent<Stats>();
            enemyS.stunned = true;
        }
        ScreenText.text = $"Enemy has been stunned for one turn.";
        yield return new WaitForSeconds(1.3f);
        state = BattleState.ETURN;
        StartCoroutine(EnemyTurn());

    }


    private float DamageModifier(float basedmg)
    {
        int increase = Random.Range(1, 3);
        int chance = Random.Range(1, 25);
        float damage;
        if (increase == 1) damage = basedmg * (1 + (float)chance / 100);
        else damage = basedmg * (1 - (float)chance / 100);
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

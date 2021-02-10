using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, PTURN, ETURN, WON, LOST }
public class Battle : MonoBehaviour
{

    private GameObject player;
    private Vector3 savedPos;
    private GameObject enemy;
    private Transform playerT;
    private Transform enemyT;
    private Stats enemyS;
    private Stats playerS;
    private GameObject canvas;
    private TextMeshProUGUI enemyHPText;
    private TextMeshProUGUI NameText;
    private TextMeshProUGUI EnemyLVLText;
    // public TextMeshProUGUI LVLTextP;
    private TextMeshProUGUI ScreenText;
    private TrackSwitcher tracks;
    private ParticleSystem DPart;
    private Slider enemyHPSlider;
    public BattleState state;
    bool started = false;

    void Start()
    {
        startingAct();
    }
    void startingAct()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        savedPos = player.transform.position;
        player.GetComponent<CharacterController>().enabled = false; 
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        //camera and canvas
        tracks = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TrackSwitcher>();
        state = BattleState.START;
        canvas = GameObject.FindGameObjectWithTag("Actions");
        canvas.SetActive(false);
        //Player/Enemy
        playerT = GameObject.Find("PlayerStation").GetComponent<Transform>();
        enemyT = GameObject.Find("EnemyStation").GetComponent<Transform>();
        //UI
        enemyHPSlider = GameObject.Find("EnemyHpSlider").GetComponent<Slider>();
        enemyHPText = GameObject.Find("EnemyHpText").GetComponent<TextMeshProUGUI>();
        NameText = GameObject.Find("EnemyNameText").GetComponent<TextMeshProUGUI>();
        EnemyLVLText = GameObject.Find("EnemyLVLText").GetComponent<TextMeshProUGUI>();
        //positions
        tracks.ChangeLookAt(playerT);
        
        player.transform.position = playerT.position;
        enemy.transform.position = enemyT.position;
        enemyS = enemy.GetComponent<Stats>();
        playerS = player.GetComponent<Stats>();
        NameText.text = enemyS.Namee;
        EnemyLVLText.text = "LVL - " + enemyS.level.ToString();
        // LVLTextP.text = playerS.level.ToString();
        DPart = Resources.Load<ParticleSystem>("Particles/Explosion");
        ScreenText = GameObject.FindGameObjectWithTag("Screentext").GetComponent<TextMeshProUGUI>();
        ScreenText.text = "The battle has started.";
        state = BattleState.PTURN;
        started = true;
        StartCoroutine(PlayerTurn());
    }
    void Update()
    {
        if (started)
        {
            player.transform.position = playerT.position;
            enemyHPText.text = enemyS.currenthp + " / " + enemyS.maxhp;
            enemyHPSlider.value = enemyS.currenthp / enemyS.maxhp;
        }

    }
    IEnumerator PlayerTurn()
    {
        yield return new WaitForSeconds(2f);
        tracks.ChangeLookAt(playerT);
        ScreenText.text = "Choose an action.";
        canvas.SetActive(true);

    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            ScreenText.text = "You won!";
            Instantiate(DPart, enemyT.position, Quaternion.identity);
            Destroy(enemy);
            foreach (var item in SceneManager.GetSceneByBuildIndex(0).GetRootGameObjects())
            {
                item.SetActive(true);
            }
            started = false;
            player.transform.position = savedPos;
            player.GetComponent<CharacterController>().enabled = true ;


            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Combat"));

        }
        if (state == BattleState.LOST)
        {
            Instantiate(DPart, playerT.position, Quaternion.identity);
            ScreenText.text = "You lost.";
        }

    }
    IEnumerator EnemyTurn()
    {
        ScreenText.text = "Enemy attacks...";
        tracks.ChangeLookAt(enemyT);
        yield return new WaitForSeconds(2f);
        if (playerS.defending) { playerS.defending = false; StartCoroutine(PlayerTurn()); }
        else
        {
            bool dead = playerS.Damage(enemyS.dmg);
            if (dead)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else
            {
                state = BattleState.PTURN;
                StartCoroutine(PlayerTurn());
            }
        }

    }

    public void AttackButton()
    {
        canvas.SetActive(false);
        StartCoroutine(AttackIE());
    }
    public void DefendButton()
    {
        canvas.SetActive(false);
        StartCoroutine(Defend(playerS));
    }
    IEnumerator AttackIE()
    {
        yield return new WaitForSeconds(2f);
        bool dead = enemyS.Damage(playerS.dmg);
        if (dead)
        {
            state = BattleState.WON;
            EndBattle();
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
        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemyTurn());
    }


}

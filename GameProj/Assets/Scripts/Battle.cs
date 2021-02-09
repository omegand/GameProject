using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PTURN, ETURN, WON, LOST }
public class Battle : MonoBehaviour
{

    private GameObject player;
    private GameObject enemy;
    private Transform playerT;
    private Transform enemyT;
    private Stats enemyS;
    private Stats playerS;
    GameObject canvas;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI LVLText;
     TextMeshProUGUI ScreenText;

    private ParticleSystem DPart;

    public Slider enemyHP;
    public BattleState state;
    void Start()
    {
        startingAct();

    }
    void startingAct()
    {
        state = BattleState.START;
        canvas = GameObject.FindGameObjectWithTag("Actions");

        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        playerT = GameObject.FindGameObjectWithTag("StationP").GetComponent<Transform>();
        enemyT = GameObject.FindGameObjectWithTag("StationE").GetComponent<Transform>();

        player.transform.position = playerT.position;
        enemy.transform.position = enemyT.position;

        enemyS = enemy.GetComponent<Stats>();
        playerS = player.GetComponent<Stats>();

        NameText.text = enemyS.Namee;
        LVLText.text = "LVL - " + enemyS.level.ToString();
        DPart = Resources.Load<ParticleSystem>("Particles/Explosion");
        ScreenText = GameObject.FindGameObjectWithTag("Screentext").GetComponent<TextMeshProUGUI>();
        ScreenText.text = "The battle has started.";
        state = BattleState.PTURN;
        canvas.SetActive(false);
        StartCoroutine(PlayerTurn());
    }
    void Update()
    {
        HPText.text = enemyS.currenthp + " / " + enemyS.maxhp;
        enemyHP.value = enemyS.currenthp / enemyS.maxhp;
    }
    IEnumerator PlayerTurn()
    {
        yield return new WaitForSeconds(2f);
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
        }
        if (state == BattleState.LOST) 
        {
            Instantiate(DPart, playerT.position, Quaternion.identity);
            ScreenText.text = "You lost.";
        }
    }
    void EnemyTurn() {
        ScreenText.text = "Enemy attacks for "+enemyS.dmg+" damage";

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

    public void AttackButton()
    {
        canvas.SetActive(false);
        bool dead = enemyS.Damage(playerS.dmg);
        if (dead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ETURN;
            EnemyTurn();
        }
    }


}

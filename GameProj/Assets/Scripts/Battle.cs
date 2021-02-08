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
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI LVLText;

    public Slider enemyHP;
    public BattleState state;
    void Start()
    {
        startingAct();

    }
    void startingAct()
    {
        state = BattleState.START;
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        playerT = GameObject.FindGameObjectWithTag("StationP").GetComponent<Transform>();
        enemyT = GameObject.FindGameObjectWithTag("StationE").GetComponent<Transform>();
        player.transform.position = playerT.position;
        enemy.transform.position = enemyT.position;
        enemyS = enemy.GetComponent<Stats>();
        NameText.text = enemyS.Namee;
        LVLText.text = "LVL - " + enemyS.level.ToString();
        state = BattleState.PTURN;
        Player();
    }
    void Update()
    {
        HPText.text = enemyS.currenthp + " / " + enemyS.maxhp;
        enemyHP.value = enemyS.currenthp / enemyS.maxhp;
    }
    void Player()
    {

    }
    public void Attack()
    {
    }


}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PTURN, ETURN, WON, LOST }
public class Battle : MonoBehaviour
{

    GameObject player;
    GameObject enemy;
    Transform playerT;
    Transform enemyT;
    Stats enemyS;
    Stats playerS;
    public TextMeshProUGUI TextMesh;
    public Slider enemyHP;
    public BattleState state;
    void Start()
    {
        state = BattleState.START;
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        playerT = GameObject.FindGameObjectWithTag("StationP").GetComponent<Transform>();
        enemyT = GameObject.FindGameObjectWithTag("StationE").GetComponent<Transform>();
        player.transform.position = playerT.position;
        enemy.transform.position = enemyT.position;
        enemyS = enemy.GetComponent<Stats>();
    }
    void Update()
    {
        TextMesh.text = enemyS.currenthp + " / " + enemyS.maxhp;
        enemyHP.value = enemyS.currenthp / enemyS.maxhp;
    }


}

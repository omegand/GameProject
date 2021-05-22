using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    private TextMeshProUGUI hpText;
    private Slider hpSlider;
    private TextMeshProUGUI lvlText;
    private Stats stats;
    private TextMeshProUGUI nameText;
    private Transform ok;
    private GameObject particles;


    void Start()
    {
        particles = transform.Find("RingParticles").gameObject;
        particles.SetActive(false);
        //Unity doesn't allow you to traverse tree of children wtf
        ok = transform.Find("EnemyCanvas") ;
        stats = gameObject.GetComponent<Stats>();
        hpSlider = ok.Find("EnemyHpSlider").GetComponent<Slider>();
        hpText =   ok.Find("EnemyHpText").GetComponent<TextMeshProUGUI>();
        lvlText =  ok.Find("EnemyLVLText").GetComponent<TextMeshProUGUI>();
        nameText = ok.Find("EnemyNameText").GetComponent<TextMeshProUGUI>();

        lvlText.text = "LVL - " + stats.level.ToString();
        nameText.text = stats.objectname;
    }
    void Update()
    {
        hpText.text = stats.currenthp + " / " + stats.maxhp;
        hpSlider.value = stats.currenthp / stats.maxhp;
    }
    private void OnMouseEnter()
    {
        particles.SetActive(true);
    }
    private void OnMouseExit()
    {
        particles.SetActive(false);
    }
}

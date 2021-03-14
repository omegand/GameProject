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
    private GameObject POSuNITY;
    private GameObject particles;


    void Start()
    {
        //Unity doesn't allow you to traverse tree of children wtf
        POSuNITY = transform.Find("Canvas").gameObject;
        stats = gameObject.GetComponent<Stats>();
        particles = transform.Find("RingParticles").gameObject;
        particles.SetActive(false);
        hpSlider = POSuNITY.transform.Find("EnemyHpSlider").GetComponent<Slider>();
        hpText =   POSuNITY.transform.Find("EnemyHpText").GetComponent<TextMeshProUGUI>();
        lvlText =  POSuNITY.transform.Find("EnemyLVLText").GetComponent<TextMeshProUGUI>();
        nameText = POSuNITY.transform.Find("EnemyNameText").GetComponent<TextMeshProUGUI>();

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

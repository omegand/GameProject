using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deal_Damage : MonoBehaviour
{
    [SerializeField]
    private float Damage_Deal;
    [SerializeField]
    private float NextDamage;

    private bool GotDamage;
    // Start is called before the first frame update
    void Start()
    {
        GotDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator EnableDamage()
    {
        yield return new WaitForSeconds(NextDamage);
        GotDamage = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag.CompareTo("Player") == 0 && !GotDamage)
        {
            Stats stats = other.GetComponent<Stats>();
            stats.currenthp -= Damage_Deal;
            GotDamage = true;

            if(stats.currenthp <= 0)
            Death.Dead();
            else
            {
                StartCoroutine(EnableDamage());
            }

        }
    }

}

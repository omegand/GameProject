using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Destroyed : MonoBehaviour
{
    GameObject HealthPickup;
    ParticleSystem boxExplode;
    Vector3 boxCenter;
    void Start()
    {
        HealthPickup = Resources.Load<GameObject>("Prefabs/Bottle_Health");
        boxExplode = Resources.Load<ParticleSystem>("Particles/BoxDestroy");
        boxCenter = GetComponent<BoxCollider>().center;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Sword"))
        {
            Vector3 newPosition = new Vector3();
            newPosition.x = Mathf.Clamp(transform.position.x, transform.position.x, transform.position.x + boxCenter.x);
            newPosition.y = transform.position.y;
            newPosition.z = Mathf.Clamp(transform.position.z, transform.position.z, transform.position.z + boxCenter.z);
            Instantiate(HealthPickup, newPosition, transform.rotation);
            boxExplode.transform.position = newPosition;
            boxExplode.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);
        }
    }
}

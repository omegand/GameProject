using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Destroyed : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject HealthPickup;
    public GameObject boxExplode;

    private Vector3 boxCenter;
    void Start()
    {
        boxCenter = GetComponent<BoxCollider>().center;
        boxExplode = Instantiate(GameObject.Find("BoxDestroy_Particle"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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

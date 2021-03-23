using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OnHit : MonoBehaviour
{
    Vector3 startingpos;
    Rigidbody rb;
    Animator anim;
    Transform parent;
    GameObject player;

    private void Start()
    {
        parent = transform.parent;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = transform.parent.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        startingpos = transform.position;
    }
    Vector3 generateVector()
    {
        int r1 = Random.Range(5, 15);
        int r2 = Random.Range(5, 15);
        int r3 = Random.Range(5, 15);

        Vector3 destination = new Vector3(
            startingpos.x + r1,
            startingpos.y + r2,
            startingpos.z + r3);
        return destination;
    }
    bool seen;
    private void LateUpdate()
    {
        seen = Physics.CheckSphere(transform.position, 10f, LayerMask.GetMask("Player"));
        if (seen)
        {
            transform.parent.position = Vector3.Lerp(parent.position, player.transform.position, 0.1f * Time.deltaTime);
            transform.parent.LookAt(player.transform);
        }
        if (Input.GetMouseButtonDown(0)) GetMouseInfo();

    }
    public void MoveBox()
    {
        Vector3 temp = generateVector();
        rb.isKinematic = false;
        rb.AddForce(temp * Time.deltaTime * 3f, ForceMode.Impulse);
    }
    public void GetMouseInfo()
    {

        RaycastHit hit;
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Hittable")))
        {
            hit.transform.gameObject.GetComponent<OnHit>().MoveBox();
            anim.enabled = false;
        }

    }
}

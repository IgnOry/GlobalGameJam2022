using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakingballç : MonoBehaviour
{
    public GameObject fractured;
    public float breakForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f")) breakingball();
    }


    public void breakingball() {
       GameObject frac =  Instantiate(fractured, transform.position, transform.rotation);

        foreach(Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>()){
            Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
            rb.AddForce(force);
        }
        Destroy(gameObject);
    }
}


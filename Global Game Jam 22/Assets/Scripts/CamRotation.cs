using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    public Transform target;
    public float speedMod = 1.0f;

    void Update()
    {
        //target = GameObject.Find("Pawn(Clone)").transform;
        //transform.RotateAround(target.position, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * speedMod);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<Animation>().isPlaying)
        {
            if (gameObject.name == "Shine") gameObject.GetComponent<Animation>().Play("Shine1");
            if (gameObject.name == "LitShine") gameObject.GetComponent<Animation>().Play("LitShine1");
        }
    }
}

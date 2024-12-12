using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    public float motion;
    // Update is called once per frame
    void Update()
    {
        GetComponent<Animator>().SetFloat("motion", motion);
    }
}

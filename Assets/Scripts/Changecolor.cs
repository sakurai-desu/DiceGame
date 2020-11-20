using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changecolor : MonoBehaviour
{
    //色を変えるためのフラグ
    public bool g_changecolorflag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (g_changecolorflag == true)
        {
            GetComponent<Renderer>().material.color = Color.black;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
    }
}

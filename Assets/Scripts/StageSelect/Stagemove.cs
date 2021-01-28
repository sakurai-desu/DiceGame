using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stagemove : MonoBehaviour
{

    public GameObject[,] g_json_button_move;

    SpwernButton g_spwern_move;

    // Start is called before the first frame update
    void Start()
    {
        g_spwern_move = GameObject.Find("Stageparent").GetComponent<SpwernButton>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

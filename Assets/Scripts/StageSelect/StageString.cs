using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageString : MonoBehaviour
{
    JsonArray g_jsonstring;

    [SerializeField]
    public string[,] g_stage_string;
    // Start is called before the first frame update
    void Start()
    {
        g_jsonstring = GetComponent<JsonArray>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

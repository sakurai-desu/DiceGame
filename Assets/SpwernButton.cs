using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwernButton : MonoBehaviour
{
    [SerializeField]
    GameObject g_button_obj;

    private JsonArray g_array_Script;

    [SerializeField]
    private int g_button_sprewn_num;

    GameObject[,] g_json_button_array;
    // Start is called before the first frame update
    void Start()
    {
        g_array_Script = GameObject.Find("Stageinformation").GetComponent<JsonArray>();

        g_button_sprewn_num = g_array_Script.g_stage_array_num;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

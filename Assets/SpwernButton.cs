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

    int g_var_num;

    int g_side_num;

    int g_remainder_num;
    // Start is called before the first frame update
    void Start()
    {
        g_array_Script = GameObject.Find("Stageinformation").GetComponent<JsonArray>();
        g_side_num = g_array_Script.g_stage_side;
        g_var_num = g_array_Script.g_stage_max_var;
        g_remainder_num = g_array_Script.g_stage_remainder;
        g_button_sprewn_num = g_array_Script.g_stage_array_num;
        g_json_button_array = new GameObject[g_side_num,g_var_num];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ButtonRespean() {

    } 

    void ButtonPool() {
        for (int i = 0; i > g_var_num; i++) {
            for (int j = 0; j > g_side_num; j++) {
                g_json_button_array[i, j] = g_button_obj;
            }
        }
    }
}

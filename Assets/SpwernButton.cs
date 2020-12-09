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

    [SerializeField]
    private GameObject[,] g_json_button_array;

    [SerializeField]
    int g_var_num;

    [SerializeField]
    int g_side_num;

    [SerializeField]
    int g_remainder_num;
    // Start is called before the first frame update
    void Start()
    {
        //ボタンを生むのに必要な情報を取得
        g_array_Script = GameObject.Find("Stageinformation").GetComponent<JsonArray>();
        //生み出す横の幅を決定
        g_side_num = g_array_Script.g_stage_side;
        //生み出す縦の幅を決定
        g_var_num = g_array_Script.g_stage_max_var;
        //生み出すあまりの幅を決定
        g_remainder_num = g_array_Script.g_stage_remainder;
        //生み出す個数
        g_button_sprewn_num = g_array_Script.g_stage_array_num;
        g_json_button_array = new GameObject[g_side_num,g_var_num];
        ButtonPool(g_button_obj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    GameObject g_button;
    void ButtonPool(GameObject g_stage_button) {
        for (int i = 0; i < g_var_num; i++) {
            for (int j = 0; j < g_side_num; j++) {
               g_button= Instantiate(g_stage_button);
                g_button.transform.parent = gameObject.transform;
                g_json_button_array[i, j] = g_button;
            }
        }
    }
}

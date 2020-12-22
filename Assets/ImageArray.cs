using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageArray : MonoBehaviour
{

    //ステージ名を入れていく配列
    public string[,] g_stage_string;

    //ストリーミングアセットから読み出すスクリプト
    Folder_Script g_streaming_folder;

    JsonArray g_json_array_script;

    int g_var_num;
    int g_side_num;


    // Start is called before the first frame update
    void Start()
    {
        g_json_array_script = GameObject.Find("Stageinformation").GetComponent<JsonArray>();
        g_var_num = g_json_array_script.g_stage_max_var;
        g_side_num = g_json_array_script.g_stage_side;
        g_streaming_folder = GameObject.Find("Stageinformation").GetComponent<Folder_Script>();
        g_stage_string = new string[g_var_num,g_side_num];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

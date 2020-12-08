using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonArray : MonoBehaviour
{
    //ステージの名前を入れる配列
    public string[] g_json_stage;

    //フォルダーの数を数えるスクリプト
    Folder_Script g_folder_Script;

    //ステージ数
    int g_stage_array_num;
    // Start is called before the first frame update
    void Start()
    {
        //スクリプトを取得
        g_folder_Script = GetComponent<Folder_Script>();

        //フォルダーを数えるスクリプトの数値を入れる
        g_stage_array_num = g_folder_Script.Filenum();

        g_json_stage = new string[g_stage_array_num];

        g_folder_Script.Filename();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

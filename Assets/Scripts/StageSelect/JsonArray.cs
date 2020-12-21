using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonArray : MonoBehaviour {
    //ステージの名前を入れる配列
    public string[] g_json_stage;

    public string[,] g_json_name;
    //フォルダーの数を数えるスクリプト
    Folder_Script g_folder_Script;

    //ステージの横の数
    public int g_stage_side;

    //ステージ数
    public int g_stage_array_num;

    //ステージの縦の数(最終的な縦の数を入れる)
    public int g_stage_max_var;

    //ステージの縦の数
    public int g_stage_var;

    //ステージのあまりの数
    public int g_stage_remainder;

    // Start is called before the first frame update
    void Start() {
        //スクリプトを取得
        g_folder_Script = GetComponent<Folder_Script>();

        //フォルダーを数えるスクリプトの数値を入れる
        g_stage_array_num = g_folder_Script.Filenum("*.json");

        g_json_stage = new string[g_stage_array_num];

        g_folder_Script.Filename("*.json");


        //ファイル数とstageの横の数を割る
        g_stage_var = g_stage_array_num / g_stage_side;

        //ファイル数とstageの横の数を割った数の余りを求める
        g_stage_remainder = g_stage_array_num % g_stage_side;
        //ファイル数とstageの横の数を割りきれなかったとき
        if (g_stage_remainder != 0) {
            g_stage_max_var = g_stage_var + 1;
        } else { 
            //割った数で縦の数を決める
          g_stage_max_var = g_stage_var;
        }
      
        Debug.Log(g_stage_var);
        Debug.Log(g_stage_remainder);
        Debug.Log(g_stage_max_var);
    }
}

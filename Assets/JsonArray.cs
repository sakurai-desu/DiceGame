using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonArray : MonoBehaviour
{
    //ステージの名前を入れる配列
    public string[] g_json_stage;

    //フォルダーの数を数えるスクリプト
    Folder_Script g_folder_Script;

    //ステージの横の数
    [SerializeField]
    private int g_stage_side;

    //ステージ数
    [SerializeField]
    private int g_stage_array_num;

    //ステージの縦の数(最終的な縦の数を入れる)
    [SerializeField]
    private int g_stage_max_var;

    //ステージの縦の数
    [SerializeField]
    private int g_stage_var;

    //ステージのあまりの数
    [SerializeField]
    private int g_stage_remainder;

    // Start is called before the first frame update
    void Start()
    {
        //スクリプトを取得
        g_folder_Script = GetComponent<Folder_Script>();

        //フォルダーを数えるスクリプトの数値を入れる
        g_stage_array_num = g_folder_Script.Filenum();

        g_json_stage = new string[g_stage_array_num];

        g_folder_Script.Filename();

        Stage_Calc();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// ステージの縦との数を計算する
    /// </summary>
    public void Stage_Calc() {
        //ファイル数とstageの横の数を割る
        g_stage_var = g_stage_array_num / g_stage_side;

        //ファイル数とstageの横の数を割った数の余りを求める
        g_stage_remainder = g_stage_array_num % g_stage_side;
        //割った数と余りを足して縦の数を決める
        g_stage_max_var = g_stage_var + g_stage_remainder;

        Debug.Log(g_stage_var);
        Debug.Log(g_stage_remainder);
        Debug.Log(g_stage_max_var);
    }
}

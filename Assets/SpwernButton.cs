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
    private string[,] g_json_stage_array;

    [SerializeField]
    int g_varmax_num;

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
        g_varmax_num = g_array_Script.g_stage_max_var;
        g_var_num = g_array_Script.g_stage_var;
        //生み出すあまりの幅を決定
        g_remainder_num = g_array_Script.g_stage_remainder;
        //生み出す個数
        g_button_sprewn_num = g_array_Script.g_stage_array_num;
        //配列の最大値を決定する
        g_json_button_array = new GameObject[g_side_num,g_varmax_num];
        g_json_stage_array = new string[g_side_num, g_varmax_num];
        ButtonPool(g_button_obj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    int g_array_num;
    GameObject g_button;
    /// <summary>
    /// オブジェクトの生成とステージ名を配列に入れるメソッド
    /// </summary>
    /// <param name="g_stage_button"></param>
    void ButtonPool(GameObject g_stage_button) {
        int i=0;
        int j = 0;
        //オブジェクトを列が埋まっている分生成する
        for (i=0; i < g_var_num; i++) {
            for (j = 0; j < g_side_num; j++) {
                //オブジェクトが生まれる
               g_button= Instantiate(g_stage_button);
                //親になる
                g_button.transform.parent = gameObject.transform;
                //二次元配列にボタンを入れる
                g_json_button_array[i, j] = g_button;
                //ステージ名を二次元配列に入れる
                g_json_stage_array[i, j] = g_array_Script.g_json_stage[g_array_num];
                Debug.Log(g_json_stage_array[i, j]);
                g_array_num++;
            }
        }
        //余りがあったらやる
        if (g_remainder_num != 0) {
            i =g_varmax_num-1;
            //余りの数だけ入れる
            for (j = 0; j < g_remainder_num; j++) {

                g_button = Instantiate(g_stage_button);
                g_button.transform.parent = gameObject.transform;
                g_json_button_array[i,j] = g_button;
                g_json_stage_array[i, j] = g_array_Script.g_json_stage[g_array_num];
                Debug.Log(g_json_stage_array[i, j]);
                g_array_num++;
            }
        }
    }
}

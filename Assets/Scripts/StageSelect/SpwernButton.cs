using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpwernButton : MonoBehaviour
{
   
    //1ぺージの最大の数(縦)
    [SerializeField]
   public int g_var_size;
    //配置するステージ選択のオブジェクト
    [SerializeField]
    GameObject g_button_obj;

    //json関連の配列を使うスクリプト
    private JsonArray g_array_Script;

    //ボタンを乱したりするのに使う数値
    [SerializeField]
    public int g_button_sprewn_num;

    //ボタンをしまう配列
    public GameObject[,] g_json_button_array;
    
    //ステージ名を格納する配列
    public string[,] g_json_stage_array;

    public int g_var_calc = 1;

    //ボタン縦の最大値
    public int g_varmax_num;

    //ボタン縦の値
    [SerializeField]
   public int g_var_num;

    //ボタン横の値
    public int g_side_num;

    //ボタンあまりの値
    public int g_remainder_num;

    //整列するときの縦の数値
    [SerializeField]
   public int g_side_set_num = 100;

    //整列するときの横の数値
    [SerializeField]
    public int g_var_set_num = 130;

    int g_side_reset;

    //縦の距離を話すために使う数値
    [SerializeField]
   public int g_var_gap_num;

    //何回も繰り返さないようにするためのもの
    bool g_var_flag;
    //ボタンに入ってるテキスト
    TextMeshProUGUI g_button_text;

    Folder_Script g_folder_Script;
    // Start is called before the first frame update
    void Start()
    {
        //ボタンを生むのに必要な情報を取得
        g_array_Script = GameObject.Find("Stageinformation").GetComponent<JsonArray>();
        g_folder_Script = GameObject.Find("Stageinformation").GetComponent<Folder_Script>();
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
        g_json_button_array = new GameObject[g_varmax_num,g_side_num];
        g_json_stage_array = new string[g_varmax_num,g_side_num];
        ButtonPool(g_button_obj);
    }
    //最低でも開ける間隔
    public float g_button_x_pos=180;

    public float g_button_y_pos=20;

    int g_array_num;
    GameObject g_button;

       public int g_var_count=0;
    public int g_side_count = 0;
    /// <summary>
    /// オブジェクトの生成とステージ名を配列に入れるメソッド
    /// </summary>
    /// <param name="g_stage_button"></param>
    void ButtonPool(GameObject g_stage_button) {

        if (g_remainder_num == 0) {
            Button(g_stage_button, g_varmax_num);
        }
        //余りがあったらやる
        if (g_remainder_num != 0) {

            Button(g_stage_button, g_var_num);
            g_var_count =g_varmax_num-1;
            //余りの数だけ入れる
            for (g_side_count = 0; g_side_count < g_remainder_num; g_side_count++) {
                ButtonSpwen(g_stage_button, g_var_count, g_side_count, g_array_num);
                g_array_num++;
            }
        }
    }
    private void Button(GameObject g_stage_button,int g_var) {
        //オブジェクトを列が埋まっている分生成する
        for (g_var_count = 0; g_var_count <= g_var_num-1; g_var_count++) {
            for (g_side_count = 0; g_side_count <= g_side_num - 1; g_side_count++) {
                ButtonSpwen(g_stage_button, g_var_count, g_side_count,g_array_num);
                g_array_num++;
            }
        }
    }
    //ページ数
   public int g_page_pointer = 1;
    int g_page_gap_num;
    /// <summary>
    /// ボタンを生んだりするメソッド
    /// </summary>
    /// <param name="buttonObj">生まれるボタン</param>
    /// <param name="i">varの数値</param>
    /// <param name="j">sideの数値</param>
    /// <param name="stage_str">ボタンに表示する数値</param>
    void ButtonSpwen(GameObject buttonObj,int i,int j,int stage_str) {
        
        stage_str += 1;
        //オブジェクトが生まれる
        g_button = Instantiate(buttonObj);
        g_button_text = g_button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        g_button_text.text = stage_str.ToString();
        //親になる
        g_button.transform.parent = gameObject.transform;
        RectTransform g_button_transform = g_button.GetComponent<RectTransform>();
        if (i < g_var_size) {
            g_button_transform.localPosition = new Vector3(g_button_y_pos + j * g_side_set_num, g_button_x_pos + i * -g_var_set_num, 0);

        } else if (i % g_var_size == 0) {

            if (g_var_flag == false) {
                g_page_pointer++;
                g_side_reset = g_var_set_num;
                g_var_gap_num +=50;
                Debug.Log(g_page_pointer);
                g_var_flag = true;
            }
        } else {
            g_var_flag = false;
        }
        g_button_transform.localPosition = new Vector3( g_button_y_pos +j* g_side_set_num, g_button_x_pos + i*-g_var_set_num - g_var_gap_num, 0);

        //二次元配列にボタンを入れる
        g_json_button_array[i, j] = g_button;
        
        //ステージ名を二次元配列に入れる
        g_json_stage_array[i, j] = g_array_Script.g_json_stage[g_array_num];

    }
}

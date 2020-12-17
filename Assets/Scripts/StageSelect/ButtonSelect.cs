using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelect : MonoBehaviour
{
    //ボタンの配列をつかさどる配列
    SpwernButton g_spwern_script;
    JsonArray g_json_script;
    //配列を検索するポインター
    [SerializeField]
    int g_var_pointer;
    //最後のページの縦の数
    [SerializeField]
    private int g_var_remainder;
    [SerializeField]
    int g_side_pointer;
   
    [SerializeField]
    private int g_var_minicheck_pointer;

    //json関連の配列を使うスクリプト
    private JsonArray g_array_Script;

    //生成するステージ数を取得
    private int g_max_stageelement;
    //1ページの縦の数を取得する変数
    [SerializeField]
    private int g_page_var_count;
    //1ページのステージの数を取得する変数
    [SerializeField]
    private int g_stage_page_count;
    //ステージの横の数を取得
    public int g_side_stage_count;
    //最後のページ中のステージが何個あるか
    [SerializeField]
    private int g_stage_remainder;
    //移動先が画面外にでるかの比較用
    [SerializeField]
    private int g_page_turnover = 1;
    //ページをめくる数
    [SerializeField]
    private float g_move_g_var;

    //縦の幅を取得
    private int g_var_siza;
    ButtonSizeChange g_buttonSize;

    GetStagename g_stagename;

    private float g_controller_time = 0.5f;

    private float g_limit_num = 0.49f;
    void Start()
    {
        //ボタンを生むのに必要な情報を取得
        g_array_Script = GameObject.Find("Stageinformation").GetComponent<JsonArray>();

        g_json_script = GameObject.Find("Stageinformation").GetComponent<JsonArray>();
        g_spwern_script = GetComponent<SpwernButton>();
        //ボタンの幅を取得
        g_move_g_var = g_spwern_script.g_button_y_pos;
       
        //横の数を取得
        g_side_stage_count = g_json_script.g_stage_side;
      
        g_page_var_count = g_spwern_script.g_var_size + 1　;
       
        //生成するステージの数を取得
        g_max_stageelement = g_json_script.g_stage_array_num;
        //1ページのステージの数を計算
        g_stage_page_count = g_side_stage_count * (g_page_var_count -1);
      
        if (g_max_stageelement % g_stage_page_count == 0) {
            Debug.Log("余り0");
            g_var_remainder = g_page_var_count -1;
        } else {
            //最後のページのステージの個数
            g_stage_remainder = g_max_stageelement % g_stage_page_count;

            g_stage_remainder = g_stage_remainder / g_side_stage_count;

            if (g_stage_remainder == 0) {
                g_var_remainder = g_stage_remainder;
            } else {
                g_var_remainder = g_stage_remainder + 1;
                Debug.Log(g_var_remainder);

            }
        }
      
    }
    bool g_oneflag;
    bool g_stick_flag;

    void Update()
    {
        if (g_oneflag == false) {

            ButtonBig();
            g_oneflag = true;
        }
        #region キーが押されたときの処理
        //Aキーが押されたとき
        if (Input.GetKeyDown(KeyCode.A)||(Input.GetAxisRaw("Horizontal") <-g_controller_time && g_stick_flag==false)) {
            ButtonOrigin();
            //２段目以降の一番左の時
            if (g_side_pointer == 0 && g_var_pointer != 0) {
                g_var_pointer -= 1;
                g_side_pointer = g_spwern_script.g_side_num - 1;
                g_page_turnover -= 1;

                if (g_page_turnover == 0) {
                    this.transform.localPosition -= new Vector3(0, g_move_g_var * 20, 0);
                    g_page_turnover = g_page_var_count;
                    g_page_turnover -= 1;

                }
            }
            //一番上の一番左の時
            else if (g_side_pointer == 0 && g_var_pointer == 0) {
                g_var_pointer = g_spwern_script.g_varmax_num - 1;
                g_side_pointer = g_spwern_script.g_side_num-g_spwern_script.g_remainder_num-1;
                //g_page_turnover = g_var_remainder;
                //最後のページの縦の数を計算して取得
            } else {
            g_side_pointer -= 1;
            }
            ButtonBig();
            g_stick_flag = true;
            //Debug.Log(g_spwern_script.g_json_stage_array[g_var_pointer, g_side_pointer]);
        }
        if (Input.GetKeyDown(KeyCode.D) || (Input.GetAxisRaw("Horizontal") > g_controller_time && g_stick_flag == false)) {
            ButtonOrigin();
            //一番下以外の一番右の時
            if (g_side_pointer == g_spwern_script.g_side_num - 1 && g_var_pointer != g_spwern_script.g_varmax_num) {
                g_side_pointer = 0;
                g_var_pointer += 1;
                g_page_turnover += 1;
                if (g_page_var_count == g_page_turnover) {
                    this.transform.localPosition += new Vector3(0, g_move_g_var * 20, 0);
                    g_page_turnover = 1;
                    ButtonBig();
                }
            }
            //一番下でなおかつ一番右の時
            else if (g_side_pointer == g_array_Script.g_stage_remainder && g_spwern_script.g_json_button_array[g_var_pointer, g_side_pointer + 1] == null) {
                g_side_pointer = 0;
                g_var_pointer = 0;
                g_page_turnover = 1;

            } else if (g_side_pointer == g_spwern_script.g_side_num - 1 && g_var_pointer == g_var_maxcheck_pointer - 1) {

                g_side_pointer = 0;
                g_var_pointer = 0;
                g_page_turnover = 1;

            } else {
                g_side_pointer += 1;
            }
            ButtonBig();
            g_stick_flag = true;
            //Debug.Log(g_spwern_script.g_json_stage_array[g_var_pointer, g_side_pointer]);
        }
        if (Input.GetKeyDown(KeyCode.W)|| (Input.GetAxisRaw("Vertical") > g_controller_time && g_stick_flag == false)) {
            ButtonOrigin();
            //ポインターがはみ出そうなとき
            if (g_var_pointer == 0) {
                g_var_pointer = g_spwern_script.g_varmax_num-1;
                g_var_pointer = VarNullChacker(0);
                g_page_turnover = g_var_remainder;
            } else {
            g_var_pointer -= 1;
                g_page_turnover -= 1;
            }
            ButtonBig();
            g_stick_flag = true;
            //Debug.Log(g_spwern_script.g_json_stage_array[g_var_pointer, g_side_pointer]);
            //次のページに行く処理
            if (g_page_turnover == 0) {
                this.transform.localPosition -= new Vector3(0, g_move_g_var * 20, 0);
                g_page_turnover = g_page_var_count;
                g_page_turnover -= 1;

            }
        }
        if (Input.GetKeyDown(KeyCode.S)||(Input.GetAxisRaw("Vertical") < -g_controller_time && g_stick_flag == false)) {
            ButtonOrigin();
            //ポインターがはみ出そうなとき
            if (g_var_pointer == g_spwern_script.g_varmax_num-1) {
                g_var_pointer = 0;
                g_page_turnover = 1;
            } else {
                g_page_turnover += 1;
            g_var_pointer += 1;
            VarNullChacker(1);
            }
            ButtonBig();
            g_stick_flag = true;
            //次のページに行く処理
            if (g_page_var_count  == g_page_turnover ) {
                this.transform.localPosition += new Vector3(0, g_move_g_var * 20, 0);
                g_page_turnover =1;
                ButtonBig();
            }

            //Debug.Log(g_spwern_script.g_json_stage_array[g_var_pointer, g_side_pointer]);
        }
        if (Input.GetAxisRaw("Vertical") > -g_limit_num && Input.GetAxisRaw("Vertical") < g_limit_num && Input.GetAxisRaw("Horizontal") < g_limit_num && Input.GetAxisRaw("Horizontal") > -g_limit_num && g_stick_flag) {
            g_stick_flag = false;
        }
            if (Input.GetKeyDown(KeyCode.Space)||Input.GetButtonDown("B")) {
            g_stagename = g_spwern_script.g_json_button_array[g_var_pointer, g_side_pointer].GetComponent<GetStagename>();
            g_stagename.g_get_stagename = g_spwern_script.g_json_stage_array[g_var_pointer, g_side_pointer];
            g_stagename.OnClick();
        }
        #endregion
    }
    /// <summary>
    /// たてれつのnullを調べる
    /// </summary>
    /// <param name="g_vectorenum">入力された方向を判断する数値（0が上、1が下）</param>
    /// <returns></returns>
    private int VarNullChacker(int g_vectorenum) {
        while (true) {
            if (g_spwern_script.g_json_button_array[g_var_pointer, g_side_pointer] == null) {
                if (g_vectorenum == 1) {
                    g_var_pointer = 0;
                    return g_var_pointer;
                } else {
                    g_var_pointer = g_spwern_script.g_varmax_num - 2;
                    return g_var_pointer;
                }
            } else {
                return g_var_pointer;
            }
        }

    }
    /// <summary>
    /// よこれつのnullを調べる
    /// </summary>
    /// <param name="g_vectorenum">入力された方向を判断する数値（0が上、1が下）</param>
    /// <returns></returns>
    private int SideNullChacker(int g_vectorenum) {
        while (true) {
            if (g_spwern_script.g_json_button_array[g_var_pointer, g_side_pointer] == null) {
                g_side_pointer-=1;
                Debug.Log(g_side_pointer);
            } else {
                return g_side_pointer;
            }
        }
    }
    /// <summary>
    /// 対処のものをでっかくするメソッド
    /// </summary>
    private void ButtonBig() {
            g_buttonSize = g_spwern_script.g_json_button_array[g_var_pointer, g_side_pointer].GetComponent<ButtonSizeChange>();
        
        g_buttonSize.BigButton();
    }
    /// <summary>
    /// 対象のもののサイズを戻すメソッド
    /// </summary>
    private void ButtonOrigin() {
        g_buttonSize = g_spwern_script.g_json_button_array[g_var_pointer, g_side_pointer].GetComponent<ButtonSizeChange>();
        g_buttonSize.OriginButton();
    }
}

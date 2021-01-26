using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelect : MonoBehaviour
{
    //ボタンの配列をつかさどる配列
    SpwernButton g_spwern_script;
    JsonArray g_json_script;
    private StageInformation g_stageInformationScript = null;
    private Stage_Image g_stage_image_Script;
    private Select_Move_Counter g_move_counter_Script;

    /// <summary>
    /// 縦の配列を検索するポインター
    /// </summary>
    public int g_var_pointer;
    /// <summary>
    /// 横の配列を検索するポインター
    /// </summary>
    public int g_side_pointer;
    /// <summary>
    /// 最後のページの縦の数
    /// </summary>
    [SerializeField]
    private int g_var_remainder;
    /// <summary>
    /// 生成するステージ数を取得
    /// </summary>
    private int g_max_stageelement;
    /// <summary>
    /// 1ページの縦の数を取得する変数
    /// </summary>
    [SerializeField]
    private int g_page_var_count;
    /// <summary>
    /// 1ページのステージの数を取得する変数
    /// </summary>
    [SerializeField]
    private int g_stage_page_count;
    /// <summary>
    /// ステージの横の数を取得
    /// </summary>
    public int g_side_stage_count;
    /// <summary>
    /// 最後のページ中のステージが何個あるか
    /// </summary>
    [SerializeField]
    private int g_stage_remainder;
    /// <summary>
    /// g_page_turnover
    /// </summary>
    [SerializeField]
    private int g_page_turnover = 1;
    /// <summary>
    /// ページをめくる数
    /// </summary>
    [SerializeField]
    private float g_move_g_var;

    ButtonSizeChange g_buttonSize;

    GetStagename g_stagename;

    Se_Source g_se_source_Script;

    private float g_controller_time = 0.5f;

    private float g_limit_num = 0.49f;
    void Start()
    {
        //ボタンを生むのに必要な情報を取得
        g_json_script = GameObject.Find("Stageinformation").GetComponent<JsonArray>();
        g_stageInformationScript = GameObject.Find("Stageinformation").GetComponent<StageInformation>();
        g_se_source_Script = GameObject.Find("SEList").GetComponent<Se_Source>();
        g_spwern_script = GetComponent<SpwernButton>();
        g_stage_image_Script = GameObject.Find("Stage_Image").GetComponent<Stage_Image>();
        g_move_counter_Script = GameObject.Find("Stage_Move_Counter").GetComponent<Select_Move_Counter>();
        //ボタンの幅を取得
        g_move_g_var = g_spwern_script.g_button_y_pos;

        //横の数を取得
        g_side_stage_count = g_json_script.g_stage_side;
      
        g_page_var_count = g_spwern_script.g_var_size + 1　;
       
        //生成するステージの数を取得
        g_max_stageelement = g_json_script.g_stage_array_num;
        //1ページのステージの数を計算
        g_stage_page_count = g_side_stage_count * (g_page_var_count -1);
        //全部のページが埋まってた場合
        if (g_max_stageelement % g_stage_page_count == 0) {
            g_var_remainder = g_page_var_count -1;
        } else {
            //最後のページのステージの個数
            g_stage_remainder = g_max_stageelement % g_stage_page_count;
            Debug.Log(g_stage_remainder);

            g_stage_remainder = g_stage_remainder / g_side_stage_count;
            Debug.Log(g_side_stage_count);

            if (g_stage_remainder == 0) {
                g_var_remainder = g_stage_remainder + 1;
                Debug.Log(g_var_remainder);

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
        g_stageInformationScript.Change_StageNum(g_var_pointer, g_side_pointer);

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

                if (g_page_turnover % g_page_var_count == 0) {
                    this.transform.localPosition -= new Vector3(0, g_move_g_var * 20, 0);
                    g_page_turnover = g_page_var_count;
                    g_page_turnover -= 1;
                }
            }
            //一番上の一番左の時
            else if (g_side_pointer == 0 && g_var_pointer == 0) {
                //ステージにあまりがない場合
                if (g_spwern_script.g_remainder_num == 0) {
                    g_var_pointer = g_spwern_script.g_varmax_num - 1;
                    g_side_pointer = g_spwern_script.g_side_num - 1;
                    g_page_turnover = g_var_remainder;
                }
                //あまりがあったとき
                else {
                    Debug.Log("あまり");
                    g_var_pointer = g_spwern_script.g_varmax_num - 1;
                    g_side_pointer = g_spwern_script.g_remainder_num - 1;
                    g_page_turnover = g_var_remainder;
                }
                //最後のページの縦の数を計算して取得
            } else {
            g_side_pointer -= 1;
            }
            ButtonBig();
            g_stick_flag = true;
            g_se_source_Script.Se_Play(2);
            g_stage_image_Script.Stage_Sprite_Change(g_var_pointer, g_side_pointer);
            g_move_counter_Script.Move_Count_Text_Change(g_var_pointer, g_side_pointer);
            g_stageInformationScript.Change_StageNum(g_var_pointer, g_side_pointer);
            //Debug.Log(g_spwern_script.g_json_stage_array[g_var_pointer, g_side_pointer]);
        }
        if (Input.GetKeyDown(KeyCode.D) || (Input.GetAxisRaw("Horizontal") > g_controller_time && g_stick_flag == false)) {
            ButtonOrigin();

            //一番下以外の一番右の時
            if (g_side_pointer == g_spwern_script.g_side_num - 1 && g_var_pointer != g_spwern_script.g_varmax_num-1) {
                g_side_pointer = 0;
                g_var_pointer += 1;
                g_page_turnover += 1;
                if (g_page_var_count == g_page_turnover) {
                    this.transform.localPosition += new Vector3(0, g_move_g_var * 20, 0);
                    g_page_turnover = 1;
                }
            }
            ////一番下でなおかつ一番右の時　ここの処理でnullエラー？
            else if (g_side_pointer == g_json_script.g_stage_remainder - 1  && g_spwern_script.g_json_button_array[g_var_pointer, g_side_pointer + 1] == null) {
                g_side_pointer = 0;
                g_var_pointer = 0;
                g_page_turnover = 1;
            }
            //最後のページの横が全部埋まってた時
            else if (g_side_pointer == g_spwern_script.g_side_num - 1 && g_var_pointer == g_spwern_script.g_varmax_num - 1) {
                g_side_pointer = 0;
                g_var_pointer = 0;
                g_page_turnover = 1;
            } else {
                g_side_pointer += 1;
            }

            ButtonBig();
            g_stick_flag = true;
            g_se_source_Script.Se_Play(2);
            g_stage_image_Script.Stage_Sprite_Change(g_var_pointer, g_side_pointer);
            g_move_counter_Script.Move_Count_Text_Change(g_var_pointer, g_side_pointer);
            g_stageInformationScript.Change_StageNum(g_var_pointer, g_side_pointer);
            //Debug.Log(g_spwern_script.g_json_stage_array[g_var_pointer, g_side_pointer]);
        }
        if (Input.GetKeyDown(KeyCode.W)|| (Input.GetAxisRaw("Vertical") > g_controller_time && g_stick_flag == false)) {
            ButtonOrigin();
            //ポインターがはみ出そうなとき
            if (g_var_pointer == 0) {
                g_var_pointer = g_spwern_script.g_varmax_num-1;
                g_var_pointer = VarNullChacker(0);

                //一番下があまりの部分かそうじゃないか
                if (g_var_pointer == g_spwern_script.g_varmax_num - 1) {
                    g_page_turnover = g_var_remainder;
                } else if (g_var_pointer != g_spwern_script.g_varmax_num - 1) {
                    g_page_turnover = g_var_remainder - 1;
                }

            } else {
                g_var_pointer -= 1;
                g_page_turnover -= 1;
            }
            ButtonBig();
            g_stick_flag = true;
            
            
            //次のページに行く処理
            if (g_page_turnover == 0) {
                this.transform.localPosition -= new Vector3(0, g_move_g_var * 20, 0);
                g_page_turnover = g_page_var_count;
                g_page_turnover -= 1;

            }
            g_se_source_Script.Se_Play(2);
            g_stage_image_Script.Stage_Sprite_Change(g_var_pointer, g_side_pointer);
            g_move_counter_Script.Move_Count_Text_Change(g_var_pointer, g_side_pointer);
            g_stageInformationScript.Change_StageNum(g_var_pointer, g_side_pointer);
        }
        if (Input.GetKeyDown(KeyCode.S)||(Input.GetAxisRaw("Vertical") < -g_controller_time && g_stick_flag == false)) {
            ButtonOrigin();
            //ポインターがはみ出そうなとき
            if (g_var_pointer == g_spwern_script.g_varmax_num - 1) {
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
            g_se_source_Script.Se_Play(2);
            g_stage_image_Script.Stage_Sprite_Change(g_var_pointer, g_side_pointer);
            g_move_counter_Script.Move_Count_Text_Change(g_var_pointer, g_side_pointer);
            g_stageInformationScript.Change_StageNum(g_var_pointer, g_side_pointer);
            //Debug.Log(g_spwern_script.g_json_stage_array[g_var_pointer, g_side_pointer]);
        }
        if (Input.GetAxisRaw("Vertical") > -g_limit_num && Input.GetAxisRaw("Vertical") < g_limit_num && Input.GetAxisRaw("Horizontal") < g_limit_num && Input.GetAxisRaw("Horizontal") > -g_limit_num && g_stick_flag) {
            g_stick_flag = false;
        }
            if (Input.GetKeyDown(KeyCode.Space)||Input.GetButtonDown("A")) {
            g_stagename = g_spwern_script.g_json_button_array[g_var_pointer, g_side_pointer].GetComponent<GetStagename>();
            g_stagename.g_get_stagename = g_spwern_script.g_json_stage_array[g_var_pointer, g_side_pointer];
            g_stageInformationScript.Change_StageNum(g_var_pointer,g_side_pointer);
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
                    g_page_turnover = 1;
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

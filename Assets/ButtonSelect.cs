using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelect : MonoBehaviour
{
    //ボタンの配列をつかさどる配列
    SpwernButton g_spwern_script;

    //配列を検索するポインター
    [SerializeField]
    int g_var_pointer;

    [SerializeField]
    int g_side_pointer;

    ButtonSizeChange g_buttonSize;

    GetStagename g_stagename;
    // Start is called before the first frame update
    void Start()
    {
        g_spwern_script = GetComponent<SpwernButton>();

        Debug.Log(g_spwern_script.g_json_stage_array[g_var_pointer, g_side_pointer]);

    }
    bool g_oneflag;
    bool g_stick_flag;
    // Update is called once per frame
    void Update()
    {
        if (g_oneflag == false) {

        ButtonBig();
            g_oneflag = true;
        }
        #region キーが押されたときの処理
        //Aキーが押されたとき
        if (Input.GetKeyDown(KeyCode.A)||(Input.GetAxisRaw("Horizontal") <-0.9&&g_stick_flag==false)) {
            ButtonOrigin();
            //２段目以降の一番左の時
            if (g_side_pointer == 0 && g_var_pointer != 0) {
                g_var_pointer -= 1;
                g_side_pointer = g_spwern_script.g_side_num - 1;
                
            }
            //一番上の一番左の時
            else if (g_side_pointer == 0 && g_var_pointer == 0) {
                g_var_pointer = g_spwern_script.g_varmax_num - 1;
                g_side_pointer = g_spwern_script.g_side_num-g_spwern_script.g_remainder_num-1;
            } else {
            g_side_pointer -= 1;
            }
            ButtonBig();
            g_stick_flag = true;
            Debug.Log(g_spwern_script.g_json_stage_array[g_var_pointer, g_side_pointer]);
        }
        if (Input.GetKeyDown(KeyCode.D) || (Input.GetAxisRaw("Horizontal") >0.9 && g_stick_flag == false)) {
            ButtonOrigin();
            //一番下以外の一番右の時
            if (g_side_pointer == g_spwern_script.g_side_num - 1 && g_var_pointer != g_spwern_script.g_varmax_num) {
                g_side_pointer = 0;
                g_var_pointer += 1;
            }
            //一番下でなおかつ一番右の時
            else if (g_side_pointer < g_spwern_script.g_side_num -1 && g_spwern_script.g_json_button_array[g_var_pointer, g_side_pointer+1] == null) {
                g_side_pointer = 0;
                g_var_pointer = 0;
            } else {
            g_side_pointer += 1;
            }
            ButtonBig();
            g_stick_flag = true;
            Debug.Log(g_spwern_script.g_json_stage_array[g_var_pointer, g_side_pointer]);
        }
        if (Input.GetKeyDown(KeyCode.W)|| (Input.GetAxisRaw("Vertical") > 0.9 && g_stick_flag == false)) {
            ButtonOrigin();
            //ポインターがはみ出そうなとき
            if (g_var_pointer == 0) {
                g_var_pointer = g_spwern_script.g_varmax_num-1;
                g_var_pointer = VarNullChacker(0);
            } else {
            g_var_pointer -= 1;
            }
            ButtonBig();
            g_stick_flag = true;
            Debug.Log(g_spwern_script.g_json_stage_array[g_var_pointer, g_side_pointer]);
        }
        if (Input.GetKeyDown(KeyCode.S)||(Input.GetAxisRaw("Vertical") < -0.9 && g_stick_flag == false)) {
            ButtonOrigin();
            //ポインターがはみ出そうなとき
            if (g_var_pointer == g_spwern_script.g_varmax_num-1) {
                g_var_pointer = 0;
            } else {
            g_var_pointer += 1;
            VarNullChacker(1);
            }
            ButtonBig();
            g_stick_flag = true;
            Debug.Log(g_spwern_script.g_json_stage_array[g_var_pointer, g_side_pointer]);
        }
        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0&&g_stick_flag) {
            g_stick_flag = false;
        }
            if (Input.GetKeyDown(KeyCode.Space)||Input.GetButtonDown("A")) {
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

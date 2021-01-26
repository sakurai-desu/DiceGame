using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXbox : MonoBehaviour
{
    CameraPote g_camera_rotate;
    Playermove g_player_move;
    private Player_Move g_player_move_Script;
    private Undo_Script g_undo_Script;

    /// <summary>
    /// xボタンが押されているときのフラグ
    /// </summary>
    bool g_xPushFlag = false;
    bool g_axis_flag;
    /// <summary>
    /// カメラの向き
    /// </summary>
    private int g_camera_num;

    /// <summary>
    /// 縦のプラス方向のパラメータ
    /// </summary>
    private const int g_ver_plus_Para = 31;
    /// <summary>
    /// 縦のマイナス方向のパラメータ
    /// </summary>
    private const int g_ver_minus_Para = 33;
    /// <summary>
    /// 横のプラス方向のパラメータ
    /// </summary>
    private const int g_side_plus_Para = 30;
    /// <summary>
    /// 横のマイナス方向のパラメータ
    /// </summary>
    private const int g_side_minus_Para = 32;

    private float g_controller_Move = 0.5f;

    private float g_start_timer=0.18f;
    private float g_timer=0;

    private float g_limit_num = 0.49f;

    bool g_y_push_flag;

    //スタートボタンが押されたかどうかを判断するスクリプト
    PushStartScri g_pushStart_Script;

    private int[] g_camera_para = { 31, 30, 33, 32 };
    int[] g_work_array;

    void Start()
    {
        g_work_array = new int[4];
        g_player_move_Script = this.GetComponent<Player_Move>();
        g_timer = g_start_timer;
        g_pushStart_Script = GameObject.Find("StartChackObj").GetComponent<PushStartScri>();
        g_undo_Script = GameObject.Find("Game_Controller").GetComponent<Undo_Script>();
    }

    void Update() {
        g_timer -= Time.deltaTime;
        if (g_timer > 0) {
            return;
        }

        if (g_pushStart_Script.g_start_flag == false) {

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButton("A")) {
                g_timer = g_start_timer;
                g_player_move_Script.Dice_Push();
            }
            if (Input.GetButtonDown("Y")) {
                g_undo_Script.Undo_Play();
            }

            if (Input.GetButtonDown("Y")) {
                g_y_push_flag = true;
            }
            if (Input.GetButtonUp("Y") && g_y_push_flag) {
                g_y_push_flag = false;
            }

            if (Input.GetButtonDown("X")) {
                ChangeXFlag();
            }
            if (Input.GetButtonUp("X") && g_xPushFlag) {
                ChangeXFlag();
            }
            //配列hの上限に達してない時移動(上)
            if (Input.GetKeyDown(KeyCode.W) || (Input.GetAxisRaw("Vertical") > g_controller_Move && g_axis_flag == false)) {
                g_player_move_Script.PlayerMove(g_camera_para[0]);
                Axis_True();
            }
            //配列hの下限に達してない時移動(下)
            if (Input.GetKeyDown(KeyCode.S) || (Input.GetAxisRaw("Vertical") < -g_controller_Move && g_axis_flag == false)) {
                g_player_move_Script.PlayerMove(g_camera_para[2]);
                Axis_True();
            }
            //配列vの下限に達してない時移動(左)
            if (Input.GetKeyDown(KeyCode.A) || (Input.GetAxisRaw("Horizontal") < -g_controller_Move && g_axis_flag == false)) {
                g_player_move_Script.PlayerMove(g_camera_para[3]);
                Axis_True();
            }
            //配列vの上限に達してない時移動(右)
            if (Input.GetKeyDown(KeyCode.D) || (Input.GetAxisRaw("Horizontal") > g_controller_Move && g_axis_flag == false)) {
                g_player_move_Script.PlayerMove(g_camera_para[1]);
                Axis_True();
            }
            //スティックが戻されたとき
            if (Input.GetAxisRaw("Vertical") > -g_limit_num && Input.GetAxisRaw("Vertical") < g_limit_num && Input.GetAxisRaw("Horizontal") < g_limit_num && Input.GetAxisRaw("Horizontal") > -g_limit_num) {
                g_axis_flag = false;
            }

        }
    }

    private void Axis_True() {
        g_player_move_Script.Change_Move_Flag();
        g_axis_flag = true;
        g_timer = g_start_timer;
    }

    public bool Get_Axis_Flag() {
        return g_axis_flag;
    }
    /// <summary>
    /// フラグを変更させる
    /// </summary>
    private void ChangeXFlag() {
        if (g_xPushFlag) {
            g_xPushFlag = false;
        } else {
            g_xPushFlag = true;
        }
    }

    /// <summary>
    /// カメラの向きを示す数値を変更する
    /// </summary>
    /// <param name="num"></param>
    public void Change_CameraNum(int num) {
        g_camera_num = num;
    }

    /// <summary>
    /// playerの入れ替えをする処理
    /// </summary>
    public void ChangePlayerR() {
        //プレイヤーが進む数をworkに入れる
        g_work_array[0] = g_camera_para[0];
        g_work_array[1] = g_camera_para[1];
        g_work_array[2] = g_camera_para[2];
        g_work_array[3] = g_camera_para[3];
        ChangeR();
    }
    void ChangeR() {
        //入れ替え
        g_camera_para[1] = g_work_array[0];
        g_camera_para[2] = g_work_array[1];
        g_camera_para[3] = g_work_array[2];
        g_camera_para[0] = g_work_array[3];
    }

    /// <summary>
    /// playerの入れ替えをする処理
    /// </summary>
    public void ChangePlayerL() {
        //プレイヤーが進む数をworkに入れる
        g_work_array[0] = g_camera_para[0];
        g_work_array[1] = g_camera_para[1];
        g_work_array[2] = g_camera_para[2];
        g_work_array[3] = g_camera_para[3];
        ChangeL();
    }
    void ChangeL() {
        //入れ替え
        g_camera_para[3] = g_work_array[0];
        g_camera_para[0] = g_work_array[1];
        g_camera_para[1] = g_work_array[2];
        g_camera_para[2] = g_work_array[3];
    }
}

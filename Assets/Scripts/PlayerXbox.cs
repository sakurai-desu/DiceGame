using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXbox : MonoBehaviour
{
    CameraPote g_camera_rotate;
    Playermove g_player_move;
    private Player_Move g_player_move_Script;

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

    private float g_start_timer=0.15f;
    private float g_timer=0;

    private float g_limit_num = 0.49f;
    void Start()
    {
        g_camera_rotate = GameObject.Find("CameraPoint").GetComponent<CameraPote>();
        g_player_move_Script = this.GetComponent<Player_Move>();
        g_timer = g_start_timer;
    }

    void Update() {
        g_timer -= Time.deltaTime;
        if (g_timer > 0) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("A")) {
            g_timer = g_start_timer;
            g_player_move_Script.Jump();
        }
        g_camera_num = g_camera_rotate.CameraRote();
        //if (g_camera_num == 0 || g_camera_num == 1) {
        //    g_camera_num = 0;
        //} else {
        //    g_camera_num = 2;
        //}
        //カメラの向きに応じてプレイヤーの操作キーを変更する
        switch (g_camera_num) {
            case 0:
                //配列hの上限に達してない時移動(上)
                if (Input.GetKeyDown(KeyCode.W) || (Input.GetAxisRaw("Vertical") > g_controller_Move && g_axis_flag == false)) {
                    g_player_move_Script.PlayerMove(g_ver_plus_Para);
                    Axis_True();
                    Debug.Log(g_camera_rotate.CameraRote());
                }
                //配列hの下限に達してない時移動(下)
                if (Input.GetKeyDown(KeyCode.S) || (Input.GetAxisRaw("Vertical") < -g_controller_Move && g_axis_flag == false)) {
                    g_player_move_Script.PlayerMove(g_ver_minus_Para);
                    Axis_True();
                }
                //配列vの下限に達してない時移動(左)
                if (Input.GetKeyDown(KeyCode.A) || (Input.GetAxisRaw("Horizontal") < -g_controller_Move && g_axis_flag == false)) {
                    g_player_move_Script.PlayerMove(g_side_minus_Para);
                    Axis_True();
                }
                //配列vの上限に達してない時移動(右)
                if (Input.GetKeyDown(KeyCode.D) || (Input.GetAxisRaw("Horizontal") > g_controller_Move && g_axis_flag == false)) {
                    g_player_move_Script.PlayerMove(g_side_plus_Para);
                    Axis_True();
                }
                //スティックが戻されたとき
                if (Input.GetAxisRaw("Vertical") > -g_limit_num && Input.GetAxisRaw("Vertical") < g_limit_num && Input.GetAxisRaw("Horizontal") < g_limit_num && Input.GetAxisRaw("Horizontal") > -g_limit_num) {
                    g_axis_flag = false;
                }
                break;
            case 1:
                //配列hの上限に達してない時移動(上)
                if (Input.GetKeyDown(KeyCode.W) || (Input.GetAxisRaw("Vertical") > g_controller_Move && g_axis_flag == false)) {
                    g_player_move_Script.PlayerMove(g_ver_minus_Para);
                    Axis_True();
                }
                //配列hの下限に達してない時移動(下)
                if (Input.GetKeyDown(KeyCode.S) || (Input.GetAxisRaw("Vertical") < -g_controller_Move && g_axis_flag == false)) {
                    g_player_move_Script.PlayerMove(g_ver_plus_Para);
                    Axis_True();
                }
                //配列vの下限に達してない時移動(左)
                if (Input.GetKeyDown(KeyCode.A) || (Input.GetAxisRaw("Horizontal") < -g_controller_Move && g_axis_flag == false)) {
                    g_player_move_Script.PlayerMove(g_side_plus_Para);
                    Axis_True();
                }
                //配列vの上限に達してない時移動(右)
                if (Input.GetKeyDown(KeyCode.D) || (Input.GetAxisRaw("Horizontal") > g_controller_Move && g_axis_flag == false)) {
                    g_player_move_Script.PlayerMove(g_side_minus_Para);
                    Axis_True();
                }

                //スティックが戻されたとき
                if (Input.GetAxisRaw("Vertical") > -g_limit_num && Input.GetAxisRaw("Vertical") < g_limit_num && Input.GetAxisRaw("Horizontal") < g_limit_num && Input.GetAxisRaw("Horizontal") > -g_limit_num) {
                    g_axis_flag = false;
                }
                break;
        }
    }

    private void Axis_True() {
        g_axis_flag = true;
        g_timer = g_start_timer;
    }

    /// <summary>
    /// カメラの向きを示す数値を変更する
    /// </summary>
    /// <param name="num"></param>
    public void Change_CameraNum(int num) {
        g_camera_num = num;
    }
}

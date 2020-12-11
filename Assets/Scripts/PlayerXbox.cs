using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXbox : MonoBehaviour
{
    Playermove g_player_move;
    private Player_Move g_player_move_Script;
    private Player_Appearance_Move g_appearance_move_Script;

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

    void Start()
    {
        g_player_move_Script = this.GetComponent<Player_Move>();
        g_appearance_move_Script = this.GetComponent<Player_Appearance_Move>();
    }

    // Update is called once per frame
    void Update() {
        //    #region ボタンの処理
        //    if (Input.GetAxisRaw("Vertical") > 0.9&&g_axis_flag==false) {
        //        g_player_move.Move_W();
        //        g_axis_flag = true;
        //    }
        //    if (Input.GetAxisRaw("Vertical") < -0.9&&g_axis_flag==false) {
        //        g_player_move.Move_S();
        //        g_axis_flag = true;
        //    }
        //    if (Input.GetAxisRaw("Horizontal") > 0.9&&g_axis_flag==false) {
        //        g_player_move.Move_D();
        //        g_axis_flag = true;
        //    }
        //    if (Input.GetAxisRaw("Horizontal") < -0.9&&g_axis_flag==false) {
        //        g_player_move.Move_A();
        //        g_axis_flag = true;
        //    }
        //    //スティックが戻されたとき
        //    if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0) {
        //        g_axis_flag = false;
        //    }
        //    #endregion

        if (g_appearance_move_Script.Get_MoveFlag()) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetButtonDown("A")) {
            g_player_move_Script.Jump();
        }
        if (g_camera_num == 0 || g_camera_num == 1) {
            g_camera_num = 0;
        } else {
            g_camera_num = 2;
        }
        //カメラの向きに応じてプレイヤーの操作キーを変更する
        switch (g_camera_num) {
            case 0:
                //配列hの上限に達してない時移動(上)
                if (Input.GetKeyDown(KeyCode.W)|| Input.GetAxisRaw("Vertical") > 0.9 && g_axis_flag == false) {
                    g_player_move_Script.PlayerMove(g_ver_plus_Para);
                    g_axis_flag = true;
                }
                //配列hの下限に達してない時移動(下)
                if (Input.GetKeyDown(KeyCode.S)|| Input.GetAxisRaw("Vertical") < -0.9 && g_axis_flag == false) {
                    g_player_move_Script.PlayerMove(g_ver_minus_Para);
                    g_axis_flag = true;
                }
                //配列vの下限に達してない時移動(左)
                if (Input.GetKeyDown(KeyCode.A)|| Input.GetAxisRaw("Horizontal") <- 0.9 && g_axis_flag == false) {
                    g_player_move_Script.PlayerMove(g_side_minus_Para);
                    g_axis_flag = true;
                }
                //配列vの上限に達してない時移動(右)
                if (Input.GetKeyDown(KeyCode.D)|| Input.GetAxisRaw("Horizontal") >0.9 && g_axis_flag == false) {
                    g_player_move_Script.PlayerMove(g_side_plus_Para);
                    g_axis_flag = true;
                }
                //スティックが戻されたとき
                if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0) {
                    g_axis_flag = false;
                }
                break;
            case 2:
                //配列hの上限に達してない時移動(上)
                if (Input.GetKeyDown(KeyCode.W) || Input.GetAxisRaw("Vertical") > 0.9 && g_axis_flag == false) {
                    g_player_move_Script.PlayerMove(g_ver_minus_Para);
                    g_axis_flag = true;
                }
                //配列hの下限に達してない時移動(下)
                if (Input.GetKeyDown(KeyCode.S) || Input.GetAxisRaw("Vertical") < -0.9 && g_axis_flag == false) {
                    g_player_move_Script.PlayerMove(g_ver_plus_Para);
                    g_axis_flag = true;
                }
                //配列vの下限に達してない時移動(左)
                if (Input.GetKeyDown(KeyCode.A) || Input.GetAxisRaw("Horizontal") <- 0.9 && g_axis_flag == false) {
                    g_player_move_Script. PlayerMove(g_side_plus_Para);
                    g_axis_flag = true;
                }
                //配列vの上限に達してない時移動(右)
                if (Input.GetKeyDown(KeyCode.D) || Input.GetAxisRaw("Horizontal") >0.9 && g_axis_flag == false) {
                    g_player_move_Script. PlayerMove(g_side_minus_Para);
                    g_axis_flag = true;
                }

                //スティックが戻されたとき
                if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0) {
                    g_axis_flag = false;
                }
                break;
        }
    }
    /// <summary>
    /// カメラの向きを示す数値を変更する
    /// </summary>
    /// <param name="num"></param>
    public void Change_CameraNum(int num) {
        g_camera_num = num;
    }
}

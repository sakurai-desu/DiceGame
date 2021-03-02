using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Direction : MonoBehaviour {

    private Input_Data g_json_Script;

    private GameObject g_player_Obj;
    /// <summary>
    /// ゲーム開始時のプレイヤーの向き
    /// </summary>
    private int g_start_Para;

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
    /// <summary>
    /// 現在のプレイヤーの向き
    /// </summary>
    private int g_player_direction=0;

    void Start() {

    }

    public void Start_Direction(GameObject player) {
        g_json_Script = GameObject.Find("Game_Controller").GetComponent<Input_Data>();
        g_start_Para = g_json_Script.g_inputJson.g_p_direction;
        g_player_Obj = player;
        Player_Direction_Change(g_start_Para);
        g_player_direction = g_start_Para;
    }

    /// <summary>
    /// プレイヤーオブジェクトの向いている方向を変更する処理
    /// </summary>
    /// <param name="para"></param>
    public void Player_Direction_Change(int para) {
        g_player_direction = para;
        switch (para) {
            case g_ver_plus_Para:
                g_player_Obj.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                break;
            case g_ver_minus_Para:
                g_player_Obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            case g_side_plus_Para:
                g_player_Obj.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
                break;
            case g_side_minus_Para:
                g_player_Obj.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                break;
        }
    }

    /// <summary>
    /// プレイヤーの向きを表す数値を返す処理
    /// </summary>
    /// <returns></returns>
    public int Get_Player_Direction() {
        return g_player_direction;
    }
}

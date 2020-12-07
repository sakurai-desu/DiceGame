using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Direction : MonoBehaviour {

    private Input_Date g_json_Script;

    private GameObject g_player_Obj;

    private int g_start_Para;

    private const int g_ver_plus_Para = 31;
    private const int g_ver_minus_Para = 33;
    private const int g_side_plus_Para = 30;
    private const int g_side_minus_Para = 32;

    void Start() {

    }

    public void Start_Direction(GameObject player) {
        g_json_Script = GameObject.Find("Game_Controller").GetComponent<Input_Date>();
        g_start_Para = g_json_Script.g_inputJson.g_p_direction;
        g_player_Obj = player;
        Player_Direction_Change(g_start_Para);
    }

    /// <summary>
    /// プレイヤーキャラクターの向いている方向を変更する処理
    /// </summary>
    /// <param name="para"></param>
    public void Player_Direction_Change(int para) {
        switch (para) {
            case g_ver_plus_Para:
                g_player_Obj.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
                break;
            case g_ver_minus_Para:
                g_player_Obj.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                break;
            case g_side_plus_Para:
                g_player_Obj.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                break;
            case g_side_minus_Para:
                g_player_Obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
        }
    }
}

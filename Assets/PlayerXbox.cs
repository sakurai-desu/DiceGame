using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXbox : MonoBehaviour
{
    Playermove g_player_move;

    bool g_axis_flag;
    // Start is called before the first frame update
    void Start()
    {
        g_player_move = GetComponent<Playermove>();
    }

    // Update is called once per frame
    void Update()
    {
        #region ボタンの処理
        if (Input.GetAxisRaw("Vertical") > 0.9&&g_axis_flag==false) {
            g_player_move.Move_W();
            g_axis_flag = true;
        }
        if (Input.GetAxisRaw("Vertical") < -0.9&&g_axis_flag==false) {
            g_player_move.Move_S();
            g_axis_flag = true;
        }
        if (Input.GetAxisRaw("Horizontal") > 0.9&&g_axis_flag==false) {
            g_player_move.Move_D();
            g_axis_flag = true;
        }
        if (Input.GetAxisRaw("Horizontal") < -0.9&&g_axis_flag==false) {
            g_player_move.Move_A();
            g_axis_flag = true;
        }
        //スティックが戻されたとき
        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0) {
            g_axis_flag = false;
        }
        #endregion
    }
}

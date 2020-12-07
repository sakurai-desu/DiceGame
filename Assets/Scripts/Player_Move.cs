using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    private Input_Date g_json_Script;
    private Game_Controller g_game_con_Script;
    private Playercontroller g_play_con_Script;
    private Player_Direction g_direction_Script;
    private Dice_Controller g_dice_con_Script;
    private Player_Animation g_anim_Script;
    private Player_Appearance_Move g_move_Script;

    private const int g_zero_Count = 0;
    private int g_max_ver;
    private int g_max_side;
    private int g_max_high;

    private int g_player_ver;
    private int g_player_side;
    private int g_player_high;

    void Start()
    {
        g_json_Script =GameObject.Find("Game_Controller").GetComponent<Input_Date>();
        g_game_con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        g_play_con_Script = GameObject.Find("Player_Controller").GetComponent<Playercontroller>();
        g_direction_Script = GameObject.Find("Player_Controller").GetComponent<Player_Direction>();
        g_dice_con_Script = GameObject.Find("Dice_Controller").GetComponent<Dice_Controller>();
        g_move_Script = this.GetComponent<Player_Appearance_Move>();
        g_anim_Script = this.GetComponent<Player_Animation>();

        (g_max_ver, g_max_side, g_max_high) = g_json_Script.Get_Array_Max();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) {
            Ver_Plus_Move();
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            Ver_Minus_Move();
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            Side_Plus_Move();
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            Side_Minus_Move();
        }
    }

    private void Ver_Plus_Move() {
        (g_player_ver, g_player_side, g_player_high) = g_play_con_Script.Get_Player_Pointer();
        Move(g_player_ver+1, g_player_side, g_player_high);
    }

    private void Ver_Minus_Move() {
        (g_player_ver, g_player_side, g_player_high) = g_play_con_Script.Get_Player_Pointer();
        Move(g_player_ver-1, g_player_side, g_player_high);
    }

    private void Side_Plus_Move() {
        (g_player_ver, g_player_side, g_player_high) = g_play_con_Script.Get_Player_Pointer();
        Move(g_player_ver , g_player_side+1, g_player_high);
    }

    private void Side_Minus_Move() {
        (g_player_ver, g_player_side, g_player_high) = g_play_con_Script.Get_Player_Pointer();
        Move(g_player_ver , g_player_side-1, g_player_high);
    }

    private void Move(int ver,int side,int high) {
        //移動先が配列の範囲外の時
        if (ver < g_zero_Count || g_max_ver <= ver
            || side < g_zero_Count || g_max_side <= side
                || high < g_zero_Count || g_max_high <= high) {
            Debug.Log("移動先は範囲外");
            //処理終了
            //移動させない状態を返す
            return;
        }
        //g_direction_Script.Player_Direction_Change(30);
        int type = g_game_con_Script.Get_Obj_Type(ver, side, high);
        switch (type) {
            case 0:
                break;
            case 100:
                //g_dice_con_Script.Change_Player_Pointer(g_movecheck_v, g_movecheck_s, g_movecheck_h);
                //GameObject dice_obj = g_game_con_Script.Get_Obj(g_movecheck_v + 1, g_movecheck_s, g_movecheck_h);
                //g_dice_con_Script.Storage_Control_Obj(dice_obj, 0);
                //g_anim_Script.Player_Roll_Anim();
                break;
        }
    }
}

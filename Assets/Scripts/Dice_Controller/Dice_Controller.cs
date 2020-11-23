using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Controller : MonoBehaviour {
    private Dice_Squares g_dice_Script;
    private Dice_Rotate g_rotate_Script;

    private Parent_Dice g_parent_Script;
    private Parent_All_Rotation g_parent_rotate_Script;

    [SerializeField]
    private GameObject g_test_con_Obj;
    [SerializeField]
    private GameObject g_con_Obj;
    [SerializeField]
    private GameObject g_next_con_Obj;
    [SerializeField]
    private GameObject g_con_Obj_Parent;

<<<<<<< HEAD
    private int g_player_ver = 0;
    private int g_player_side = 0;
    private int g_player_high = 1;

    private const int g_ver_plus_Para = 0;
    private const int g_ver_minus_Para = 1;
    private const int g_side_plus_Para = 2;
    private const int g_side_minus_Para = 3;
=======
    private int g_player_v = 0;
    private int g_player_s = 0;
    private int g_player_h = 0;
>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d

    void Start() {
        g_parent_rotate_Script = GetComponent<Parent_All_Rotation>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            g_con_Obj = g_test_con_Obj;
            g_dice_Script = g_con_Obj.GetComponent<Dice_Squares>();
            g_rotate_Script = g_con_Obj.GetComponent<Dice_Rotate>();
<<<<<<< HEAD
=======
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            Plus_Side_Move();
>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d
        }
        if (g_con_Obj == null) {
            return;
        }

<<<<<<< HEAD
=======


>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d
        //回転中の間は回転させない
        if (g_rotate_Script.Get_Rotate_Flag()) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
<<<<<<< HEAD
            Move(g_side_minus_Para);
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Move(g_side_plus_Para);
=======
            Minus_Side_Move();
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Plus_Side_Move();
>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Move(g_ver_plus_Para);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Move(g_ver_minus_Para);
        }
    }

<<<<<<< HEAD
    /// <summary>
    /// プレイヤーの指標を更新
    /// </summary>
    /// <param name="ver"></param>
    /// <param name="side"></param>
    /// <param name="high"></param>
    public void Change_Player_Pointer(int ver,int side,int high) {
        g_player_ver = ver;
        g_player_side = side;
        g_player_high = high;
    }

    /// <summary>
    /// サイコロを回転移動させる処理
    /// </summary>
    /// <param name="para"></param>
    private void Move(int para) {
=======

    private void Plus_Side_Move() {
>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d
        //回転の中心にしているサイコロの親を取得
        g_con_Obj_Parent = g_con_Obj.transform.parent.gameObject;
        //親のスクリプトを取得
        g_parent_Script = g_con_Obj_Parent.GetComponent<Parent_Dice>();
<<<<<<< HEAD
        switch (para) {
            case g_ver_plus_Para:
                g_next_con_Obj = g_parent_Script.Plus_Ver(g_player_ver, g_player_side, g_player_high);
                break;
            case g_ver_minus_Para:
                g_next_con_Obj = g_parent_Script.Minus_Ver(g_player_ver, g_player_side, g_player_high);
                break;
            case g_side_plus_Para:
                g_next_con_Obj = g_parent_Script.Plus_Side(g_player_ver, g_player_side, g_player_high);
                break;
            case g_side_minus_Para:
                g_next_con_Obj = g_parent_Script.Minus_Side(g_player_ver, g_player_side, g_player_high);
                break;
        }
=======
        //次の回転の中心にするサイコロを取得
        g_next_con_Obj = g_parent_Script.Plus_Side(g_player_v, g_player_s, g_player_h);
        //回転の中心にするサイコロを変更
        g_con_Obj = g_next_con_Obj;
        //サイコロのスクリプト取得
        g_dice_Script = g_con_Obj.GetComponent<Dice_Squares>();
        //サイコロのスクリプト取得
        g_rotate_Script = g_con_Obj.GetComponent<Dice_Rotate>();

        g_dice_Script.Side_Plus_Move();
    }

    private void Minus_Side_Move() {
        //回転の中心にしているサイコロの親を取得
        g_con_Obj_Parent = g_con_Obj.transform.parent.gameObject;
        //親のスクリプトを取得
        g_parent_Script = g_con_Obj_Parent.GetComponent<Parent_Dice>();
        //次の回転の中心にするサイコロを取得
        g_next_con_Obj = g_parent_Script.Minus_Side(g_player_v, g_player_s, g_player_h);
>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d
        //回転の中心にするサイコロを変更
        g_con_Obj = g_next_con_Obj;
        //サイコロのスクリプト取得
        g_dice_Script = g_con_Obj.GetComponent<Dice_Squares>();
        //サイコロのスクリプト取得
        g_rotate_Script = g_con_Obj.GetComponent<Dice_Rotate>();
<<<<<<< HEAD
        //サイコロを親オブジェクトごと回転させる処理
        g_parent_rotate_Script.All_Rotation(g_con_Obj, para);
=======

        g_dice_Script.Side_Minus_Move();
>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d
    }

}

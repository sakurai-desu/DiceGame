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

    private int g_player_v = 0;
    private int g_player_s = 0;
    private int g_player_h = 0;

    void Start() {
        g_parent_rotate_Script = GetComponent<Parent_All_Rotation>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            g_con_Obj = g_test_con_Obj;
            g_dice_Script = g_con_Obj.GetComponent<Dice_Squares>();
            g_rotate_Script = g_con_Obj.GetComponent<Dice_Rotate>();
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            Plus_Side_Move();
        }
        if (g_con_Obj == null) {
            return;
        }



        //回転中の間は回転させない
        if (g_rotate_Script.Get_Rotate_Flag()) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Minus_Side_Move();
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Plus_Side_Move();
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            g_dice_Script.Ver_Plus_Move();
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            g_dice_Script.Ver_Minus_Move();
        }
    }


    private void Plus_Side_Move() {
        //回転の中心にしているサイコロの親を取得
        g_con_Obj_Parent = g_con_Obj.transform.parent.gameObject;
        //親のスクリプトを取得
        g_parent_Script = g_con_Obj_Parent.GetComponent<Parent_Dice>();
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
        //回転の中心にするサイコロを変更
        g_con_Obj = g_next_con_Obj;
        //サイコロのスクリプト取得
        g_dice_Script = g_con_Obj.GetComponent<Dice_Squares>();
        //サイコロのスクリプト取得
        g_rotate_Script = g_con_Obj.GetComponent<Dice_Rotate>();

        g_dice_Script.Side_Minus_Move();
    }
}

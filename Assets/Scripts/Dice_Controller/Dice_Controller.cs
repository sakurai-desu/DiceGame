using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Controller : MonoBehaviour
{
    private Dice_Squares g_dice_Script;
    private Dice_Rotate g_rotate_Script;

    private Parent_Dice g_parent_Script;
    [SerializeField]
    private GameObject g_test_con_Obj;
    [SerializeField]
    private GameObject g_con_Obj;
    [SerializeField]
    private GameObject g_next_con_Obj;
    [SerializeField]
    private GameObject g_con_Obj_Parent;

    private int g_player_v;
    private int g_player_s;
    private int g_player_h;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            Storage_Control_Obj(g_test_con_Obj);
        }
        if (g_con_Obj == null) {
            return;
        }
        //g_dice_Script = g_con_Obj.GetComponent<Dice_Squares>();
        //g_rotate_Script = g_con_Obj.GetComponent<Dice_Rotate>();
        
        //回転中の間は回転させない
        if (g_rotate_Script.Get_Rotate_Flag()) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            g_dice_Script.Side_Minus_Move();
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            g_dice_Script.Side_Plus_Move();
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            g_dice_Script.Ver_Plus_Move();
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            g_dice_Script.Ver_Minus_Move();
        }
    }

    public void Storage_Control_Obj(GameObject storage_Obj) {
        g_con_Obj = storage_Obj;
        g_con_Obj_Parent = g_con_Obj.transform.parent.gameObject;
        g_parent_Script = g_con_Obj_Parent.GetComponent<Parent_Dice>();
        g_next_con_Obj=g_parent_Script.Plus_Side(g_player_v, g_player_s, g_player_h);

        g_dice_Script = g_con_Obj.GetComponent<Dice_Squares>();
        g_rotate_Script = g_con_Obj.GetComponent<Dice_Rotate>();
    }
}

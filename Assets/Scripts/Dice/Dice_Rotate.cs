using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Rotate : MonoBehaviour {
    private Playercontroller g_player_con_Script;
    private Dice_Squares g_dice_Script;
    private Parent_All_Rotation g_parent_rotate_Script;
    private TroubleScr g_trouble_script;
    private Se_Source g_se_source_Script;
    /// <summary>
    /// 回転させるオブジェクト
    /// </summary>
    private GameObject g_dice_Obj;

    private GameObject g_parent_Obj;

    /// <summary>
    /// 回転の中心
    /// </summary>
    private Vector3 g_rotate_Point = Vector3.zero;
    /// <summary>
    /// 回転の軸
    /// </summary>
    private Vector3 g_rotate_Axis = Vector3.zero;
    /// <summary>
    /// サイコロの回転させる角度
    /// </summary>
    private float g_rotation_Amount = 0;
    /// <summary>
    /// 回転させる最大値
    /// </summary>
    private const float g_rotation_Max = 90;
    /// <summary>
    /// 回転の初期速度
    /// </summary>
    private const float g_start_rotation_Speed = 15;
    /// <summary>
                                            /// 回転の速度
                                            /// </summary>
    private float g_rotation_Speed = 15;
    private float g_rotation_speed_Min = 5;
    /// <summary>
    /// サイコロのサイズ
    /// </summary>
    private float g_dice_Size;
    [SerializeField]
    private float g_size_change=1;

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

    void Start() {
        g_player_con_Script = GameObject.Find("Player_Controller").GetComponent<Playercontroller>();
        g_parent_rotate_Script = GameObject.Find("Dice_Controller").GetComponent<Parent_All_Rotation>();
        g_trouble_script = GameObject.Find("TroubleObj").GetComponent<TroubleScr>();
        g_dice_Script = this.GetComponent<Dice_Squares>();
        g_se_source_Script = GameObject.Find("Se_Source").GetComponent<Se_Source>();
        //操作オブジェクト取得
        g_dice_Obj = this.gameObject;
        //サイズを求める
        g_dice_Size = g_dice_Obj.transform.localScale.x / g_size_change;
        //回転速度の初期化
        g_rotation_Speed = g_start_rotation_Speed;
    }

    private void Get_Parent() {
        g_parent_Obj = this.gameObject.transform.parent.gameObject;
    }
    /// <summary>
    /// 与えられたパラメータに応じた方向に回転する処理
    /// </summary>
    /// <param name="para"></param>
    public void This_Rotate(int para) {
        switch (para) {
            case g_ver_plus_Para:
                Ver_Plus_Rotate();
                break;
            case g_ver_minus_Para:
                Ver_Minus_Rotate();
                break;
            case g_side_plus_Para:
                Side_Plus_Rotate();
                break;
            case g_side_minus_Para:
                Side_Minus_Rotate();
                break;
        }
        g_trouble_script.Trouble();
    }

    /// <summary>
    /// 横軸のプラス方向の回転
    /// </summary>
    private void Side_Plus_Rotate() {
        //回転の中心を決める
        g_rotate_Point = g_dice_Obj.transform.position + new Vector3(g_dice_Size, -g_dice_Size, 0);
        //回転の軸を決める
        g_rotate_Axis = new Vector3(0, 0, -1);
        //サイコロ回転
        StartCoroutine(Rotate());
    }
    /// <summary>
    /// 横軸のマイナス方向の回転
    /// </summary>
    private void Side_Minus_Rotate() {
        //回転の中心を決める
        g_rotate_Point = g_dice_Obj.transform.position + new Vector3(-g_dice_Size, -g_dice_Size, 0);
        //回転の軸を決める
        g_rotate_Axis = new Vector3(0, 0, 1);
        //サイコロ回転
        StartCoroutine(Rotate());
    }
    /// <summary>
    /// 縦軸のプラス方向の回転
    /// </summary>
    private void Ver_Plus_Rotate() {
        //回転の中心を決める
        g_rotate_Point = g_dice_Obj.transform.position + new Vector3(0, -g_dice_Size, g_dice_Size);
        //回転の軸を決める
        g_rotate_Axis = new Vector3(1, 0, 0);
        //サイコロ回転
        StartCoroutine(Rotate());
    }
    /// <summary>
    /// 縦軸のマイナス方向の回転
    /// </summary>
    private void Ver_Minus_Rotate() {
        //回転の中心を決める
        g_rotate_Point = g_dice_Obj.transform.position + new Vector3(0, -g_dice_Size, -g_dice_Size);
        //回転の軸を決める
        g_rotate_Axis = new Vector3(-1, 0, 0);
        //サイコロ回転
        StartCoroutine(Rotate());
    }

    /// <summary>
    /// サイコロを一定の速度で回転させる処理
    /// </summary>
    /// <returns></returns>
    IEnumerator Rotate() {
        //回転中にする
        g_player_con_Script.MoveFlag_True();
        //回転速度の初期化
        g_rotation_Speed = g_start_rotation_Speed;
        //回転の角度合計を保持する変数
        float rotation_Sum = 0f;
        //回転させたい親を取得
        Get_Parent();
        //くっついているダイスの個数取得
        int dice_count = g_parent_Obj.GetComponent<Parent_Dice>().Get_Children_Count();
        g_rotation_Speed = g_rotation_Speed - dice_count;
        if (g_rotation_Speed< g_rotation_speed_Min) {
            g_rotation_Speed = g_rotation_speed_Min;
        }
        //合計が決めた角度になるまで続ける
        while (rotation_Sum < g_rotation_Max) {
            //角度を変更
            g_rotation_Amount = g_rotation_Speed;
            //角度合計を加算
            rotation_Sum += g_rotation_Amount;
            //角度合計が最大値を超えてしまった時
            if (rotation_Sum > g_rotation_Max) {
                //角度を調整する
                g_rotation_Amount -= rotation_Sum - g_rotation_Max;
            }
            //軸と中心を元に回転させる
            g_parent_Obj.transform.RotateAround(g_rotate_Point, g_rotate_Axis, g_rotation_Amount);
            yield return null;
        }
        //回転中をではなくする
        g_player_con_Script.MoveFlag_False();
        //回転の中心を初期化
        g_rotate_Point = Vector3.zero;
        //回転の軸を初期化
        g_rotate_Axis = Vector3.zero;
        //回転SE再生
        g_se_source_Script.Dice_Rotate_Se_Play();
        //回転先にダイスを格納する
        g_parent_rotate_Script.Reset_And_Storage_Obj();
        //処理終了
        yield break;
    }
}

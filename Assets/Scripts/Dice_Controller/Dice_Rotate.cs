using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Rotate : MonoBehaviour
{
    private Dice_Squares g_dice_Script;

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
    private float g_rotation_Max = 90;
    /// <summary>
    /// 回転の速度
    /// </summary>
    private float g_rotation_Speed = 15;
    /// <summary>
    /// サイコロのサイズ
    /// </summary>
    private float g_dice_Size;

    /// <summary>
    /// 回転中かどうかを判定するフラグ
    /// </summary>
    private bool g_rotate_Flag = false;

    void Start()
    {
        g_dice_Script = this.GetComponent<Dice_Squares>();
        //操作オブジェクト取得
        g_dice_Obj = this.gameObject;
        //サイズを求める
        g_dice_Size = g_dice_Obj.transform.localScale.x / 1f;
    }

    private void Get_Parent() {
        g_parent_Obj = this.gameObject.transform.parent.gameObject;
    }

    /// <summary>
    /// 横軸のプラス方向の回転
    /// </summary>
    public void Side_Plus_Rotate()
    {
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
    public void Side_Minus_Rotate()
    {
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
    public void Ver_Plus_Rotate()
    {
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
    public void Ver_Minus_Rotate()
    {
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
    IEnumerator Rotate()
    {
        //回転中にする
        g_rotate_Flag = true;
        //回転の角度合計を保持する変数
        float rotation_Sum = 0f;
        //合計が決めた角度になるまで続ける
        while (rotation_Sum < g_rotation_Max)
        {
            //角度を変更
            g_rotation_Amount = g_rotation_Speed;
            //角度合計を加算
            rotation_Sum += g_rotation_Amount;
            //角度合計が最大値を超えてしまった時
            if (rotation_Sum > g_rotation_Max)
            {
                //角度を調整する
                g_rotation_Amount -= rotation_Sum - g_rotation_Max;
            }
            Get_Parent();

            //軸と中心を元に回転させる
            g_parent_Obj.transform.RotateAround(g_rotate_Point, g_rotate_Axis, g_rotation_Amount);
            yield return null;
        }

        //回転中をではなくする
        g_rotate_Flag = false;
        //回転の中心を初期化
        g_rotate_Point = Vector3.zero;
        //回転の軸を初期化
        g_rotate_Axis = Vector3.zero;
        g_dice_Script.All_Check();
        //処理終了
        yield break;
    }

    /// <summary>
    /// 回転フラグを返す処理
    /// </summary>
    /// <returns>回転中か判定するフラグ</returns>
    public bool Get_Rotate_Flag()
    {
        //回転フラグを返す
        return g_rotate_Flag;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Squares : MonoBehaviour {

    private Game_Controller g_game_Con_Script;
    private Dice_Controller g_dice_Con_Script;
    private Check_Dice g_check_Script;
<<<<<<< HEAD
=======
    private Parent_All_Rotation g_all_rotate_Script;
>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d

    /// <summary>
    /// サイコロ回転用スクリプト
    /// </summary>
    private Dice_Rotate g_rotate_Script;

    /// <summary>
    /// 現在のマス目を格納する配列
    /// </summary>
    [SerializeField]
    private int[] g_squares_Array;
    /// <summary>
    /// マス目の値を保持するための配列
    /// </summary>
    private int[] g_work_Array;

    /// <summary>
    /// 初期化用変数
    /// </summary>
    private const int g_zero_Count = 0;
    /// <summary>
    /// 初期化用変数
    /// </summary>
    private const int g_one_Count = 1;

    [SerializeField]
    /// <summary>
    /// 自分の現在の縦の指標
    /// </summary>
    private int g_this_Ver = 0;
    [SerializeField]
    /// <summary>
    /// 自分の現在の横の指標
    /// </summary>
    private int g_this_Side = 0;
    [SerializeField]
    /// <summary>
    /// 自分の現在の高さの指標
    /// </summary>
    private int g_this_High = 0;

    //配列の要素数を取得する変数
    /// <summary>
    /// 縦の要素数
    /// </summary>
    private int g_max_Ver;
    /// <summary>
    /// 横の要素数
    /// </summary>
    private int g_max_Side;
    /// <summary>
    /// 高さの要素数
    /// </summary>
    private int g_max_High;

    private const int g_ver_plus_Para = 0;
    private const int g_ver_minus_Para = 1;
    private const int g_side_plus_Para = 2;
    private const int g_side_minus_Para = 3;

    void Start() {
        g_game_Con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        g_check_Script = GameObject.Find("Dice_Controller").GetComponent<Check_Dice>();
<<<<<<< HEAD
=======
        g_dice_Con_Script= GameObject.Find("Dice_Controller").GetComponent<Dice_Controller>();
        g_all_rotate_Script= GameObject.Find("Dice_Controller").GetComponent<Parent_All_Rotation>();
>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d

        g_rotate_Script = GetComponent<Dice_Rotate>();
        g_work_Array = new int[6];
        //縦横高さを決めた数へ変更する
        (g_max_Ver, g_max_Side, g_max_High) = g_game_Con_Script.Get_Array_Max();
    }

    /// <summary>
    /// 指標を更新する処理
    /// </summary>
    /// <param name="ver">縦</param>
    /// <param name="side">横</param>
    /// <param name="high">高さ</param>
    public void Storage_This_Index(int ver, int side, int high) {
        g_this_Ver = ver;
        g_this_Side = side;
        g_this_High = high;
    }

<<<<<<< HEAD
    public void Change_Squares(int para) {
        switch (para) {
            case g_ver_plus_Para:
                Ver_Plus_Squeares_Change();
                break;
            case g_ver_minus_Para:
                Ver_Minus_Squeares_Change();
                break;
            case g_side_plus_Para:
                Side_Plus_Squeares_Change();
                break;
            case g_side_minus_Para:
                Side_Minus_Squeares_Change();
                break;
        }
    }

    private void Ver_Plus_Squeares_Change() {
=======
    /// <summary>
    /// 横軸のプラス方向の移動
    /// </summary>
    public void Side_Plus_Move() {
        //移動範囲外の時処理終了
        if (g_this_Side + g_one_Count >= g_max_Side) {
            return;
        }
        //移動先が埋まっているか調べる
        int type = g_game_Con_Script.Get_Obj_Type(g_this_Ver, g_this_Side+1, g_this_High);
        //移動先が埋まっていた時処理終了
        if (type == 100) {
            return;
        }

        //オブジェクトを回転させる
        g_rotate_Script.Side_Plus_Rotate();

        g_all_rotate_Script.Plus_Side_Rotate(this.gameObject);
        //このオブジェクトを配列に格納
        //g_game_Con_Script.Storage_Obj(g_this_Ver, g_this_Side + 1, g_this_High, this.gameObject);
        //g_game_Con_Script.Storage_Obj_Type(g_this_Ver, g_this_Side + 1, g_this_High, 100);
        ////元々格納されていたところを空にする
        //g_game_Con_Script.Storage_Reset(g_this_Ver, g_this_Side, g_this_High);
        //自分のいる横の位置を＋1
        //g_this_Side++;
    }

    /// <summary>
    /// 横軸のマイナス方向の移動
    /// </summary>
    public void Side_Minus_Move() {
        //移動範囲外の時処理終了
        if (g_this_Side - g_one_Count < g_zero_Count) {
            return;
        }
        //移動先が埋まっているか調べる
        int type = g_game_Con_Script.Get_Obj_Type(g_this_Ver, g_this_Side - 1, g_this_High);
        //移動先が埋まっていた時処理終了
        if (type == 100) {
            return;
        }
        //オブジェクトを回転させる
        g_rotate_Script.Side_Minus_Rotate();

        g_all_rotate_Script.Minus_Side_Rotate(this.gameObject);
        ////このオブジェクトを配列に格納
        //g_game_Con_Script.Storage_Obj(g_this_Ver, g_this_Side - 1, g_this_High, this.gameObject);
        //g_game_Con_Script.Storage_Obj_Type(g_this_Ver, g_this_Side - 1, g_this_High, 100);
        ////元々格納されていたところを空にする
        //g_game_Con_Script.Storage_Reset(g_this_Ver, g_this_Side, g_this_High);
        ////自分のいる横の位置を−1
        //g_this_Side--;

    }

    /// <summary>
    /// 縦軸のプラス方向の移動
    /// </summary>
    public void Ver_Plus_Move() {
        //移動範囲外の時処理終了
        if (g_this_Ver + g_one_Count >= g_max_Ver) {
            return;
        }
        //移動先が埋まっているか調べる
        int type = g_game_Con_Script.Get_Obj_Type(g_this_Ver+1, g_this_Side, g_this_High);
        //移動先が埋まっていた時処理終了
        if (type == 100) {
            return;
        }
        //オブジェクトを回転させる
        g_rotate_Script.Ver_Plus_Rotate();
>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d
        //現在のマス目を保持
        Retention_Squares();
        //マス目入れ替え
        g_squares_Array[0] = g_work_Array[2];
        g_squares_Array[2] = g_work_Array[4];
        g_squares_Array[4] = g_work_Array[5];
        g_squares_Array[5] = g_work_Array[0];
<<<<<<< HEAD
    }
    private void Ver_Minus_Squeares_Change() {
=======

        //このオブジェクトを配列に格納
        g_game_Con_Script.Storage_Obj(g_this_Ver + 1, g_this_Side, g_this_High, this.gameObject);
        g_game_Con_Script.Storage_Obj_Type(g_this_Ver + 1, g_this_Side, g_this_High, 100);
        //元々格納されていたところを空にする
        g_game_Con_Script.Storage_Reset(g_this_Ver, g_this_Side, g_this_High);
        //自分のいる縦の位置を−1
        g_this_Ver++;
        

    }

    /// <summary>
    /// 縦軸のマイナス方向の移動
    /// </summary>
    public void Ver_Minus_Move() {
        //移動範囲外の時処理終了
        if (g_this_Ver - g_one_Count < g_zero_Count) {
            return;
        }
        //移動先が埋まっているか調べる
        int type = g_game_Con_Script.Get_Obj_Type(g_this_Ver-1, g_this_Side, g_this_High);
        //移動先が埋まっていた時処理終了
        if (type == 100) {
            return;
        }
        //オブジェクトを回転させる
        g_rotate_Script.Ver_Minus_Rotate();
>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d
        //現在のマス目を保持
        Retention_Squares();
        //マス目入れ替え
        g_squares_Array[2] = g_work_Array[0];
        g_squares_Array[4] = g_work_Array[2];
        g_squares_Array[5] = g_work_Array[4];
        g_squares_Array[0] = g_work_Array[5];
<<<<<<< HEAD
    }
    private void Side_Plus_Squeares_Change() {
        //現在のマス目を保持
        Retention_Squares();
        //マス目入れ替え
        g_squares_Array[3] = g_work_Array[0];
        g_squares_Array[4] = g_work_Array[3];
        g_squares_Array[1] = g_work_Array[4];
        g_squares_Array[0] = g_work_Array[1];
    }
    private void Side_Minus_Squeares_Change() {
        //現在のマス目を保持
        Retention_Squares();
        //マス目入れ替え
        g_squares_Array[0] = g_work_Array[3];
        g_squares_Array[3] = g_work_Array[4];
        g_squares_Array[4] = g_work_Array[1];
        g_squares_Array[1] = g_work_Array[0];
=======

        //このオブジェクトを配列に格納
        g_game_Con_Script.Storage_Obj(g_this_Ver - 1, g_this_Side, g_this_High, this.gameObject);
        g_game_Con_Script.Storage_Obj_Type(g_this_Ver - 1, g_this_Side, g_this_High,100);
        //元々格納されていたところを空にする
        g_game_Con_Script.Storage_Reset(g_this_Ver, g_this_Side, g_this_High);
        //自分のいる縦の位置を−1
        g_this_Ver--;
>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d
    }

    public void Side_Plus_Squears_Change() {
        //現在のマス目を保持
        Retention_Squares();
        //マス目入れ替え
        g_squares_Array[3] = g_work_Array[0];
        g_squares_Array[4] = g_work_Array[3];
        g_squares_Array[1] = g_work_Array[4];
        g_squares_Array[0] = g_work_Array[1];
    }
    public void Side_Minus_Squears_Change() {
        //現在のマス目を保持
        Retention_Squares();
        //マス目入れ替え
        g_squares_Array[0] = g_work_Array[3];
        g_squares_Array[3] = g_work_Array[4];
        g_squares_Array[4] = g_work_Array[1];
        g_squares_Array[1] = g_work_Array[0];
    }



    /// <summary>
    /// 現在のマス目を保持する処理
    /// </summary>
    private void Retention_Squares() {
        //マス目の数の分繰り返す
        for (int pointer = g_zero_Count; pointer < g_squares_Array.Length; pointer++) {
            //保持用配列にマス目の値を保持する
            g_work_Array[pointer] = g_squares_Array[pointer];
        }
    }

    /// <summary>
    /// 全方位のくっつくオブジェクトをチェックする処理
    /// </summary>
    public void All_Check() {
        //移動後に全方位をチェックする
        //くっつくことができるサイコロを探す
        g_check_Script.Check_All_Direction(g_this_Ver, g_this_Side, g_this_High, this.gameObject);
    }

    /// <summary>
    /// 与えられたポインターに対応したマス目の数を返す処理
    /// </summary>
    /// <param name="pointer">マス目配列の指標</param>
    /// <returns></returns>
    public int Get_Array_Squares(int pointer) {
        //マス目の数値を返す
        return g_squares_Array[pointer];
    }

    /// <summary>
    /// 現在の指標を返す処理
    /// </summary>
    /// <returns></returns>
    public (int, int, int) Get_Dice_Pointer() {
        return (g_this_Ver, g_this_Side, g_this_High);
    }

    /// <summary>
    /// 指標を更新する処理
    /// </summary>
    /// <param name="ver">縦</param>
    /// <param name="side">横</param>
    /// <param name="high">高さ</param>
    public void Storage_This_Pointer(int ver,int side,int high) {
        g_this_Ver = ver;
        g_this_Side = side;
        g_this_High = high;
    }
}

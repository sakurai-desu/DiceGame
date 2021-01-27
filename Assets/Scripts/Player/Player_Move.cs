using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {
    private Input_Date g_json_Script;
    private Game_Controller g_game_con_Script;
    private Playercontroller g_play_con_Script;
    private Player_Direction g_direction_Script;
    private Dice_Controller g_dice_con_Script;
    private Player_Animation g_anim_Script;
    private Player_Appearance_Move g_appearance_move_Script;
    private PlayerXbox g_xbox_Script;
    private PlayerDirectXbox g_playerDirectXbox_Script;

    /// <summary>
    /// 初期化用変数
    /// </summary>
    private const int g_zero_Count = 0;
    /// <summary>
    /// 配列の最大値：縦
    /// </summary>
    private int g_max_ver;
    /// <summary>
    /// 配列の最大値：横
    /// </summary>
    private int g_max_side;
    /// <summary>
    /// 配列の最大値：高さ
    /// </summary>
    private int g_max_high;

    /// <summary>
    /// プレイヤーが現在いる位置：縦
    /// </summary>
    private int g_player_ver;
    /// <summary>
    /// プレイヤーが現在いる位置：横
    /// </summary>
    private int g_player_side;
    /// <summary>
    /// プレイヤーが現在いる位置：高さ
    /// </summary>
    private int g_player_high;

    private int g_check_high;
    /// <summary>
    /// 移動可能か判別するためのフラグ・True：移動可能・False：移動不可能
    /// </summary>
    private bool g_is_move = true;
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
    /// プレイヤーオブジェクト
    /// </summary>
    private GameObject g_player_Obj;

    private bool g_player_move = false;
    private bool g_player_auto_move = false;
    [SerializeField]
    /// <summary>
    /// ダイスが長押しで自動移動する間隔
    /// </summary>
    public float g_move_interval = 0.25f;
    [SerializeField]
    /// <summary>
    /// 長押し移動用タイマー
    /// </summary>
    private float g_auto_move_timer = 0;
    [SerializeField]
    /// <summary>
    /// 一度目の移動から何秒後に自動移動を開始するか決める変数
    /// </summary>
    private float g_start_timer = -0.3f;

    void Start() {
        g_player_Obj = this.gameObject;
        g_json_Script = GameObject.Find("Game_Controller").GetComponent<Input_Date>();
        g_game_con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        g_play_con_Script = GameObject.Find("Player_Controller").GetComponent<Playercontroller>();
        g_direction_Script = GameObject.Find("Player_Controller").GetComponent<Player_Direction>();
        g_dice_con_Script = GameObject.Find("Dice_Controller").GetComponent<Dice_Controller>();
        g_appearance_move_Script = this.GetComponent<Player_Appearance_Move>();
        g_anim_Script = this.GetComponent<Player_Animation>();
        g_xbox_Script = this.GetComponent<PlayerXbox>();
        g_playerDirectXbox_Script = this.GetComponent<PlayerDirectXbox>();
        //配列の最大値を取得
        (g_max_ver, g_max_side, g_max_high) = g_json_Script.Get_Array_Max();
    }

    private void Update() {
        //移動開始状態になったとき
        if (g_player_move) {
            //移動開始状態を解除する
            g_player_move = false;
            //一度目から二度目までの移動間隔を初期化
            g_auto_move_timer = g_start_timer;
            //自動移動可能状態にする
            g_player_auto_move = true;
        }
        //自動移動可能状態の時
        if (g_player_auto_move) {
            //タイマーで秒数カウントアップ
            g_auto_move_timer += Time.deltaTime;
            //指定した間隔で処理を行う
            if (g_auto_move_timer >= g_move_interval) {
                //プレイヤーの現在の向きを取得
                int player_direction = g_direction_Script.Get_Player_Direction();
                //取得した方向に移動する
                PlayerMove(player_direction);
                //カウントアップタイマーをリセット
                g_auto_move_timer = g_zero_Count;
            }
        }
        //ボタンから指を離したとき
        if (!g_xbox_Script.Get_Axis_Flag()) {
            //自動移動状態を解除する
            g_player_auto_move = false;
        }
        //if (!g_playerDirectXbox_Script.Get_Direct_Axis_Flag()) {
        //    //自動移動状態を解除する
        //    g_player_auto_move = false;
        //}
    }

    /// <summary>
    /// プレイヤーを移動開始状態にする処理
    /// </summary>
    public void Change_Move_Flag() {
        g_player_move = true;
    }

    /// <summary>
    /// 指定したパラメータと位置に応じてプレイヤーを移動させる
    /// </summary>
    /// <param name="ver">縦</param>
    /// <param name="side">横</param>
    /// <param name="high">高さ</param>
    /// <param name="para">移動方向パラメータ</param>
    public void PlayerMove(int para) {
        if (g_play_con_Script.Get_MoveFlag()) {
            return;
        }
        //プレイヤーの現在のポインター取得
        (g_player_ver, g_player_side, g_player_high) = g_play_con_Script.Get_Player_Pointer();
        //移動前のプレイヤーの向きを取得（ジャンプ判定用）
        int before_direction = g_direction_Script.Get_Player_Direction();
        //プレイヤーオブジェクトの向きをパラメータに応じて変更する
        g_direction_Script.Player_Direction_Change(para);
        //移動後のプレイヤーの向きを取得（ジャンプ判定用）
        int after_direction = g_direction_Script.Get_Player_Direction();
        //移動する向きに応じて処理を変える
        switch (para) {
            //縦プラス方向
            case g_ver_plus_Para:
                g_player_ver = g_player_ver + 1;
                break;
            //縦マイナス方向
            case g_ver_minus_Para:
                g_player_ver = g_player_ver - 1;
                break;
            //横プラス方向
            case g_side_plus_Para:
                g_player_side = g_player_side + 1;
                break;
            //横マイナス方向
            case g_side_minus_Para:
                g_player_side = g_player_side - 1;
                break;
        }
        //移動が可能か調べる
        g_is_move = Move_Check(g_player_ver, g_player_side, g_player_high);
        //移動不可状態なら
        if (!g_is_move) {
            //移動処理中止
            return;
        }
        //移動先に格納されているオブジェクトのタイプ
        int type = g_game_con_Script.Get_Obj_Type(g_player_ver, g_player_side, g_player_high);
        //取得したオブジェクトのタイプに応じて処理
        //空白の時
        //プレイヤーを移動させる
        if (type == 0) {
            //落下可能か調べる
            g_is_move = Fall_Check(g_player_ver, g_player_side, g_player_high);
            //落下不可能の時
            if (!g_is_move) {
                //移動処理中止
                return;
            }
            //移動先が今と同じ高さなら
            if (g_player_high - g_check_high == 0) {
                //移動アニメーション再生
                g_anim_Script.Player_Move_Anim();
            }
            //移動先が自分のいる場所より低い位置なら
            else if (g_player_high - g_check_high != 0) {
                //落下モーションを再生
                g_anim_Script.Player_Jump_Anim();
            }
            //移動先の高さの指標を変更する
            g_player_high = g_check_high;

            //移動先のポジション取得
            Vector3 get_pos = g_game_con_Script.Get_Pos(g_player_ver, g_player_side, g_player_high);
            //取得した位置にプレイヤーを移動させる
            g_appearance_move_Script.Player_Move(get_pos);
            //プレイヤーの現在地を更新する
            g_play_con_Script.Storage_Player_Pointer(g_player_ver, g_player_side, g_player_high);
        }
        //移動先が移動前の向いている方向と同じなら
        //ジャンプするか調べる
        else if (before_direction == after_direction) {
            //ジャンプできるか調べる
            //条件を満たせばジャンプさせる
            Jump();
        }
    }
    /// <summary>
    /// 移動先が配列範囲外もしくは床があるか調べる処理
    /// </summary>
    /// <returns></returns>
    private bool Move_Check(int ver, int side, int high) {
        //移動先が配列の範囲外の時
        if (ver < g_zero_Count || g_max_ver <= ver
            || side < g_zero_Count || g_max_side <= side
                || high < g_zero_Count || g_max_high <= high) {
            //Debug.Log("移動先は範囲外");
            //処理終了
            //移動させない状態を返す
            return false;
        }
        //移動可能
        return true;
    }
    /// <summary>
    /// 落下が可能か調べる処理
    /// </summary>
    /// <param name="ver">縦</param>
    /// <param name="side">横</param>
    /// <param name="high">高さ</param>
    /// <returns></returns>
    private bool Fall_Check(int ver, int side, int high) {
        //落下できるか判定するフラグ
        bool is_fall = true;
        //移動先の一つ下を保持
        g_check_high = high;
        while (true) {
            //移動先の床に格納されているオブジェクトのタイプ
            int ground_type = g_game_con_Script.Get_Obj_Type(ver, side, g_check_high - 1);
            //移動先の床がないとき
            if (ground_type != 0) {
                //落下できる状態にする
                is_fall = true;
                //処理終了
                break;
            }
            //チェックする高さの指標を一つ下にずらす
            g_check_high--;
            //チェックする位置が配列の範囲外になったら終了
            if (g_check_high < 0) {
                //落下できない状態にする
                is_fall = false;
                //処理終了
                break;
            }
        }
        //落下できるかどうかを返す
        return is_fall;
    }

    /// <summary>
    /// プレイヤーをジャンプさせる処理
    /// </summary>
    public void Jump() {

        #region　ジャンプを単体操作する場合に使用するので残している
        //if (g_play_con_Script.Get_MoveFlag()) {
        //    return;
        //}
        //プレイヤーが現在向いている方向を取得
        int direction_para = g_direction_Script.Get_Player_Direction();
        //現在のプレイヤーのポインタ取得
        (g_player_ver, g_player_side, g_player_high) = g_play_con_Script.Get_Player_Pointer();
        int player_just_above = g_game_con_Script.Get_Obj_Type(g_player_ver, g_player_side, g_player_high+1);
        int player_just_above_plusone = g_game_con_Script.Get_Obj_Type(g_player_ver, g_player_side, g_player_high+2);
        //移動する向きに応じて処理を変える
        switch (direction_para) {
            //縦プラス方向
            case g_ver_plus_Para:
                g_player_ver = g_player_ver + 1;
                break;
            //縦マイナス方向
            case g_ver_minus_Para:
                g_player_ver = g_player_ver - 1;
                break;
            //横プラス方向
            case g_side_plus_Para:
                g_player_side = g_player_side + 1;
                break;
            //横マイナス方向
            case g_side_minus_Para:
                g_player_side = g_player_side - 1;
                break;
        }
        #endregion

        //ジャンプ先が移動可能か調べる
        bool is_jump = Move_Check(g_player_ver, g_player_side, g_player_high + 1);
        //ジャンプ不可能な状態の時
        if (!is_jump) {
            //処理中断
            return;
        }
        //ジャンプ先の床に格納されているオブジェクトのタイプ
        int jump_point_ground = g_game_con_Script.Get_Obj_Type(g_player_ver, g_player_side, g_player_high);
        //ジャンプ先に格納されているオブジェクトのタイプ取得
        int jump_point = g_game_con_Script.Get_Obj_Type(g_player_ver, g_player_side, g_player_high + 1);
        int jump_point_plusone = g_game_con_Script.Get_Obj_Type(g_player_ver, g_player_side, g_player_high + 2);
        //ジャンプ先に床が存在する＆ジャンプ先が空白
        if (jump_point_ground != 0 && jump_point == 0&&jump_point_plusone==0&&
            player_just_above==0&&player_just_above_plusone==0) {
            //移動アニメーション再生
            g_anim_Script.Player_Jump_Anim();
            //移動先のポジション取得
            Vector3 get_pos = g_game_con_Script.Get_Pos(g_player_ver, g_player_side, g_player_high + 1);
            //取得した位置にプレイヤーを移動させる
            g_appearance_move_Script.Player_Move(get_pos);
            //プレイヤーの現在地を更新する
            g_play_con_Script.Storage_Player_Pointer(g_player_ver, g_player_side, g_player_high + 1);
        }
    }

    public void Dice_Push() {
        if (g_play_con_Script.Get_MoveFlag()) {
            return;
        }
        //プレイヤーが現在向いている方向を取得
        int direction_para = g_direction_Script.Get_Player_Direction();
        //プレイヤーの現在のポインター取得
        (g_player_ver, g_player_side, g_player_high) = g_play_con_Script.Get_Player_Pointer();
        //検索する向きに応じて処理を変える
        switch (direction_para) {
            //縦プラス方向
            case g_ver_plus_Para:
                g_player_ver = g_player_ver + 1;
                break;
            //縦マイナス方向
            case g_ver_minus_Para:
                g_player_ver = g_player_ver - 1;
                break;
            //横プラス方向
            case g_side_plus_Para:
                g_player_side = g_player_side + 1;
                break;
            //横マイナス方向
            case g_side_minus_Para:
                g_player_side = g_player_side - 1;
                break;
        }
        //検索先に格納されているオブジェクトのタイプ
        int type = g_game_con_Script.Get_Obj_Type(g_player_ver, g_player_side, g_player_high);
        //取得したオブジェクトのタイプに応じて処理
        //ダイスの時
        //ダイスを移動させる
        if (type >= 100) {
            //Debug.Log("ダイスを押す");
            //操作対象のダイスを取得
            GameObject dice_obj = g_game_con_Script.Get_Obj(g_player_ver, g_player_side, g_player_high);
            //取得したダイスを中心に回転させる
            g_dice_con_Script.Storage_Control_Obj(dice_obj, direction_para);
        }
    }
}

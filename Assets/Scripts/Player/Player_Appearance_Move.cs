using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Appearance_Move : MonoBehaviour {
    private Game_Controller g_game_con_Script;
    private Playercontroller g_player_Script;
    /// <summary>
    /// プレイヤーのオブジェクト
    /// </summary>
    private GameObject g_player_obj;
    /// <summary>
    /// 移動先の座標
    /// </summary>
    private Vector3 g_end_Pos;
    /// <summary>
    /// 初期化用変数
    /// </summary>
    private const int g_zero_count = 0;
    /// <summary>
    /// 移動速度の最大値
    /// </summary>
    private const float g_max_speed = 8;
    /// <summary>
    /// 移動速度
    /// </summary>
    private float g_move_speed = 8;
    /// <summary>
    /// 移動速度を加算するための変数
    /// </summary>
    private float g_plus_speed = 0;
    /// <summary>
    /// 移動中か判別するフラグ・True：移動中・False:停止中
    /// </summary>
    private bool g_is_move=false;

    void Start() {
        g_game_con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        g_player_Script = GameObject.Find("Player_Controller").GetComponent<Playercontroller>();
        g_player_obj = this.gameObject;
    }

    /// <summary>
    /// 移動先を変更し移動する
    /// </summary>
    /// <param name="end_Pos"></param>
    public void Player_Move(Vector3 move_Pos) {
        //移動先を取得
        g_end_Pos = move_Pos;
        //プレイヤーを移動させる
        StartCoroutine(Player_Smooth_Move());
    }

    /// <summary>
    /// プレイヤーをなめらかに動かす処理
    /// </summary>
    /// <returns></returns>
    IEnumerator Player_Smooth_Move() {
        //移動中フラグON
        g_is_move = true;
        //速度初期化
        g_move_speed = g_zero_count;
        //速度加算変数初期化
        g_plus_speed = g_zero_count;
        //プレイヤーの位置が移動先につくまで繰り返す
        while (g_player_obj.transform.position != g_end_Pos) {
            //移動速度上げる
            if (g_move_speed < g_max_speed) {
                g_move_speed = g_move_speed + g_plus_speed;
                g_plus_speed = g_plus_speed + 0.2f;
            }
            //移動先に向かって移動
            g_player_obj.transform.position = Vector3.MoveTowards(g_player_obj.transform.position,
              g_end_Pos, g_move_speed * Time.deltaTime);
            yield return null;
        }
        //移動先への移動終了
        g_player_obj.transform.position = g_end_Pos;
        //移動中フラグOFF
        g_is_move = false;
        Goal();
        yield break;
    }
    private void Goal() {
        (int p_ver, int p_side, int p_high) = g_player_Script.Get_Player_Pointer();
        int type = g_game_con_Script.Get_Obj_Type(p_ver, p_side, p_high-1);
        if (type == 20) {
            Debug.Log("ゴールの上");
            SceneManager.LoadScene("SelectScene");
        }
    }
    /// <summary>
    /// 移動中かどうか判別するフラグを返す処理
    /// </summary>
    /// <returns></returns>
    public bool Get_MoveFlag() {
        return g_is_move;
    }
    public void MoveFlag_False() {
        g_is_move = false;
    }
}

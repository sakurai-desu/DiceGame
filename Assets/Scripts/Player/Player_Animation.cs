using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    private Playercontroller g_player_con_Script;
    private Player_Appearance_Move g_move_Script;
    private Animator g_player_Anim;
    [SerializeField]
    private GameObject g_player_obj;
    private bool g_play_flag = false;
    [SerializeField]
    AnimationClip g_move_anim;
    [SerializeField]
    AnimationClip g_jump_anim;

    void Start()
    {
        g_player_con_Script = GameObject.Find("Player_Controller").GetComponent<Playercontroller>();
        g_move_Script = this.GetComponent<Player_Appearance_Move>();
        g_player_Anim = g_player_obj.GetComponent<Animator>();
    }

    void Update() {
        ////移動中フラグ取得
        //g_play_flag = g_player_con_Script.Get_MoveFlag();
        ////停止中のとき
        //if (!g_play_flag) {
        //    g_player_Anim.SetBool("jump_active", false);
        //    g_player_Anim.SetBool("move_active", false);
        //    g_player_Anim.SetBool("roll_active", false);
        //}
    }
    /// <summary>
    /// ジャンプアニメーション
    /// </summary>
    public void Player_Jump_Anim() {
        g_player_Anim.SetBool("jump_active", true);
    }
    /// <summary>
    /// 移動アニメーション
    /// </summary>
    public void Player_Move_Anim() {
        g_player_Anim.SetBool("move_active", true);
    }
    /// <summary>
    /// ダイスを押すアニメーション
    /// </summary>
    public void Player_Roll_Anim() {
        g_player_Anim.SetBool("roll_active", true);
    }

    public void Flag_False() {
        g_player_Anim.SetBool("jump_active", false);
        g_player_Anim.SetBool("move_active", false);
        g_player_Anim.SetBool("roll_active", false);
    }
}

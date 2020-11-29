using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    [SerializeField]
    private GameObject g_player_Obj;
    private Animator g_player_Anim;

    private bool g_play_flag = true;

    void Start()
    {
        g_player_Anim = g_player_Obj.GetComponent<Animator>();
    }

    //void Update()
    //{
    //    if (g_play_flag) {
    //        g_player_Anim.SetBool("jump_active", false);
    //        g_player_Anim.SetBool("move_active", false);
    //        g_player_Anim.SetBool("roll_active", false);
    //        g_play_flag = false;
    //    }

    //    if (Input.GetKeyDown(KeyCode.J)) {
    //        Player_Jump_Anim();
    //    }
    //    if (Input.GetKeyDown(KeyCode.K)) {
    //        Player_Move_Anim();
    //    }
    //    if (Input.GetKeyDown(KeyCode.L)) {
    //        Player_Roll_Anim();
    //    }
    //}

    private void Player_Jump_Anim() {
        g_play_flag = true;
        g_player_Anim.SetBool("jump_active", true);
    }
    private void Player_Move_Anim() {
        g_play_flag = true;
        g_player_Anim.SetBool("move_active", true);
    }
    private void Player_Roll_Anim() {
        g_play_flag = true;
        g_player_Anim.SetBool("roll_active", true);
    }
}

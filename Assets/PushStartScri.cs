using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushStartScri : MonoBehaviour
{
    [SerializeField]
    GameObject g_option_panel=null;

    //オプションのUIを出すアニメ
    Animator g_optionamim;

    /// <summary>
    /// スタートが押されたのを検知するフラグ
    /// </summary>
    public bool g_start_flag;

    void Start()
    {
        g_optionamim = g_option_panel.GetComponent<Animator>();
        g_optionamim.SetBool("StartPushFlag", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start")&&g_start_flag==false) {
            g_optionamim.SetBool("StartPushFlag", true);
            g_start_flag =true;
        }
       else if (Input.GetButtonDown("Start")&&g_start_flag) {
            g_optionamim.SetBool("StartPushFlag", false);
            g_start_flag = false;
        }
    }
}

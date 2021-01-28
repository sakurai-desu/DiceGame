using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushStartScri : MonoBehaviour {
    [SerializeField]
    GameObject g_option_panel = null;
    [SerializeField]
    private GameObject g_controller_guide = null;

    //オプションのUIを出すアニメ
    Animator g_optionamim;

    //効果音データ
    private Se_Source g_se_source_Script;

    /// <summary>
    /// スタートが押されたのを検知するフラグ
    /// </summary>
    public bool g_start_flag;

    void Start() {
        g_se_source_Script = GameObject.Find("SEList").GetComponent<Se_Source>();
        g_optionamim = g_option_panel.GetComponent<Animator>();
        g_optionamim.SetBool("StartPushFlag", false);
    }

    void Update() {
        if (Input.GetButtonDown("Start") && g_start_flag == false) {
            g_controller_guide.SetActive(true);
            g_optionamim.SetBool("StartPushFlag", true);
            g_start_flag = true;
            g_se_source_Script.Se_Play(6);
        } else if (Input.GetButtonDown("Start") && g_start_flag) {
            g_controller_guide.SetActive(false);
            g_optionamim.SetBool("StartPushFlag", false);
            g_start_flag = false;
            g_se_source_Script.Se_Play(7);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetStagename : MonoBehaviour
{
    private StageInformation g_info_Script;
    private Fade_In_Out g_fade_Script;


    public string g_get_stagename = "";

    GameObject g_get_child;

    Text g_stage_text;
    void Start()
    {
        g_info_Script = GameObject.Find("Stageinformation").GetComponent<StageInformation>();
        g_fade_Script = GameObject.Find("Fade_Image").GetComponent<Fade_In_Out>();
    }

    void Update() {
        if (Input.GetButtonDown("Back")||Input.GetKeyDown(KeyCode.Escape)) {
            Move_Title();
        }
    }

    public void OnClick() {
        g_fade_Script.Start_Fade_Out(Move_MainScene());
    }

    private void Move_Title() {
        g_fade_Script.Start_Fade_Out(Move_TitleScene());
    }

    private IEnumerator Move_MainScene() {
        g_info_Script.Change_StageName(g_get_stagename);
        SceneManager.LoadScene("MainScene");
        yield break;
    }

    private IEnumerator Move_TitleScene() {
        SceneManager.LoadScene("TitleScene");
        yield break;
    }

}

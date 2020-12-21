using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetStagename : MonoBehaviour
{
    private StageInformation g_info_Script;
    public string g_get_stagename = "";

    GameObject g_get_child;

    Text g_stage_text;
    void Start()
    {
        g_info_Script = GameObject.Find("Stageinformation").GetComponent<StageInformation>();
    }

    void Update() {
        if (Input.GetButtonDown("Back")||Input.GetKeyDown(KeyCode.Escape)) {
            Move_MainScene();
        }
    }

    public void OnClick() {
        g_info_Script.Change_StageName(g_get_stagename);
        //this.gameObject.transform.root.GetComponent<Stageparameter>().MainScene();
        SceneManager.LoadScene("MainScene");
    }

    private void Move_MainScene() {
        SceneManager.LoadScene("TitleScene");
    }
}

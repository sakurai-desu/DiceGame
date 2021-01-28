using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionText : MonoBehaviour
{
    private Fade_In_Out g_fade_Script;
    private string g_scene_name = null;

    void Start()
    {
        g_fade_Script = GameObject.Find("Fade_Image").GetComponent<Fade_In_Out>();
    }

    public void SelectScene(string scenemane) {
        g_fade_Script.Start_Fade_Out(Move_MainScene());
        g_scene_name = scenemane;
    }

    private IEnumerator Move_MainScene() {
        SceneManager.LoadScene(g_scene_name);
        yield break;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectScript : MonoBehaviour
{
    [SerializeField]
    string g_select_name=null;

    Image g_button=null;

    private Fade_In_Out g_fade_Script;
    private ButtonArray g_button_array_Script;
    private Se_Source g_se_source_Script;

    void Start()
    {
        g_fade_Script = GameObject.Find("Fade_Image").GetComponent<Fade_In_Out>();
        g_button_array_Script = GameObject.Find("ButtonParent").GetComponent<ButtonArray>();
        g_se_source_Script = GameObject.Find("SEList").GetComponent<Se_Source>();
        g_button = GetComponent<Image>();   
    }
    
    public void OcClick() {
        g_button_array_Script.enabled = false;
        g_fade_Script.Start_Fade_Out(Load_Scene());
    }

    private IEnumerator Load_Scene() {
        g_se_source_Script.Se_Play(3);
        SceneManager.LoadScene(g_select_name);
        yield break;
    }

    public void SelectColor() {
        g_button.color = new Color32(255,180,0,255);
    }
    public void DontSelect() {
        g_button.color = Color.white;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectScript : MonoBehaviour
{
    [SerializeField]
    string g_select_name;

    Image g_button;

    private Fade_In_Out g_fade_Script;
    private ButtonArray g_button_array_Script;

    void Start()
    {
        g_fade_Script = GameObject.Find("Fade_Image").GetComponent<Fade_In_Out>();
        g_button_array_Script = GameObject.Find("ButtonParent").GetComponent<ButtonArray>();
        g_button = GetComponent<Image>();   
    }
    
    public void OcClick() {
        g_button_array_Script.enabled = false;
        g_fade_Script.Start_Fade_Out(Load_Scene());
    }

    private IEnumerator Load_Scene() {
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

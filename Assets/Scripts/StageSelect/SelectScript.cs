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
    // Start is called before the first frame update
    void Start()
    {
        g_button = GetComponent<Image>();   
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OcClick() {
        SceneManager.LoadScene(g_select_name);
    }
    public void SelectColor() {
        g_button.color = new Color32(255,180,0,255);
    }
    public void DontSelect() {
        g_button.color = Color.white;
    }
}

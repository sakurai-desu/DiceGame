using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroubleScr : MonoBehaviour
{
    /// <summary>
    /// 手数
    /// </summary>
    [SerializeField]
    int g_troublenum = 10;

    Text g_troublenumtext;
    [SerializeField]
    GameObject g_resetcanvas;
    // Start is called before the first frame update
    void Start()
    {
        g_troublenumtext = GetComponent<Text>();
        g_troublenumtext.text = g_troublenum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 手数をいじる
    /// </summary>
   public  void Trouble() {
        g_troublenum--;
        if (g_troublenum == -1) {
            Debug.Log("ゲームオーバー");
            g_resetcanvas.SetActive(true);

        }
        g_troublenumtext.text = g_troublenum.ToString();

   }
}

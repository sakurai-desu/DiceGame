using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stageparameter : MonoBehaviour
{
    private string[,] g_string_element;

    private GameObject[,] g_button_element;

    /// <summary>
    /// ステージ個数
    /// </summary>
    [SerializeField]
    int g_stage_counter;

    /// <summary>
    /// 横ステージ個数
    /// </summary>
    [SerializeField]
    int g_side_element;

    /// <summary>
    /// 縦ステージ個数
    /// </summary>
    [SerializeField]
    int g_var_element;

    int g_numberCursor;

    public int g_selectNo;


    // Start is called before the first frame update
    void Start()
    {
        //これにbuttonのprefabをいれる配列
        g_button_element = new GameObject[g_var_element, g_side_element];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveRight() {
        if (g_selectNo < (g_button_element.Length - 1)) {
            g_selectNo++;
        }
    }
   
}

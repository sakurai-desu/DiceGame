using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSizeChange : MonoBehaviour
{
    //トランスフォーム
    RectTransform g_transform_button;

    [SerializeField]
    float g_ori_x= 3.4f;
    [SerializeField]
    float g_ori_y= 2.1f;
    // Start is called before the first frame update
    void Awake()
    {
        //自信のトランスフォームを格納
        g_transform_button = GetComponent<RectTransform>();
        g_transform_button.localScale = new Vector3(g_ori_x, g_ori_y, 0.1f);
    }
    private float g_changenum = 1.2f;
    /// <summary>
    /// 自信を大きくするメソッド
    /// </summary>
    public void BigButton() {
        g_transform_button.localScale = new Vector3(g_ori_x *g_changenum, g_ori_y * g_changenum, 0.1f);
    }
    /// <summary>
    /// 自信のサイズを元に戻すメソッド
    /// </summary>
    public void OriginButton() {
        g_transform_button.localScale = new Vector3(g_ori_x, g_ori_y, 0.1f);
    }
}

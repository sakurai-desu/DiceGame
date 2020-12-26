using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSizeChange : MonoBehaviour
{
    //トランスフォーム
    RectTransform g_transform_button;

    //画像データ
    Image g_mySprite;
    [SerializeField]
    Sprite g_unenableImage;
    [SerializeField]
    Sprite g_enableImage;

    [SerializeField]
    float g_ori_x= 1f;
    [SerializeField]
    float g_ori_y= 1f;
    // Start is called before the first frame update
    void Awake()
    {
        //自身のトランスフォームを格納
        g_transform_button = GetComponent<RectTransform>();
        //自身の画像コンポーネントを格納
        g_mySprite = this.gameObject.GetComponent<Image>();
    }
    private void Start() {
        
        g_transform_button.localScale = new Vector3(g_ori_x, g_ori_y, 0.1f);
    }
    private float g_changenum = 1.2f;
    /// <summary>
    /// 自信を大きくするメソッド
    /// </summary>
    public void BigButton() {
        //g_transform_button.localScale = new Vector3(g_ori_x *g_changenum, g_ori_y * g_changenum, 0.1f);

        //画像を差し替え
        g_mySprite.sprite = g_enableImage;
    }
    /// <summary>
    /// 自信のサイズを元に戻すメソッド
    /// </summary>
    public void OriginButton() {
        g_transform_button.localScale = new Vector3(g_ori_x, g_ori_y, 0.1f);

        //画像を元の画像に差し替え
        g_mySprite.sprite = g_unenableImage;
    }
}

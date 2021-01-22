using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Test_Script : MonoBehaviour
{
    private Image g_test_image;
    private string g_folderPath;
    private string[] g_image_files;

    private void Start()
    {
        g_test_image = this.GetComponent<Image>();
        g_folderPath = Application.streamingAssetsPath + "/Image/";
        g_image_files = Directory.GetFiles(g_folderPath,"*.jpg");
        Debug.Log(g_image_files[0]);
        byte[] png = File.ReadAllBytes(g_image_files[0]);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(png);
        Sprite _sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        g_test_image.sprite = _sprite;
    }

    private void Update()
    {
        
    }
}

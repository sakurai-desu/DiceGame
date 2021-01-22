using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Stage_Image : MonoBehaviour
{
    /// <summary>
    /// ステージプレビュー用の画像
    /// </summary>
    private Image g_play_image;
    /// <summary>
    /// 画像が入っているフォルダーのパス
    /// </summary>
    private string g_folderPath;
    /// <summary>
    /// 画像ファイルの名前を格納する配列
    /// </summary>
    private string[] g_image_files;
    [SerializeField]
    /// <summary>
    /// 作成したスプライトを格納する配列
    /// </summary>
    private Sprite[] g_stage_sprites;

    private void Start() {
        //スプライトを作成して配列に格納
        Stage_Sprite_Create();
        //画像のスプライトを変更する
        g_play_image.sprite = g_stage_sprites[0];
    }

    /// <summary>
    /// ストリーミングアセット内の画像をスプライトとして新規作成する
    /// </summary>
    private void Stage_Sprite_Create() {
        //変更する画像取得
        g_play_image = this.GetComponent<Image>();
        //画像のパス取得
        g_folderPath = Application.streamingAssetsPath + "/Image/";
        //取得したパスのフォルダーの中にある画像を取得する
        g_image_files = Directory.GetFiles(g_folderPath, "*.png");
        //画像の枚数に応じて配列を作成
        g_stage_sprites = new Sprite[g_image_files.Length];

        for (int i = 0; i < g_stage_sprites.Length; i++) {
            //バイト配列に変換
            byte[] png = File.ReadAllBytes(g_image_files[i]);
            //テクスチャ生成
            Texture2D tex = new Texture2D(1, 1);
            //テクスチャにデータをロード
            tex.LoadImage(png);
            //テクスチャをつかって新しいスプライトを作成する
            Sprite _sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            g_stage_sprites[i] = _sprite;
        }
    }

    /// <summary>
    /// 与えられたステージの番号に応じて、ステージプレビュー用のスプライトを変更する
    /// </summary>
    /// <param name="_stage_number">ステージの番号</param>
    public void Stage_Sprite_Change(int  _stage_ver,int _stage_side) {
        //選択したステージが一段目の時
        if (_stage_ver > 0) {
            _stage_side = (_stage_ver * 4) + _stage_side + 1;
        } else {
            _stage_side++;
        }
        int _stage_number = _stage_side-1;
        //画像のスプライトを変更する
        g_play_image.sprite = g_stage_sprites[_stage_number];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour {

    GameObject g_arraystats;

    //生成するプレイヤーオブジェクト
    [SerializeField]
    private GameObject g_player;   
    Game_Controller g_arryscript;
    //プレイヤー生成位置
    public int g_playerpointer_v;
    public int g_playerpointer_h;
    public int g_playerpointer_s;

    bool g_arrayflag = false;
    //player位置
    Vector3 g_sponplayer;
    

    // Start is called before the first frame update
    void Start() {
        g_arraystats = GameObject.Find("Game_Controller");
        g_arryscript = g_arraystats.GetComponent<Game_Controller>();

        //配列を一度だけ読み込み用フラグ
        g_arrayflag = true;
    }

    // Update is called once per frame
    void Update() {

        //if (g_arrayflag == true) {
        //    //Testpool.GetArrayから配列読み込み
        //    g_sponplayer = g_arryscript.Get_Pos(g_playerpointer_v, g_playerpointer_s, g_playerpointer_h);
        //    PlayerCreator(g_sponplayer);
        //    g_arrayflag = false;
        //}
    }
    //プレイヤー生成
    public GameObject PlayerCreator(Vector3 g_playerposition) {
        //生成オブジェクト
        GameObject g_playerobj;
        //指定位置に生成
        g_playerobj = Instantiate(g_player, new Vector3(g_playerposition.x,g_playerposition.y+1,g_playerposition.z), Quaternion.identity);

        g_playerpointer_s = (int)g_playerobj.transform.position.x;
        g_playerpointer_v = (int)g_playerobj.transform.position.z;
        g_playerpointer_h = (int)g_playerobj.transform.position.y;
        return g_playerobj;
    }
   
}

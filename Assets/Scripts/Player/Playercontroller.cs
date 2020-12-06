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

    void Start() {
        g_arraystats = GameObject.Find("Game_Controller");
        g_arryscript = g_arraystats.GetComponent<Game_Controller>();

        //配列を一度だけ読み込み用フラグ
        g_arrayflag = true;
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

    public (int, int, int) Get_Player_Pointer() {
        return (g_playerpointer_v, g_playerpointer_s, g_playerpointer_h);
    }
}

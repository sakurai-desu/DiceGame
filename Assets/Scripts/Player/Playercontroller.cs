using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour {
    private Player_Move g_player_move_Script;
    GameObject g_arraystats;

    //生成するプレイヤーオブジェクト
    [SerializeField]
    private GameObject g_player;   
    Game_Controller g_arryscript;
    //プレイヤー生成位置
    public int g_player_pointer_ver;
    public int g_player_pointer_high;
    public int g_player_pointer_side;

    bool g_arrayflag = false;
    //player位置
    Vector3 g_sponplayer;

    void Start() {
        g_arraystats = GameObject.Find("Game_Controller");
        g_arryscript = g_arraystats.GetComponent<Game_Controller>();

        //配列を一度だけ読み込み用フラグ
        g_arrayflag = true;
    }

    private void Update() {
        
    }

    //プレイヤー生成
    public GameObject PlayerCreator(Vector3 g_playerposition) {
        //生成オブジェクト
        GameObject g_playerobj;
        //指定位置に生成
        g_playerobj = Instantiate(g_player, new Vector3(g_playerposition.x,g_playerposition.y+1,g_playerposition.z), Quaternion.identity);

        g_player_pointer_side = (int)g_playerobj.transform.position.x;
        g_player_pointer_ver = (int)g_playerobj.transform.position.z;
        g_player_pointer_high = (int)g_playerobj.transform.position.y;
        return g_playerobj;
    }

    public void Storage_Player_Pointer(int ver,int side,int high) {
        g_player_pointer_ver = ver;
        g_player_pointer_side = side;
        g_player_pointer_high = high;
    }

    public (int, int, int) Get_Player_Pointer() {
        return (g_player_pointer_ver, g_player_pointer_side, g_player_pointer_high);
    }
}

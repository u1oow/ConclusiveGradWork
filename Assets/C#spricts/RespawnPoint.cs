using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{

    public GameObject respawnPoint;
    public Vector3 trans;
    public GameObject playerObject;//プレイヤーオブジェクトを取得する準備
    //private string objName;

    void Start()
    {
        trans = respawnPoint.transform.position;
    }
    public void FallPlayerWarper(Player player)
    {
        Debug.Log("落下してしまいました");
        playerObject.gameObject.transform.position = trans;
        return;
        /*/objName = other.gameObject.name;
        //if関数の中にstringを使う方法は知っているけど、要はないかな。
         if (objName.Equals("player"))
         {
             Debug.Log("落下してしまいました");
             other.gameObject.transform.position = trans;    
         }
         /*/
    }
    

    //落下したかどうかを変数で判定しようとこころむ→結局落下したときにメゾットを呼び出したほうが楽だと気付く
    /*/
    void FixedUpdate()
    {
        if (fall)
        {
            fall = false;
            //Debug.Log("落下してしまいました");
            Player playerScript;
            GameObject obj = GameObject.Find("Player");
            playerScript = obj.GetComponent<Player>();
            playerScript.caurseOut = true;
        }

    }
    //どうしてか上手に行っていないポイント。
    //→今の僕なら言えるけど、.publicGameCbject player player;で忘れずアタッチすればいけそうな気もする
    /*/
}

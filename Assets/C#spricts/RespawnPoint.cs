using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{

    public GameObject respawnPoint;
    private Vector3 trans;
    private bool fall = false;

    // Start is called before the first frame update
    void Start()
    {
        trans = respawnPoint.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        fall = true;
        other.gameObject.transform.position = trans;
    }


    // Update is called once per frame

    //落下したかどうかを変数で判定しようとこころむ・
    void FixedUpdate()
    {
        if (fall)
        {
            fall = false;
            Debug.Log("落下してしまいました");
            Player playerScript;
            GameObject obj = GameObject.Find("Player");
            playerScript = obj.GetComponent<Player>();
            playerScript.caurseOut = true;
        }

    }
    //どうしてか上手に行っていないポイント。
}

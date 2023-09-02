using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{

    public GameObject respawnPoint;
    private Vector3 trans;
    // Start is called before the first frame update
    void Start()
    {
        trans = respawnPoint.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.position = trans;
    }


    // Update is called once per frame
   
    /*/
    void Update()
    {
        transform.Rotate(new Vector3(0,1,0));
    }
    /*/
}

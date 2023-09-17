using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerCheck : MonoBehaviour
{
    [HideInInspector] public bool isOn = false;

    private string playerTag = "Player";

    #region//�v���C���[�ւ̐ڐG����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            isOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            isOn = false;
        }
    }
    #endregion
}
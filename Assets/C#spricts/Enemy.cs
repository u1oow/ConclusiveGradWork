using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("UŒ‚—Í")] private int attackPower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDamage(Player player)
    {
        player.Damage(attackPower);
        //“GÚG‚Ì’Ê’m‚ª—ˆ‚½‚çA©•ª‚ÌUŒ‚—Í‚ğƒvƒŒ[ƒ„[‚É“`‚¦‚é

    }
}

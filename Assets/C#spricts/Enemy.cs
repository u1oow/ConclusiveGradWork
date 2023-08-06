using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("攻撃力")] private int attackPower;

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
        //敵接触の通知が来たら、自分の攻撃力をプレーヤーに伝える

    }
}

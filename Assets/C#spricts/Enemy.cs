using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("攻撃力")] private int attackPower;
    [SerializeField, Header("加算するスコア")] private int myScore;

    public void PlayerDamage(Player player)
    {
        player.Damage(attackPower);
        //敵接触の通知が来たら、自分の攻撃力をプレーヤーに伝える
        
        /*/
        //ダメージを食らったら、その敵（落下）に応じてスコアを加算する
        if(GameManager.instance != null)
        {
            GameManager.instance.score += myScore;
        }
        /*/
    }
}

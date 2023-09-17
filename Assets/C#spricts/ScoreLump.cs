using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLump : MonoBehaviour
{
    private Text scoreText = null;
    private int oldScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        if (GameManager.instance != null)
        {
            if (GameManager.instance.score < 1000)
            {
                oldScore = GameManager.instance.score;
                if (GameManager.instance.score < 10)
                {
                    scoreText.text = "�� �~ 00" + GameManager.instance.score;
                }
                else if (GameManager.instance.score < 100)
                {
                    scoreText.text = "�� �~ 0" + GameManager.instance.score;
                }
                else
                {
                    scoreText.text = "�� �~ " + GameManager.instance.score;
                }

            }
            else
            {
                scoreText.text = "�� �~ 999+";
                GameManager.instance.score = 999;
            }
        }
        else
        {
            Debug.Log("�Q�[���}�l�[�W���[��u���Y��Ă����I�I");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(oldScore != GameManager.instance.score)
        {
            if(GameManager.instance.score < 1000)
            {
                oldScore = GameManager.instance.score;
                if (GameManager.instance.score < 10)
                {
                    scoreText.text = "�� �~ 00" + GameManager.instance.score;
                }
                else if (GameManager.instance.score < 100)
                {
                    scoreText.text = "�� �~ 0" + GameManager.instance.score;
                }
                else 
                {
                    scoreText.text = "�� �~ " + GameManager.instance.score;
                }
                
            }
            else
            {
                scoreText.text = "�� �~ 999+";
                GameManager.instance.score = 999;
            }
        }
        
    }
}

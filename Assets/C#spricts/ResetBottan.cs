using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBottan : MonoBehaviour
{
    [Header("ステージコントローラー")] public GameObject stageControlerObj;

    // Start is called before the first frame update
    public void PressStart()
    {
        Debug.Log("リセットボタンが押されました");
        stageControlerObj.GetComponent<StageControl>().Retry();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

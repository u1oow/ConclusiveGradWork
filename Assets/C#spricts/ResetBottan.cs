using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBottan : MonoBehaviour
{
    [Header("�X�e�[�W�R���g���[���[")] public GameObject stageControlerObj;

    // Start is called before the first frame update
    public void PressStart()
    {
        Debug.Log("���Z�b�g�{�^����������܂���");
        stageControlerObj.GetComponent<StageControl>().Retry();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

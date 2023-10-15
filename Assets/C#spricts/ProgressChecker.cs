using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressChecker : MonoBehaviour
{
    [Header("プレイヤーカメラ位置")] public GameObject locationObj;
    [Header("ゴールポイント位置")] public GameObject goalObj;
    private float xDistance;
    private float firstDistance;
    private float ratioDistance;
    private Vector3 playerCameraPosition;
    private Vector3 goalPosition;
    private Vector3 pinLocalPos;
    private Vector3 firstPinLocalPos;
    private float barLength;


    // Start is called before the first frame update
    void Start()
    {
        Transform myTransform = this.transform;
        playerCameraPosition = locationObj.transform.position;
        goalPosition = goalObj.transform.position;
        xDistance = Mathf.Abs(goalPosition.x - playerCameraPosition.x);
        firstDistance = xDistance;
        //Debug.Log(myTransform.localPosition);
        Vector3 pinLocalPos = myTransform.localPosition;//
        firstPinLocalPos = pinLocalPos;//
        barLength = Mathf.Abs(firstPinLocalPos.x * 2);
        //Debug.Log("xDistance" + xDistance);
        //Debug.Log("firstDistance" + firstDistance);
        //Debug.Log("barLength" + barLength);
    }

    // Update is called once per frame
    void Update()
    {
        ResearchDistance();
        MovePin();
        //Debug.Log(ratioDistance);
    }

    private float ResearchDistance()
    {
        playerCameraPosition = locationObj.transform.position;
        if (!Convert.ToBoolean(xDistance = Mathf.Abs(goalPosition.x - playerCameraPosition.x))) return ratioDistance;

        xDistance = Mathf.Abs(goalPosition.x - playerCameraPosition.x);

        ratioDistance = xDistance / firstDistance;

        return ratioDistance;
    }

    private void MovePin()
    {
        //Debug.Log(firstPinLocalPos.x);
        pinLocalPos.x = barLength * (1 - ratioDistance) - Mathf.Abs(firstPinLocalPos.x);
        this.transform.localPosition = pinLocalPos;
    }


}

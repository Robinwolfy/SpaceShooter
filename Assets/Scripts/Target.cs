using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    int targetMaskFront;
    int targetMaskRight;
    int targetMaskLeft;
    Tracking trackingL;
    Tracking trackingR;
    public GameObject gunR;
    public GameObject gunL;
    

    void Awake()
    {
        targetMaskFront = LayerMask.GetMask("TargetF");
        targetMaskRight = LayerMask.GetMask("TargetR");
        targetMaskLeft = LayerMask.GetMask("TargetL");
        trackingL = gunL.GetComponent<Tracking>();
        trackingR = gunR.GetComponent<Tracking>();
    }

    void Start()
    {
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        Targeting();
    }

    void Targeting()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, targetMaskFront))
        {
            transform.position = hit.point;
            trackingL.locked = true;
            trackingR.locked = true;
        }

        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, targetMaskRight))
        {
            transform.position = hit.point;
            trackingR.locked = true;
            trackingL.locked = false;
        }
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, targetMaskLeft))
        {
            transform.position = hit.point;
            trackingL.locked = true;
            trackingR.locked = false;
        }
    }
}


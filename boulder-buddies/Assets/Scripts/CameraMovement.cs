using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance;

    public Transform middlePoint;
    private Transform trans;
    public float smoothing;
    private Vector3 newPos;

    [HideInInspector] public float yPos;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        trans = GetComponent<Transform>();
        yPos = 3f;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.restarting)
            return;


        //newPos = new Vector3(middlePoint.position.x, 3f, -10f);
        //newPos = new Vector3(Mathf.Lerp(trans.position.x, middlePoint.position.x, smoothing), middlePoint.position.y, -10f);
        newPos = new Vector3(Mathf.Lerp(trans.position.x, middlePoint.position.x, smoothing), Mathf.Lerp(trans.position.y, yPos, smoothing / 10f), -10f);
    }

    private void LateUpdate()
    {
        trans.position = newPos;
    }


    public void SetPosition(float xPos)
    {
        newPos = new Vector3(xPos, 3f, -10f);
        trans.position = new Vector3(xPos, 3f, -10f);
    }
}

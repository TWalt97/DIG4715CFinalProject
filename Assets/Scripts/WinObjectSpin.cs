using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinObjectSpin : MonoBehaviour
{
    public float rotateSpeed = 1f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    void Start()
    {
        posOffset = transform.position;
    }
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.Self);

        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}

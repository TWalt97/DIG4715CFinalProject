using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAiming : MonoBehaviour
{
    public float turnSpeed = 15;
    public float aimDuration = 0.3f;
    public Transform cameraLookAt;
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;

    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);

        cameraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);

        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
    }
}

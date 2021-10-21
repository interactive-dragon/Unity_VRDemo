using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugRotate : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    /*
     * Todo - Figure out why leaving this script on the CameraWrapper object prevents the android build from running
     */
    private void Awake()
    {
        #if !UNITY_EDITOR
            this.gameObject.SetActive(false);
        #endif

    }
    void Update()
    {
        #if UNITY_EDITOR
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        #endif
    }
}

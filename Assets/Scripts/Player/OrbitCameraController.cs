using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCameraController : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private float distance;
    [SerializeField] private float minAngle = -80;
    [SerializeField] private float maxAngle = 80;
    [SerializeField] private float sensitivity = 1;

    private float xAngle;
    private float yAngle;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() 
    {
        xAngle += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        xAngle = Mathf.Clamp(xAngle, minAngle, maxAngle);
        yAngle += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        transform.localEulerAngles = new Vector3(xAngle, yAngle, 0);
        transform.position = parent.position + transform.forward * -distance;
    }
}

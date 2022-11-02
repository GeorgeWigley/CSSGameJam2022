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

    [Header("Collisions")]
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float collisionBuffer = 1;
    [SerializeField] private float minDistance = 2;

    private float xAngle;
    private float yAngle;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate() 
    {
        xAngle += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        xAngle = Mathf.Clamp(xAngle, minAngle, maxAngle);
        yAngle += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        float currentDistance = distance;
        transform.localEulerAngles = new Vector3(xAngle, yAngle, 0);
        Vector3 newPos = parent.position + transform.forward * -distance;
        if (Physics.Linecast(parent.position + transform.forward * -minDistance, newPos, out RaycastHit hit, collisionMask))
        {
            currentDistance = hit.distance - collisionBuffer;
        }
        transform.position = parent.position + transform.forward * -currentDistance;
    }
}

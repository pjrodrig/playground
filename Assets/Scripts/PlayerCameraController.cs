using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 1, -2);
    public Vector3 rotationOffset = new Vector3();
    public float cameraSensitivity = 1f;

    Vector3 destination = Vector3.zero;

    void Start() {
        SetCameraTarget(target);
    }

    void SetCameraTarget(Transform t) {
        target = t;
    }

    void LateUpdate() {
        FollowTarget();
    }
 
    void FollowTarget() {
        //TODO: limit veritcal rotation
        float horizontalInput = Input.GetAxis("RHorizontal");
        float verticalInput = Input.GetAxis("RVertical");
        Quaternion horizontalRotation = Quaternion.AngleAxis(horizontalInput * cameraSensitivity, Vector3.up);
        Quaternion verticalRotation = Quaternion.AngleAxis(-1f * verticalInput * cameraSensitivity, transform.right);
        offset =  horizontalRotation * verticalRotation * offset;
        transform.position = target.position + offset;
        transform.LookAt(target);
        
    }
}

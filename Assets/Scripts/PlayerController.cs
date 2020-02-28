using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;
    public float velocity = 2;

    Rigidbody rigidBody;
    Animator animator;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
    }

    void FixedUpdate() {
        Move();
    }

    void Move() {
        float forwardMotion = Input.GetAxis("Vertical");
        float rightMotion = Input.GetAxis("Horizontal");
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 motion = (forward * forwardMotion) + (cameraTransform.right * rightMotion);
        if(!motion.Equals(Vector3.zero)) {
            transform.rotation = Quaternion.LookRotation(motion, Vector3.up);
        }
        motion = motion * velocity;
        animator.SetFloat("Movement Speed", motion.magnitude);
        Debug.Log(motion.magnitude);
        //maintain veritcal motion
        motion.y = rigidBody.velocity.y;
        rigidBody.velocity = motion;
    }
}

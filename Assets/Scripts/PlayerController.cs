using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;
    public float RUN_SPEED = 2f;
    public float JUMP_SPEED = 4f;

    Rigidbody rigidBody;
    Animator animator;
    CapsuleCollider collider;
    
    bool isGrounded;
    float startJump = -1f;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        Jump(isGrounded);
    }

    void OnCollisionEnter(Collision theCollision)
 {
     if (theCollision.gameObject.name == "Terrain")
     {
         isGrounded = true;
     }
 }
 
    void OnCollisionExit(Collision theCollision)
    {
        if (theCollision.gameObject.name == "Terrain")
        {
            isGrounded = false;
        }
    }


    void FixedUpdate() {
        Move(isGrounded);
        UpdateStates(isGrounded);
    }

    void UpdateStates(bool isGrounded) {

    }

    void Look() {

    }

    void Move(bool isGrounded) {
        float forwardMotion = Input.GetAxis("Vertical");
        float rightMotion = Input.GetAxis("Horizontal");
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 motion = (forward * forwardMotion) + (cameraTransform.right * rightMotion);
        if(!motion.Equals(Vector3.zero)) {
            transform.rotation = Quaternion.LookRotation(motion, Vector3.up);
        }
        //only apply force if grounded
        if(isGrounded) {
            motion = motion * RUN_SPEED;
            animator.SetFloat("Movement", motion.magnitude);
            //maintain veritcal motion
            motion.y = rigidBody.velocity.y;
            rigidBody.velocity = motion;
        }
    }

    void Jump(bool isGrounded) {
        if(isGrounded) {
            if(Input.GetButtonUp("Jump")) {
                Vector3 jumpVelocity = rigidBody.velocity;
                jumpVelocity.y = JUMP_SPEED;
                Debug.Log(Time.time - startJump);
                rigidBody.velocity = jumpVelocity * Mathf.Min(2f, (1 + (Time.time - startJump)/ 10));
                startJump = -1;
            } else if(Input.GetButtonDown("Jump") && startJump == -1f) {
                startJump = Time.time;
                Debug.Log(startJump);
            }
        }
    }

    void Fly() {
        
    }
}

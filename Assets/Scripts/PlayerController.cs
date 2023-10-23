using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;
    private Rigidbody rigidBody => GetComponent<Rigidbody>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        bool isWalking = (horizontalInput != 0f || verticalInput != 0f);

        if (isWalking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(horizontalInput, 0f, verticalInput));
            rigidBody.MoveRotation(Quaternion.Slerp(rigidBody.rotation, targetRotation, Time.deltaTime * 14f));
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        animator.SetFloat("WalkMultiplier", rigidBody.velocity.magnitude * 0.4f);

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, new Vector3(movement.x, rigidBody.velocity.y, movement.z).normalized * moveSpeed, Time.deltaTime * 15f);
    }
}

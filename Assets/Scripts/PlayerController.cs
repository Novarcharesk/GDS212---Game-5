using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;
    private Rigidbody rigidBody => GetComponent<Rigidbody>();

    public GameObject pendingObject;
    public GameObject heldObject;

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
        rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, new Vector3(movement.x, 0, movement.z).normalized * moveSpeed, Time.deltaTime * 15f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pendingObject != null)
            {
                heldObject = pendingObject;
            }
            else
            {
                heldObject = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Atom"))
        {
            pendingObject = other.gameObject;
        }
    }

    private void OnTriggerLeave(Collider other)
    {
        if (other.CompareTag("Atom") && other.gameObject == pendingObject)
        {
            pendingObject = null;
            Debug.Log("Leaving " + other.gameObject.name + "and setting pendingObject to null");
        }
        else
        {
            Debug.Log("Leaving " + other.gameObject.name + "but not setting pendingObject to null");
        }
    }
}

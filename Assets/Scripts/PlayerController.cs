using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [Header("References")]
    [SerializeField] private Animator animator;
    public Transform pickupTargetSmall;
    public Transform pickupTargetMedium;
    public Transform pickupTargetLarge;
    public Transform pickupTargetVeryLarge;

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
                // checking if still inside collider (this check usually always passes)
                bool insideCollider = false;
                for (int i = 0; i < pendingObject.GetComponentsInChildren<Collider>().Length; i++)
                {
                    if (pendingObject.GetComponentsInChildren<Collider>()[i].bounds.Contains(transform.position))
                    {
                        Debug.Log("Picking up " + pendingObject.name);
                        PickupObject(pendingObject);
                        insideCollider = true;
                        break;
                    }
                }
                if (!insideCollider)
                {
                    Debug.Log("Not inside collider of " + pendingObject.name + " so setting pendingObject to null");
                    pendingObject = null;
                }
            }
            else if (heldObject != null)
            {
                // drop the object here
                Debug.Log("Dropping " + heldObject.name);
                heldObject = null;
            }
            else
            {
                // check for atoms in range
                Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Atom"))
                    {
                        Debug.Log("Picking up " + collider.gameObject.name);
                        PickupObject(collider.gameObject);
                        break;
                    }
                }
            }
        }
    }

    private void PickupObject(GameObject obj)
    {
        Debug.Log("Picking up " + obj.name);
        heldObject = obj;
        pendingObject = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Atom") && heldObject == null)
        {
            pendingObject = other.gameObject;
            Debug.Log("Entering " + other.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Atom") && other.gameObject == pendingObject)
        {
            pendingObject = null;
            Debug.Log("Leaving " + other.gameObject.name + " and setting pendingObject to null");
        }
    }
}

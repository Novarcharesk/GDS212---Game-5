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
    private List<GameObject> pendingObjects = new List<GameObject>();
    private GameObject heldObject;

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
            if (pendingObjects.Count > 0)
            {
                // find the object closest to the pickup small transform
                GameObject closestObject = null;
                float closestDistance = float.MaxValue;
                foreach (GameObject obj in pendingObjects)
                {
                    float distance = Vector3.Distance(obj.transform.position, pickupTargetSmall.position);
                    if (distance < closestDistance)
                    {
                        closestObject = obj;
                        closestDistance = distance;
                    }
                }
                if (closestObject != null)
                {
                    bool insideCollider = false;
                    Collider[] colliders = Physics.OverlapSphere(transform.position, 0.25f);
                    foreach (Collider collider in colliders)
                    {
                        if (collider.gameObject == closestObject)
                        {
                            PickupObject(closestObject);
                            insideCollider = true;
                            break;
                        }
                    }
                    if (!insideCollider)
                    {
                        Debug.Log("Not inside collider so clearing pendingObjects");
                        pendingObjects.Clear();

                    }
                }
            }
            else if (heldObject != null)
            {
                // drop the object
                DropObject(heldObject);
            }
            else
            {
                // check for atoms in range
                Collider[] colliders = Physics.OverlapSphere(pickupTargetSmall.position, 0.25f);
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Atom"))
                    {
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
        obj.GetComponent<AtomManager>().PickupAtom();
        heldObject = obj;
        pendingObjects.Clear();
    }

    private void DropObject(GameObject obj)
    {
        Debug.Log("Dropping " + obj.name);
        obj.GetComponent<AtomManager>().DropAtom();
        heldObject = null;
        pendingObjects.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Atom") && heldObject == null)
        {
            pendingObjects.Add(other.gameObject);
            other.gameObject.GetComponent<AtomManager>().debugActive = true;
            Debug.Log("Entering " + other.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Atom"))
        {
            pendingObjects.Remove(other.gameObject);
            other.gameObject.GetComponent<AtomManager>().debugActive = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float yPosition = 5f;
    [SerializeField] private float gridSize = 3f;
    [SerializeField] private AtomSize atomSize = AtomSize.Small;
    private Rigidbody rigidBody => GetComponent<Rigidbody>();
    private PlayerController playerController => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    private HingeJoint hingeJointToPlayer;

    public bool debugActive;

    // Start is called before the first frame update
    void Start()
    {
        SnapToGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickupAtom()
    {
        switch (atomSize)
        {
            case AtomSize.Small:
                transform.position = playerController.pickupTargetSmall.position;
                break;
            case AtomSize.Medium:
                transform.position = playerController.pickupTargetMedium.position;
                break;
            case AtomSize.Large:
                transform.position = playerController.pickupTargetLarge.position;
                break;
            case AtomSize.VeryLarge:
                transform.position = playerController.pickupTargetVeryLarge.position;
                break;
        }
        // set rotation to playerController.pickupTargetSmall.rotation + 0, 90, 180, 270 depending on the current rotation
        float rotationOffset = Mathf.Round(playerController.transform.rotation.eulerAngles.y / 90) * 90 - playerController.transform.rotation.eulerAngles.y;
        Debug.Log(rotationOffset);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y - rotationOffset, 0);
        rigidBody.isKinematic = false;
        if (hingeJointToPlayer != null)
        {
            Destroy(hingeJointToPlayer);
            hingeJointToPlayer = null;
        }
        hingeJointToPlayer = gameObject.AddComponent<HingeJoint>();
        hingeJointToPlayer.connectedBody = playerController.GetComponent<Rigidbody>();
        hingeJointToPlayer.axis = new Vector3(0, 0, 1);
        hingeJointToPlayer.useSpring = true;
        hingeJointToPlayer.spring = new JointSpring() { spring = 10, damper = 1 };
        hingeJointToPlayer.useLimits = true;
        hingeJointToPlayer.limits = new JointLimits() { min = -10, max = 10 };
    }

    public void DropAtom()
    {
        Destroy(hingeJointToPlayer);
        hingeJointToPlayer = null;
        rigidBody.isKinematic = true;
        SnapToGrid();
    }

    private void SnapToGrid()
    {
        float x = Mathf.Round(transform.position.x / gridSize) * gridSize;
        float z = Mathf.Round(transform.position.z / gridSize) * gridSize;
        transform.position = new Vector3(x, yPosition, z);
        float y = Mathf.Round(transform.rotation.eulerAngles.y / 90) * 90;
        transform.rotation = Quaternion.Euler(0, y, 0);
    }

    public enum AtomSize
    {
        Small,
        Medium,
        Large,
        VeryLarge
    }

    private void OnDrawGizmos()
    {
        if (debugActive)
        {
            Gizmos.color = new Color(0, 1, 0, 0.35f);
        }
        else
        {
            Gizmos.color = new Color(1, 0, 0, 0.35f);
        }
        switch (atomSize)
        {
            case AtomSize.Small:
                Gizmos.DrawSphere(new Vector3(transform.position.x, 1, transform.position.z), 5.5f);
                break;
            case AtomSize.Medium:
                Gizmos.DrawSphere(new Vector3(transform.position.x, 1, transform.position.z), 6f);
                break;
            case AtomSize.Large:
                Gizmos.DrawSphere(new Vector3(transform.position.x, 1, transform.position.z), 7f);
                break;
            case AtomSize.VeryLarge:
                Gizmos.DrawSphere(new Vector3(transform.position.x, 1, transform.position.z), 8.25f);
                break;
        }
    }
}
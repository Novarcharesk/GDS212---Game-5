using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float yPosition = 5f;
    [SerializeField] private AtomSize atomSize = AtomSize.Small;
    private Rigidbody rigidBody => GetComponent<Rigidbody>();
    private PlayerController playerController => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    private HingeJoint hingeJointToPlayer;

    public bool debugActive;

    // Start is called before the first frame update
    void Start()
    {

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
                transform.rotation = playerController.pickupTargetSmall.rotation;
                break;
            case AtomSize.Medium:
                transform.position = playerController.pickupTargetMedium.position;
                transform.rotation = playerController.pickupTargetMedium.rotation;
                break;
            case AtomSize.Large:
                transform.position = playerController.pickupTargetLarge.position;
                transform.rotation = playerController.pickupTargetLarge.rotation;
                break;
            case AtomSize.VeryLarge:
                transform.position = playerController.pickupTargetVeryLarge.position;
                transform.rotation = playerController.pickupTargetVeryLarge.rotation;
                break;
        }
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
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
        transform.rotation = Quaternion.identity;
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
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
        HingeJoint hingeJoint = gameObject.AddComponent<HingeJoint>();
        hingeJoint.connectedBody = playerController.GetComponent<Rigidbody>();
        hingeJoint.axis = new Vector3(0, 0, 1);
        hingeJoint.useSpring = true;
        hingeJoint.spring = new JointSpring() { spring = 10, damper = 1 };
        hingeJoint.useLimits = true;
        hingeJoint.limits = new JointLimits() { min = -10, max = 10 };
    }

    public void DropAtom()
    {
        Destroy(GetComponent<HingeJoint>());
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
}
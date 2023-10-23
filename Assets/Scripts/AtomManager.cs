using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomManager : MonoBehaviour
{
    private Rigidbody rigidBody => GetComponent<Rigidbody>();

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetKinematic(bool isKinematic)
    {
        rigidBody.isKinematic = isKinematic;
    }
}

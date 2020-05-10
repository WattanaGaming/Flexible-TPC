using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Example script to make the character follow an object
public class TPCAI : MonoBehaviour
{
    public Transform objectToFollow;

    private ThirdPersonController tpc;

    // Start is called before the first frame update
    void Start()
    {
        tpc = this.GetComponent<ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 objectRelative = objectToFollow.InverseTransformPoint(this.transform.position);
        Vector3 targetPosition = new Vector3(-objectRelative.x, 0f, -objectRelative.z);
        tpc.desiredMovementDirection = targetPosition;
    }
}

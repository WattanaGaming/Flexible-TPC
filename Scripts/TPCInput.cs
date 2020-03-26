using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic input script
// Have fun with the dependencies in the main controller lel
[RequireComponent(typeof(ThirdPersonController))]
public class TPCInput : MonoBehaviour
{
    public string horizontalInputName = "Horizontal";
    public string verticalInputName = "Vertical";

    private ThirdPersonController tpc;
    private Camera mainCamera;
    private float InputX;
    private float InputY;

    // Start is called before the first frame update
    void Start()
    {
        tpc = this.GetComponent<ThirdPersonController>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        InputX = Input.GetAxis(horizontalInputName);
        InputY = Input.GetAxis(verticalInputName);

        Vector3 camForward = mainCamera.transform.forward;
        Vector3 camRight = mainCamera.transform.right;

        camForward.y = camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        tpc.desiredMovementDirection = camForward * InputY + camRight * InputX;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [Header("General settings")]
    public Transform target;
    public Vector3 targetOffset;
    public bool isActive = true;

    [Header("Camera and movement settings")]
    public float cameraDistance = 5f;
    public float cameraPadding = 0.1f;
    public Vector2 mouseSensitivity = new Vector2(2f, 1f);
    public float orbitSpeedMultiplier = 1f;
    public LayerMask collisionMask;

    [Header("Input settings")]
    public string mouseX = "Mouse X";
    public string mouseY = "Mouse Y";

    private Transform pivot;
    private Vector2 mouseVector;
    private Vector2 mouseDelta;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            Debug.LogError("The target transform is not set. Camera controller disabled");
            isActive = false;
            return;
        }
        else if (target == this.transform)
        {
            Debug.LogError("The target transform should not be the camera itself. Camera controller disabled");
            isActive = false;
            return;
        }
        
        Debug.Log("Creating a pivot GameObject");
        pivot = new GameObject().transform;
        pivot.name = "Pivot";
        Debug.Log("Setting the new GameObject as the parent of the camera");
        transform.parent = pivot;
    }

    // Update is called once per frame
    void Update()
    {
        mouseDelta = new Vector2(Input.GetAxis(mouseX), Input.GetAxis(mouseY));
        mouseDelta.Scale(mouseSensitivity * orbitSpeedMultiplier);

        mouseVector += mouseDelta;
        mouseVector.y = Mathf.Clamp(mouseVector.y, -90f, 90f);

        if (isActive)
        {
            pivot.position = target.position + targetOffset;
            pivot.eulerAngles = new Vector3(-mouseVector.y, mouseVector.x, 0f);

            AdjustCameraDistance();
        }
    }

    void AdjustCameraDistance()
    {
        transform.localPosition = new Vector3(0f, 0f, -cameraDistance + cameraPadding);

        RaycastHit hit;
        if (Physics.Raycast(pivot.position, -pivot.forward, out hit, Mathf.Infinity, collisionMask))
        {
            if (hit.distance < cameraDistance)
            {
                transform.localPosition = new Vector3(0f, 0f, -hit.distance + cameraPadding);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        if (target != null)
        {
            Gizmos.DrawSphere(target.position + targetOffset, 0.25f);
            Gizmos.DrawWireSphere(target.position + targetOffset, cameraDistance);
        }
    }
}

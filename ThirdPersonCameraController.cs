using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [Header("General settings")]
    public Transform pivot;
    public float cameraDistance = 5f;
    public float orbitSpeedMultiplier = 1.5f;
    public LayerMask collisionMask;
    public bool isActive = true;

    [Header("Input settings")]
    public string mouseX = "Mouse X";
    public string mouseY = "Mouse Y";

    private Vector2 mouseVector;
    
    // Start is called before the first frame update
    void Start()
    {
        if (pivot == null)
        {
            Debug.LogError("The pivot point is not set. Please assign a pivot point before using this camera(Controller is now disabled).");
            isActive = false;
        }
        else if (transform.parent != pivot)
        {
            Debug.LogWarning("The pivot point is not the parent of this camera. Setting the pivot point as the parent object.");
            transform.parent = pivot;
        }
    }

    // Update is called once per frame
    void Update()
    {
        mouseVector.x += Input.GetAxis(mouseX) * orbitSpeedMultiplier * 1.5f;
        mouseVector.y += -Input.GetAxis(mouseY) * orbitSpeedMultiplier;
        mouseVector.y = Mathf.Clamp(mouseVector.y, -90f, 90f);

        if (isActive)
        {
            RotatePivot();
            AdjustCameraDistance();
        }
    }

    void RotatePivot()
    {
        pivot.eulerAngles = new Vector3(mouseVector.y, mouseVector.x, 0f);
    }

    void AdjustCameraDistance()
    {
        transform.localPosition = new Vector3(0f, 0f, -cameraDistance);

        RaycastHit hit;
        if (Physics.Raycast(pivot.position, -pivot.forward, out hit, Mathf.Infinity, collisionMask))
        {
            if (hit.distance < cameraDistance)
            {
                transform.localPosition = new Vector3(0f, 0f, -hit.distance);
            }
        }
    }
}

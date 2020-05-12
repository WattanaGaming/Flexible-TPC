using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Not-so-basic input script
// Have fun with the dependencies in the main controller lel
[RequireComponent(typeof(ThirdPersonController))]
public class ThirdPersonInput : MonoBehaviour
{
    public string horizontalInputName = "Horizontal";
    public string verticalInputName = "Vertical";

    public ActionMap[] actionMaps;

    private ThirdPersonController tpController;
    private Transform mainCameraTransform;
    private float InputX;
    private float InputY;

    // Start is called before the first frame update
    void Start()
    {
        tpController = this.GetComponent<ThirdPersonController>();
        mainCameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        InputX = Input.GetAxis(horizontalInputName);
        InputY = Input.GetAxis(verticalInputName);

        Vector3 camForward = mainCameraTransform.forward;
        Vector3 camRight = mainCameraTransform.right;

        camForward.y = camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        tpController.desiredMovementDirection = camForward * InputY + camRight * InputX;

        ProcessActionMaps();
    }

    void ProcessActionMaps()
    {
        foreach (ActionMap actionMap in actionMaps)
        {
            switch (actionMap.activationType)
            {
                case ActivationType.KeyDown:
                    if (Input.GetKeyDown(actionMap.activationKey))
                    {
                        tpController.characterAnimator.SetTrigger(actionMap.animatorParameter);
                    }
                    break;
                case ActivationType.KeyUp:
                    if (Input.GetKeyUp(actionMap.activationKey))
                    {
                        tpController.characterAnimator.SetTrigger(actionMap.animatorParameter);
                    }
                    break;
                case ActivationType.Hold:
                    tpController.characterAnimator.SetBool(actionMap.animatorParameter, Input.GetKey(actionMap.activationKey));
                    break;
                default:
                    Debug.LogWarning("The activation type is unknown. Something is wrong, I can feel it.");
                    break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Oh? You're approaching me? Instead of running away, you're coming right to me?
Even Though your first assets imported, Standard Assets, told you the secret of CSharp,
*dead standard asset scripts* like an exam student scrambling to finish the problems
on an exam until the last moments before the chime?
*/
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    [Header("Movement settings")]
    public float rotationSpeed = 0.1f;
    [Tooltip("Specify the minimum input magnitude required for the character to start rotating.")]
    public float allowRotation;

    [Header("Animator parameters")]
    public string inputMagnitudeParameter = "InputMagnitude";

    // Receive inputs from a separate script
    [HideInInspector] public Vector3 desiredMovementDirection;

    [HideInInspector] public Animator characterAnimator; // This one is made public for external control
    private CharacterController characterController;
    private float inputMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = this.GetComponent<Animator>();
        characterController = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Make the movement FPS-independent.
        inputMagnitude = desiredMovementDirection.sqrMagnitude;
        characterAnimator.SetFloat(inputMagnitudeParameter, inputMagnitude, 0f, Time.deltaTime);

        if (inputMagnitude > allowRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMovementDirection), rotationSpeed);
        }
    }
}

[System.Serializable]
public struct ActionMap
{
    public string animatorParameter;
    public KeyCode activationKey; // This is only used by the input script. It's up to the developer to decide if they want to use this or not.
    public ActivationType activationType;
}

public enum ActivationType
{
    KeyDown,
    KeyUp,
    Hold
}
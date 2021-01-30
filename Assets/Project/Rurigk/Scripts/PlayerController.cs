using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    //public event Action<PlayerInput> onControlsChanged;

    CharacterController characterController;
    public TextMeshProUGUI debugText;

    // Inputs actions
    private InputAction inputLookAction;
    private InputAction inputMoveAction;

    [Header("Movement")]
    Vector3 playerVelocity;
    public Vector2 movementSpeed;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public float groundDetectionDistance = 0.1f;

    [Header("Camera")]
    public GameObject cameraPivot;
    public GameObject cameraObject;
    public float cameraMaximumUpRotation = 80;
    public float cameraMaximumDownRotation = 80;
    public float maxCameraDistance = 1;
    public Vector2 lookSpeed;
    public Vector2 gamepadLookSpeed;
    Vector2 currentLookSpeed;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        /*onControlsChanged += OnControllsChanged;
        playerInput.onControlsChanged += onControlsChanged;*/
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
            inputLookAction = playerInput.actions["Look"];
            inputMoveAction = playerInput.actions["Move"];
        }

        if ((characterController.isGrounded) && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Move
        Vector2 moveAxis = inputMoveAction.ReadValue<Vector2>();
        float mxDelta = Time.deltaTime * (moveAxis.x * movementSpeed.x);
        float myDelta = Time.deltaTime * (moveAxis.y * movementSpeed.y);
        characterController.Move(transform.forward * myDelta);
        characterController.Move(transform.right * mxDelta);

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        // Rotate
        Vector2 lookAxis = inputLookAction.ReadValue<Vector2>();
        if(playerInput.currentControlScheme == "Gamepad")
		{
            currentLookSpeed = gamepadLookSpeed;
        }
        else
		{
            currentLookSpeed = lookSpeed;
        }
        float vxDelta = Time.deltaTime * (lookAxis.x * currentLookSpeed.x);
        float vyDelta = Time.deltaTime * (lookAxis.y * currentLookSpeed.y);
        transform.Rotate(0f, (currentLookSpeed.x * vxDelta), 0f);

        float cameraVerticalRotation = cameraPivot.transform.localRotation.eulerAngles.x - (vyDelta * currentLookSpeed.y);
        cameraPivot.transform.localRotation = Quaternion.Euler(ClampAngle(cameraVerticalRotation, cameraMaximumDownRotation, cameraMaximumUpRotation), 0f, 0f);

        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;

        if (Physics.Raycast(cameraPivot.transform.position, cameraPivot.transform.TransformDirection(Vector3.forward * -1), out hit, maxCameraDistance, layerMask))
        {
            cameraObject.transform.position = cameraPivot.transform.position + -cameraPivot.transform.forward * (hit.distance - 0.1f);
        }
        else
        {
            cameraObject.transform.position = cameraPivot.transform.position + -cameraPivot.transform.forward * (maxCameraDistance - 0.1f);
        }

        cameraObject.transform.LookAt(cameraPivot.transform);

    }

    void OnControllsChanged(PlayerInput input)
	{
        if(debugText)
            debugText.text = input.currentControlScheme;
	}

    public void OnJump(InputValue input)
	{
        // Jump
        if (IsGrounded() || characterController.isGrounded)
        {
            Debug.Log("Jump");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }

    bool IsGrounded()
	{
        RaycastHit hit;

        if (Physics.Raycast(transform.position + new Vector3(0, 0.02f,0), transform.TransformDirection(Vector3.up * -1), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position + new Vector3(0, 0.02f, 0), hit.point, Color.red);
            if (hit.distance < groundDetectionDistance)
			{
                return true;
			}
        }
        return false;
	}

    float ClampAngle(float angle, float min, float max)
    {
        angle = NormalizeAngle(angle);
        angle = angle > 180 ? angle - 360 : (angle < -180 ? angle += 360 : angle);

        min = NormalizeAngle(min);
        min = min > 180 ? min - 360 : (min < -180 ? min += 360 : min);

        max = NormalizeAngle(max);
        max = max > 180 ? max - 360 : (max < -180 ? max += 360 : max);

        return Mathf.Clamp(angle, min, max);
    }

    protected float NormalizeAngle(float angle)
    {
        return angle = (angle % 360 + 360) % 360; ;
    }
}

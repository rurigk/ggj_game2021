using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    PlayerInput playerInput;
    //public event Action<PlayerInput> onControlsChanged;

    CharacterController characterController;
    public TextMeshProUGUI debugText;

    // Inputs actions
    private InputAction inputLookAction;
    private InputAction inputMoveAction;

    [Header("Audio Manager")]
    public PlayerAudioManager audioManager;

    [Header("Movement")]
    Vector3 playerVelocity;
    public Vector2 movementSpeed;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public float groundDetectionDistance = 0.1f;

    bool ceilDetectionLocked = false;

    [Header("Camera")]
    public GameObject cameraPivot;
    public GameObject cameraPivotInternal;
    public GameObject cameraObject;
    public float cameraMaximumUpRotation = 80;
    public float cameraMaximumDownRotation = 80;
    public float maxCameraDistance = 1;
    public float maxInternalPivotDistance = 1.2f;
    public Vector2 lookSpeed;
    public Vector2 gamepadLookSpeed;
    Vector2 currentLookSpeed;

    [Header("Model Tilt Effect")]
    public float maxTilt = 10;
    public Transform tiltXAxis;
    public Transform tiltYAxis;
    Vector2 tiltVector = new Vector2();

    [Header("Shoot settings")]
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Animator characterAnimator;
    bool shootLock = false;

    [Header("Player lock")]
    bool playerLocked = false;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!Instance)
        {
            Instance = this;
        }
        characterController = GetComponent<CharacterController>();
        audioManager = GetComponent<PlayerAudioManager>();
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

        // Ceil detection
        bool characterCeiled = IsCharacterControllerCeiled();
        if (!ceilDetectionLocked && characterCeiled && playerVelocity.y > 0)
        {
            playerVelocity.y = 0f;
            ceilDetectionLocked = true;
        }
        else if(ceilDetectionLocked && !characterCeiled)
		{
            ceilDetectionLocked = false;
        }


        if(!playerLocked)
		{
            // Move
            Vector2 moveAxis = inputMoveAction.ReadValue<Vector2>();
            float mxDelta = Time.deltaTime * (moveAxis.x * movementSpeed.x);
            float myDelta = Time.deltaTime * (moveAxis.y * movementSpeed.y);
            characterController.Move(transform.forward * myDelta);
            characterController.Move(transform.right * mxDelta);

            playerVelocity.y += gravityValue * Time.deltaTime;

            if (IsCharacterControllerGrounded() && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            characterController.Move(playerVelocity * Time.deltaTime);

            // Tilt

            tiltVector = Vector2.Lerp(tiltVector, moveAxis, Time.deltaTime * 10);

            tiltXAxis.localRotation = Quaternion.Euler(0f, 0f, maxTilt * -tiltVector.x);
            tiltYAxis.localRotation = Quaternion.Euler(maxTilt * tiltVector.y, 0f, 0f);

            // Audio volume by movement

            /*float deltaX = moveAxis.x < 0 ? -moveAxis.x : moveAxis.x;
            float deltaY = moveAxis.y < 0 ? -moveAxis.y : moveAxis.y;
            float audioDelta = deltaX > deltaY ? deltaX : deltaY;

            audioManager.SetHoverSoundVolume(audioDelta);
            */

            // Rotate
            Vector2 lookAxis = inputLookAction.ReadValue<Vector2>();
            if (playerInput.currentControlScheme == "Gamepad")
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

            // Raycast mask
            int layerMask = 1 << 8;
            layerMask = ~layerMask;

            // Calc internal camera pivot

            RaycastHit pivotHit;
            if (Physics.Raycast(cameraPivot.transform.position, cameraPivotInternal.transform.TransformDirection(Vector3.up), out pivotHit, maxInternalPivotDistance, layerMask))
            {
                cameraPivotInternal.transform.localPosition = Vector3.up * (pivotHit.distance * 0.95f);
            }
            else
            {
                cameraPivotInternal.transform.localPosition = Vector3.up * (maxInternalPivotDistance * 0.95f);
            }

            // Camera distance and position

            RaycastHit hit;
            if (Physics.Raycast(cameraPivotInternal.transform.position, cameraPivotInternal.transform.TransformDirection(Vector3.forward * -1), out hit, maxCameraDistance, layerMask))
            {
                Vector3 newCameraPos = cameraPivotInternal.transform.position + -cameraPivotInternal.transform.forward * (hit.distance - 0.1f);
                cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, newCameraPos, Time.deltaTime * 20);
                //cameraObject.transform.position = newCameraPos;
            }
            else
            {
                Vector3 newCameraPos = cameraPivotInternal.transform.position + -cameraPivotInternal.transform.forward * (maxCameraDistance - 0.1f);
                cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, newCameraPos, Time.deltaTime * 20);
                //cameraObject.transform.position = newCameraPos;
            }

            Debug.DrawLine(cameraPivotInternal.transform.position, cameraPivotInternal.transform.position + (cameraPivotInternal.transform.forward * 6f), Color.magenta);
            cameraObject.transform.LookAt(cameraPivotInternal.transform.position + (cameraPivotInternal.transform.forward * 6f));
        }
    }

    void OnControllsChanged(PlayerInput input)
	{
        //if(debugText)
            //debugText.text = input.currentControlScheme;
	}

    public void OnJump(InputValue input)
	{
        // Jump
        if (IsCharacterControllerGrounded())
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }

    public void Jump(float scale)
    {
        // Jump
        playerVelocity.y = Mathf.Sqrt((jumpHeight * scale) * -3.0f * gravityValue);
    }

    public void OnFire(InputValue input)
    {
        // Jump
        if (!characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shoot") && !shootLock)
        {
            shootLock = true;
            characterAnimator.SetTrigger("Shoot");
            StartCoroutine(CreateBullet());
        }
    }

    IEnumerator CreateBullet()
	{
        yield return new WaitForSeconds(0.1f);

        audioManager.PlayShootSound();

        GameObject bullet = Instantiate(bulletPrefab);

        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(cameraObject.transform.position, cameraObject.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawLine(cameraObject.transform.position, hit.point, Color.magenta);
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.LookAt(hit.point);
        }
        else
        {
            Vector3 point = cameraObject.transform.position + (cameraObject.transform.forward * 1000);
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.LookAt(point);
        }
        shootLock = false;
        //Debug.Break();
        yield return null;
    }

    public bool IsCharacterControllerGrounded()
	{
        float steps = 8;
        float stepAngle = 360 / steps;

        float distance = (characterController.height / 2);
        distance = distance * 1.1f;
        float distanceFromCenter = characterController.radius;

        if (RayCast(transform.position, -transform.up, 0, 0, distance)) return true;

        for (int iRays = 0; iRays < steps; iRays++)
		{
            if(RayCast(transform.position, -transform.up, stepAngle * iRays, distanceFromCenter, distance))
			{
                return true;
			}
		}

        for (int iRays = 0; iRays < steps; iRays++)
        {
            if (RayCast(transform.position, -transform.up, stepAngle * iRays, distanceFromCenter * 0.6f, distance))
            {
                return true;
            }
        }

        return false;
	}

    public bool IsCharacterControllerCeiled()
    {
        float steps = 8;
        float stepAngle = 360 / steps;

        float distance = (characterController.height / 2);
        distance = distance * 1.1f;
        float distanceFromCenter = characterController.radius;
        if (RayCast(transform.position, transform.up, 0, 0, distance)) return true;

        for (int iRays = 0; iRays < steps; iRays++)
        {
            if (RayCast(transform.position, transform.up, stepAngle * iRays, distanceFromCenter, distance * 0.8f))
            {
                return true;
            }
        }

        return false;
    }

    public void LockPlayer()
	{
        playerLocked = true;
	}

    public void UnlockPlayer()
    {
        playerLocked = false;
    }

    public bool RayCast(Vector3 origin, Vector3 direction, float angle, float distanceFromCenter, float distance)
	{
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        float rad = (angle - transform.rotation.eulerAngles.y) * (Mathf.PI / 180);
        Vector3 calculatedPosition = new Vector3(distanceFromCenter * Mathf.Cos(rad), 0f, distanceFromCenter * Mathf.Sin(rad));
        Vector3 originPosition = transform.position + calculatedPosition;

        //Debug.DrawLine(originPosition, originPosition + (direction * (distance)), Color.red);

        RaycastHit hit;
        if (Physics.Raycast(originPosition, direction, out hit, distance * 2, layerMask))
        {
            Debug.DrawLine(originPosition, originPosition + (direction * (distance)), Color.green);
            if (hit.distance < distance)
            {
                return true;
            }
        }
        else
		{
            Debug.DrawLine(originPosition, originPosition + (direction * (distance)), Color.blue);
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
        return angle = (angle % 360 + 360) % 360;
    }
}

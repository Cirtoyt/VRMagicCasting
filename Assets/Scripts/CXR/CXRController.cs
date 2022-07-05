using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class CXRController : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private Vector3 m_ControllerPosition;
    [SerializeField] private Vector3 m_ControllerVelocity;
    [SerializeField] private Quaternion m_ControllerRotation;
    [SerializeField] private bool m_IsGripActivated;
    [SerializeField] [Range(0, 1)] private float m_GripValue;
    [SerializeField] private bool m_IsTriggerActivated;
    [SerializeField] [Range(0, 1)] private float m_TriggerValue;
    [SerializeField] private Vector2 m_ThumbstickValue;
    [SerializeField] private bool m_IsPrimaryButtonPressed;
    [SerializeField] private bool m_IsSecondaryButtonPressed;
    [SerializeField] private bool m_IsMenuButtonPressed;
    public Vector3 position { get { return m_ControllerPosition; } }
    public Vector3 velocity { get { return m_ControllerVelocity; } }
    public Quaternion rotation { get { return m_ControllerRotation; } }
    public bool isGripActivated { get { return m_IsGripActivated; } }
    public float gripValue { get { return m_GripValue; } }
    public bool isTriggerActivated { get { return m_IsTriggerActivated; } }
    public float triggerValue { get { return m_TriggerValue; } }
    public Vector2 thumbstickValue { get { return m_ThumbstickValue; } }
    public bool isPrimaryButtonPressed { get { return m_IsPrimaryButtonPressed; } }
    public bool isSecondaryButtonPressed { get { return m_IsSecondaryButtonPressed; } }
    public bool isMenuButtonPressed { get { return m_IsMenuButtonPressed; } }
    [Header("Events")] // Other scripts can add listeners to these events in their code
    [Space]
    public UnityEvent positionValueEvent;
    public UnityEvent velocityValueEvent;
    public UnityEvent rotationValueEvent;
    public UnityEvent gripBeginEvent;
    public UnityEvent gripEndEvent;
    public UnityEvent gripValueEvent;
    public UnityEvent triggerBeginEvent;
    public UnityEvent triggerEndEvent;
    public UnityEvent triggerValueEvent;
    public UnityEvent thumbstickValueEvent;
    public UnityEvent primaryButtonBeginEvent;
    public UnityEvent primaryButtonEndEvent;
    public UnityEvent secondaryButtonBeginEvent;
    public UnityEvent secondaryButtonEndEvent;
    public UnityEvent menuButtonBeginEvent;
    public UnityEvent menuButtonEndEvent;
    [Header("OpenXR Input Actions")]
    [SerializeField] private InputActionProperty positionActionOpenXR;
    [SerializeField] private InputActionProperty velocityActionOpenXR;
    [SerializeField] private InputActionProperty rotationActionOpenXR;
    [SerializeField] private InputActionProperty gripButtonActionOpenXR;
    [SerializeField] private InputActionProperty gripValueActionOpenXR;
    [SerializeField] private InputActionProperty triggerButtonActionOpenXR;
    [SerializeField] private InputActionProperty triggerValueActionOpenXR;
    [SerializeField] private InputActionProperty thumbstickActionOpenXR;
    [SerializeField] private InputActionProperty primaryButtonActionOpenXR;
    [SerializeField] private InputActionProperty secondaryButtonActionOpenXR;
    [SerializeField] private InputActionProperty menuButtonActionOpenXR;
    [Space]
    [Space]
    [Header("Oculus Touch Input Actions")]
    [SerializeField] private InputActionProperty positionActionOculusTouch;
    [SerializeField] private InputActionProperty velocityActionOculusTouch;
    [SerializeField] private InputActionProperty rotationActionOculusTouch;
    [SerializeField] private InputActionProperty gripButtonActionOculusTouch;
    [SerializeField] private InputActionProperty gripValueActionOculusTouch;
    [SerializeField] private InputActionProperty triggerButtonActionOculusTouch;
    [SerializeField] private InputActionProperty triggerValueActionOculusTouch;
    [SerializeField] private InputActionProperty thumbstickActionOculusTouch;
    [SerializeField] private InputActionProperty primaryButtonActionOculusTouch;
    [SerializeField] private InputActionProperty secondaryButtonActionOculusTouch;
    [SerializeField] private InputActionProperty menuButtonActionOculusTouch;

    private void Awake()
    {
#if !UNITY_ANDROID
        positionActionOpenXR.action.performed += OnPosition;
        velocityActionOpenXR.action.performed += OnVelocity;
        rotationActionOpenXR.action.performed += OnRotation;
        gripButtonActionOpenXR.action.performed += OnGripButtonPerformed;
        gripButtonActionOpenXR.action.canceled += OnGripButtonCancelled;
        gripValueActionOpenXR.action.performed += OnGripValuePerformed;
        gripValueActionOpenXR.action.canceled += OnGripValueCancelled;
        triggerButtonActionOpenXR.action.performed += OnTriggerButtonPerformed;
        triggerButtonActionOpenXR.action.canceled += OnTriggerButtonCancelled;
        triggerValueActionOpenXR.action.performed += OnTriggerValuePerformed;
        triggerValueActionOpenXR.action.canceled += OnTriggerValueCancelled;
        thumbstickActionOpenXR.action.performed += OnThumbstickPerformed;
        thumbstickActionOpenXR.action.canceled += OnThumbstickCancelled;
        primaryButtonActionOpenXR.action.performed += OnPrimaryButtonPerformed;
        primaryButtonActionOpenXR.action.canceled += OnPrimaryButtonCancelled;
        secondaryButtonActionOpenXR.action.performed += OnSecondaryButtonPerformed;
        secondaryButtonActionOpenXR.action.canceled += OnSecondaryButtonCancelled;
        menuButtonActionOpenXR.action.performed += OnMenuButtonPerformed;
        menuButtonActionOpenXR.action.canceled += OnMenuButtonCancelled;
#else
        positionActionOculusTouch.action.performed += OnPosition;
        velocityActionOculusTouch.action.performed += OnVelocity;
        rotationActionOculusTouch.action.performed += OnRotation;
        gripButtonActionOculusTouch.action.performed += OnGripButtonPerformed;
        gripButtonActionOculusTouch.action.canceled += OnGripButtonCancelled;
        gripValueActionOculusTouch.action.performed += OnGripValuePerformed;
        gripValueActionOculusTouch.action.canceled += OnGripValueCancelled;
        triggerButtonActionOculusTouch.action.performed += OnTriggerButtonPerformed;
        triggerButtonActionOculusTouch.action.canceled += OnTriggerButtonCancelled;
        triggerValueActionOculusTouch.action.performed += OnTriggerValuePerformed;
        triggerValueActionOculusTouch.action.canceled += OnTriggerValueCancelled;
        thumbstickActionOculusTouch.action.performed += OnThumbstickPerformed;
        thumbstickActionOculusTouch.action.canceled += OnThumbstickCancelled;
        primaryButtonActionOculusTouch.action.performed += OnPrimaryButtonPerformed;
        primaryButtonActionOculusTouch.action.canceled += OnPrimaryButtonCancelled;
        secondaryButtonActionOculusTouch.action.performed += OnSecondaryButtonPerformed;
        secondaryButtonActionOculusTouch.action.canceled += OnSecondaryButtonCancelled;
        menuButtonActionOculusTouch.action.performed += OnMenuButtonPerformed;
        menuButtonActionOculusTouch.action.canceled += OnMenuButtonCancelled;
#endif
    }

    private void OnPosition(InputAction.CallbackContext callbackContext)
    {
        m_ControllerPosition = callbackContext.ReadValue<Vector3>();
        positionValueEvent.Invoke();
    }

    private void OnVelocity(InputAction.CallbackContext callbackContext)
    {
        m_ControllerVelocity = callbackContext.ReadValue<Vector3>();
        velocityValueEvent.Invoke();
    }

    private void OnRotation(InputAction.CallbackContext callbackContext)
    {
        m_ControllerRotation = callbackContext.ReadValue<Quaternion>();
        rotationValueEvent.Invoke();
    }

    private void OnGripButtonPerformed(InputAction.CallbackContext callbackContext)
    {
        m_IsGripActivated = true;
        gripBeginEvent.Invoke();
    }

    private void OnGripButtonCancelled(InputAction.CallbackContext callbackContext)
    {
        m_IsGripActivated = false;
        gripEndEvent.Invoke();
    }

    private void OnGripValuePerformed(InputAction.CallbackContext callbackContext)
    {
        m_GripValue = callbackContext.ReadValue<float>();
        gripValueEvent.Invoke();
    }

    private void OnGripValueCancelled(InputAction.CallbackContext callbackContext)
    {
        m_GripValue = 0;
        gripValueEvent.Invoke();
    }

    private void OnTriggerButtonPerformed(InputAction.CallbackContext callbackContext)
    {
        m_IsTriggerActivated = true;
        triggerBeginEvent.Invoke();
    }

    private void OnTriggerButtonCancelled(InputAction.CallbackContext callbackContext)
    {
        m_IsTriggerActivated = false;
        triggerEndEvent.Invoke();
    }

    private void OnTriggerValuePerformed(InputAction.CallbackContext callbackContext)
    {
        m_TriggerValue = callbackContext.ReadValue<float>();
        triggerValueEvent.Invoke();
    }

    private void OnTriggerValueCancelled(InputAction.CallbackContext callbackContext)
    {
        m_TriggerValue = 0;
        triggerValueEvent.Invoke();
    }

    private void OnThumbstickPerformed(InputAction.CallbackContext callbackContext)
    {
        m_ThumbstickValue = callbackContext.ReadValue<Vector2>();
        thumbstickValueEvent.Invoke();
    }
    private void OnThumbstickCancelled(InputAction.CallbackContext callbackContext)
    {
        m_ThumbstickValue = Vector2.zero;
        thumbstickValueEvent.Invoke();
    }

    private void OnPrimaryButtonPerformed(InputAction.CallbackContext callbackContext)
    {
        m_IsPrimaryButtonPressed = true;
        primaryButtonBeginEvent.Invoke();
    }

    private void OnPrimaryButtonCancelled(InputAction.CallbackContext callbackContext)
    {
        m_IsPrimaryButtonPressed = false;
        primaryButtonEndEvent.Invoke();
    }

    private void OnSecondaryButtonPerformed(InputAction.CallbackContext callbackContext)
    {
        m_IsSecondaryButtonPressed = true;
        secondaryButtonBeginEvent.Invoke();
    }

    private void OnSecondaryButtonCancelled(InputAction.CallbackContext callbackContext)
    {
        m_IsSecondaryButtonPressed = false;
        secondaryButtonEndEvent.Invoke();
    }

    private void OnMenuButtonPerformed(InputAction.CallbackContext callbackContext)
    {
        m_IsMenuButtonPressed = true;
        menuButtonBeginEvent.Invoke();
    }

    private void OnMenuButtonCancelled(InputAction.CallbackContext callbackContext)
    {
        m_IsMenuButtonPressed = false;
        menuButtonEndEvent.Invoke();
    }

    private void OnEnable()
    {
#if !UNITY_ANDROID
        positionActionOpenXR.action.Enable();
        velocityActionOpenXR.action.Enable();
        rotationActionOpenXR.action.Enable();
        gripButtonActionOpenXR.action.Enable();
        gripValueActionOpenXR.action.Enable();
        triggerButtonActionOpenXR.action.Enable();
        triggerValueActionOpenXR.action.Enable();
        thumbstickActionOpenXR.action.Enable();
        primaryButtonActionOpenXR.action.Enable();
        secondaryButtonActionOpenXR.action.Enable();
        menuButtonActionOpenXR.action.Enable();
#else
        positionActionOculusTouch.action.Enable();
        velocityActionOculusTouch.action.Enable();
        rotationActionOculusTouch.action.Enable();
        gripButtonActionOculusTouch.action.Enable();
        gripValueActionOculusTouch.action.Enable();
        triggerButtonActionOculusTouch.action.Enable();
        triggerValueActionOculusTouch.action.Enable();
        thumbstickActionOculusTouch.action.Enable();
        primaryButtonActionOculusTouch.action.Enable();
        secondaryButtonActionOculusTouch.action.Enable();
        menuButtonActionOculusTouch.action.Enable();
#endif
    }

    private void OnDisable()
    {
#if !UNITY_ANDROID
        positionActionOpenXR.action.Disable();
        velocityActionOpenXR.action.Disable();
        rotationActionOpenXR.action.Disable();
        gripButtonActionOpenXR.action.Disable();
        gripValueActionOpenXR.action.Disable();
        triggerButtonActionOpenXR.action.Disable();
        triggerValueActionOpenXR.action.Disable();
        thumbstickActionOpenXR.action.Disable();
        primaryButtonActionOpenXR.action.Disable();
        secondaryButtonActionOpenXR.action.Disable();
        menuButtonActionOpenXR.action.Disable();
#else
         positionActionOculusTouch.action.Disable();
        velocityActionOculusTouch.action.Disable();
        rotationActionOculusTouch.action.Disable();
        gripButtonActionOculusTouch.action.Disable();
        gripValueActionOculusTouch.action.Disable();
        triggerButtonActionOculusTouch.action.Disable();
        triggerValueActionOculusTouch.action.Disable();
        thumbstickActionOculusTouch.action.Disable();
        primaryButtonActionOculusTouch.action.Disable();
        secondaryButtonActionOculusTouch.action.Disable();
        menuButtonActionOculusTouch.action.Disable();
#endif
    }
}

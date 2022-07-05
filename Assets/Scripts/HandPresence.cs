using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(CXRController))]
public class HandPresence : MonoBehaviour
{
    public bool ShowController
    {
        get => m_showController;
        set
        {
            if (m_showController != value)
            {
                if (value == true)
                {
                    
                }
                else
                {
                    
                }
                m_showController = value;
            }
        }
    }
    [Header("Statics")]
    [SerializeField] private InputDeviceCharacteristics targetControllerCharacteristics;
    [SerializeField] private List<GameObject> controllerPrefabs;
    [SerializeField] private GameObject handModelPrefab;
    [SerializeField] private Animator handAnimator;

    private bool m_showController;
    private CXRController controller;
    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;

    void Awake()
    {
        controller = GetComponentInParent<CXRController>();

        TryInitialize();
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(targetControllerCharacteristics, devices);

        foreach (var device in devices)
        {
            Debug.Log(device.name + device.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogWarning("Did not find corresponding controller model");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (m_showController)
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
            }

            if (handAnimator)
            {
                handAnimator.SetFloat("Grip", controller.gripValue);
                handAnimator.SetFloat("Trigger", controller.triggerValue);
            }
        }
    }

    private void TryUpdateControllerVisibility()
    {

    }
}
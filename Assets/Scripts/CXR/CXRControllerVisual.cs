using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CXRControllerVisual : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private bool showController = false;
    [Header("Statics")]
    [SerializeField] private CXRController controller;
    [SerializeField] private InputDeviceCharacteristics targetControllerCharacteristics;
    [SerializeField] private List<GameObject> controllerPrefabs;

    private InputDevice targetDevice;
    private GameObject spawnedControllerModel;

    private void Awake()
    {
        TryInitialise();
    }

    private void Update()
    {
        CheckReInitialisationNeeded();

        CheckUpdateModelVisibility();
    }

    private void CheckReInitialisationNeeded()
    {
        if (targetDevice != null)
        {
            if (!targetDevice.isValid)
            {
                TryInitialise();
            }
        }
        else
        {
            TryInitialise();
        }
    }

    private void TryInitialise()
    {
        // Remove old model

        if (spawnedControllerModel)
        {
            Destroy(spawnedControllerModel);
        }

        // Spawn new model

        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(targetControllerCharacteristics, devices);

        // Debug print all found devices
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
                spawnedControllerModel = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogWarning("Did not find corresponding controller model. Spawning default model");
                spawnedControllerModel = Instantiate(controllerPrefabs[0], transform);
            }
        }
        else
        {
            Debug.LogWarning("Could not find device with desired properties: " + targetControllerCharacteristics + " to spawn the matching controller model");
        }
    }

    private void CheckUpdateModelVisibility()
    {
        if (spawnedControllerModel)
        {
            if (spawnedControllerModel.activeSelf && !showController)
            {
                spawnedControllerModel.SetActive(false);
            }
            else if (!spawnedControllerModel.activeSelf && showController)
            {
                spawnedControllerModel.SetActive(true);
            }
        }
    }

    public void SetVisibility(bool newState)
    {
        showController = newState;
        CheckUpdateModelVisibility();
    }

    public bool IsVisible => showController;
}

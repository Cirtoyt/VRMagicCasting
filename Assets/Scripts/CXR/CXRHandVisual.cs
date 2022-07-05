using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CXRHandVisual : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private bool showHand = true;
    public bool HideHandWhilstGrabbing = false;
    [Header("Statics")]
    [SerializeField] private CXRController controller;
    [SerializeField] private GameObject handModel;

    private Animator anim;

    private void Awake()
    {
        anim = handModel.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        controller.gripBeginEvent.AddListener(HideHandOnGrabBegin);
        controller.gripEndEvent.AddListener(ShowHandOnGrabEnd);
    }

    private void Update()
    {
        CheckUpdateModelVisibility();

        if (handModel)
        {
            AnimateHand();
        }
    }

    private void AnimateHand()
    {
        anim.SetFloat("Grip", controller.gripValue);
        anim.SetFloat("Trigger", controller.triggerValue);
    }

    public void HideHandOnGrabBegin()
    {
        if (HideHandWhilstGrabbing)
        {
            SetVisibility(false);
        }
    }

    public void ShowHandOnGrabEnd()
    {
        if (HideHandWhilstGrabbing)
        {
            SetVisibility(true);
        }
    }

    private void OnDisable()
    {
        controller.gripBeginEvent.RemoveListener(HideHandOnGrabBegin);
        controller.gripEndEvent.RemoveListener(ShowHandOnGrabEnd);
    }

    private void CheckUpdateModelVisibility()
    {
        if (handModel)
        {
            if (handModel.activeSelf && !showHand)
            {
                handModel.SetActive(false);
            }
            else if (!handModel.activeSelf && showHand)
            {
                handModel.SetActive(true);
            }
        }
    }

    public void SetVisibility(bool newState)
    {
        showHand = newState;
        CheckUpdateModelVisibility();
    }

    public bool IsVisible => showHand;
}

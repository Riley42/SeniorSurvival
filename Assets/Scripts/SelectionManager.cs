using StarterAssets;
using System;
using TMPro;
using UnityEngine;

public class SelectionManager : Singleton<SelectionManager>
{
    [SerializeField] StarterAssetsInputs playerInput;
    [SerializeField] GameObject interactionObject;

    private float interactionRange = 5f;
    private TextMeshProUGUI interactionText;

    protected override void Awake()
    {
        base.Awake();
        interactionText = interactionObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        CheckForInteractables();
    }

    private void CheckForInteractables()
    {
        if (RaycastFromCamera<IInteractable>(interactionRange, GameLayers.InteractableLayer, out var interactable))
        {
            ShowHoverMessage("Press 'E' to interact with " + interactable.GetDisplayName());
            HandleInteraction(interactable);
        }
        else
        {
            ClearHoverMessage();
        }
    }
    public IHarvestable CheckForHarvestable()
    {
        if (RaycastFromCamera<IHarvestable>(5f, GameLayers.InteractableLayer, out var harvestable))
        {
            return harvestable;
        }
        return null;
    }

    private void HandleInteraction(IInteractable interactable)
    {
        if (playerInput.interact)
        {
            interactable.Interact();
            playerInput.interact = false; // Reset the input
        }
    }

    private bool RaycastFromCamera<T>(float range, LayerMask layerMask, out T result) where T : class
    {
        result = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, range, layerMask, QueryTriggerInteraction.Ignore))
        {
            result = hitInfo.transform.GetComponent<T>();
            return result != null;            
        }

        return false;
    }

    void ShowHoverMessage(string message)
    {
        interactionText.text = message;
        interactionObject.SetActive(true);
    }
    void ClearHoverMessage()
    {
        interactionText.text = string.Empty;
        interactionObject.SetActive(false);
    }
}

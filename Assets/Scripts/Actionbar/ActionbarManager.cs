using StarterAssets;
using System;
using UnityEngine;

public class ActionbarManager : MonoBehaviour
{
    [SerializeField] StarterAssetsInputs inputs;
    [SerializeField] ItemSO testItem;

    public static event Action<ItemSO> OnUse;

    private void Update()
    {
        HandleMouseClickInput();
    }

    private void HandleMouseClickInput()
    {
        if (inputs.primaryAction)
        { 
            HandlePrimaryClick();
            inputs.PrimaryInput(false);
        }
    }

    private void HandlePrimaryClick()
    {
        var item = testItem;
        if (item == null) return;

        OnUse?.Invoke(item);

    }
}

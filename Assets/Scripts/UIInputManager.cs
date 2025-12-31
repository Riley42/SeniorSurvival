using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class UIInputManager : Singleton<UIInputManager>
{
    PlayerInput playerInput;
    InputActionMap uiMap;
    InputActionMap playerMap;

    InputAction inventoryAction;
    InputAction craftingAction;
    InputAction exitWindow;

    bool isInventoryOpen = false;
    bool isCraftingOpen = false;

    public event Action<bool> OnInventoryToggled;
    public event Action<bool> OnCraftingToggled;
    public event Action OnExitWindow;

    protected override void Awake()
    {
        base.Awake();

        playerInput = GetComponent<PlayerInput>();
        uiMap = playerInput.actions.FindActionMap("UI");
        playerMap = playerInput.actions.FindActionMap("Player");

        inventoryAction = uiMap.FindAction("ToggleInventory");
        craftingAction = uiMap.FindAction("ToggleCrafting");
        exitWindow = uiMap.FindAction("ExitWindow");
    }
    private void OnEnable()
    {
        uiMap.Enable();
        inventoryAction.performed += ToggleInventory;
        craftingAction.performed += ToggleCrafting;
        exitWindow.performed += HandleExitWindow;
    }
    private void OnDisable()
    {
        uiMap.Disable();
        inventoryAction.performed -= ToggleInventory;
        craftingAction.performed -= ToggleCrafting;
        exitWindow.performed -= HandleExitWindow;
    }
    private void ToggleInventory(InputAction.CallbackContext context)
    {
        isInventoryOpen = !isInventoryOpen;
        HandleCursorLock(isInventoryOpen);
        OnInventoryToggled?.Invoke(isInventoryOpen);
    }
    private void ToggleCrafting(InputAction.CallbackContext context)
    {
        isCraftingOpen = !isCraftingOpen;
        HandleCursorLock(isCraftingOpen);
        OnCraftingToggled?.Invoke(isCraftingOpen);
    }

    private void HandleExitWindow(InputAction.CallbackContext context)
    {
        HandleCursorLock(false);
        OnExitWindow?.Invoke();
        isInventoryOpen = false;
        OnInventoryToggled?.Invoke(false);
    }

    public void HandleCursorLock(bool isMenuOpen)
    {
        if (isMenuOpen)
        {
            playerMap.Disable();
        }
        else
        {
            playerMap.Enable();
        }
        Cursor.lockState = isMenuOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }

}

using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool interact;
		public bool primaryAction;
		public bool secondaryAction;
		public float scrollWheel;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;



#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}
		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}
		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}
		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
        public void OnInteract(InputValue value)
        {
            InteractInput(value.isPressed);
        }
        public void OnPrimaryAction(InputValue value)
        {
            PrimaryInput(value.isPressed);
        }
        public void OnSecondaryAction(InputValue value)
        {
            SecondaryInput(value.isPressed);
        }
        public void OnScrollWheel(InputValue value)
		{
            ScrollInput(value.Get<Vector2>());
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 
		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}
		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}
		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
        public void InteractInput(bool newInteractState)
        {
            interact = newInteractState;
        }
        public void ScrollInput(Vector2 scrollDelta)
        {
            // We only care about vertical scrolling (Y-axis)
            scrollWheel = scrollDelta.y;
        }
        public void PrimaryInput(bool newPrimaryState)
        {
            primaryAction = newPrimaryState;
        }
        public void SecondaryInput(bool newSecondaryState)
        {
            secondaryAction = newSecondaryState;
        }

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}	
}
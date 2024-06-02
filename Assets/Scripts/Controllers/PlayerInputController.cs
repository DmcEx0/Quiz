using Quiz.Tool;
using System;
using UnityEngine;
using VContainer.Unity;
using static UnityEngine.InputSystem.InputAction;

namespace Quiz.Controllers
{
    public class PlayerInputController : IInitializable, IDisposable
    {
        private GameInput _playerInput;

        public void Initialize()
        {
            _playerInput = new GameInput();

            _playerInput.Enable();

            _playerInput.Player.Click.canceled += MadeClick;
        }

        public void Dispose()
        {
            _playerInput.Player.Click.canceled -= MadeClick;

            _playerInput.Disable();
        }

        private void MadeClick(CallbackContext ctx)
        {
            if (GetHitClick().collider == null)
            {
                return;
            }

            if (GetHitClick().collider.TryGetComponent<IClickable>(out IClickable clickable))
            {
                clickable.Click();
            }
        }

        private RaycastHit2D GetHitClick()
        {
            var pointerPosition = _playerInput.Player.ClickPointer.ReadValue<Vector2>();
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(pointerPosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            return hit;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private KeyCode _left = KeyCode.LeftArrow;
    [SerializeField] private KeyCode _right = KeyCode.RightArrow;

    [SerializeField] private Vector2 _movementVector = Vector2.zero;

    [Serializable]public class MovementVectorUpdate : UnityEvent<Vector2> {}
    public MovementVectorUpdate MovementVectorUpdateCallback = new MovementVectorUpdate();

    [Serializable] public class ButtonHandler
    {
        [SerializeField] private string _name = string.Empty;
        public string Name { get { return _name; } }

        [SerializeField] private KeyCode _keyCode = KeyCode.None;

        [Space]
        public UnityEvent OnDown = new UnityEvent();
        public UnityEvent OnUp = new UnityEvent();

        public void HandleButton()
        {
            bool down = Input.GetKeyDown(_keyCode);
            bool up = Input.GetKeyUp(_keyCode);

            if (down)
                OnDown.Invoke();
            if (up)
                OnUp.Invoke();
        }
    }

    [SerializeField] private List<ButtonHandler> _additionalButtons = new List<ButtonHandler>();

    public void HandleMovement()
    {
        bool left = Input.GetKey(_left);
        bool right = Input.GetKey(_right);

        if (left)
            _movementVector.x = -1;
        else if (right)
            _movementVector.x = 1;
        else
            _movementVector.x = 0;

        if (left && right)
            _movementVector.x = 0;

        MovementVectorUpdateCallback.Invoke(_movementVector);
    }

    private void Update()
    {
        HandleMovement();
        foreach (var item in _additionalButtons)
            item.HandleButton();
    }
}

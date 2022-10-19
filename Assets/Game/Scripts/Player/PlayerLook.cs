using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;
using Game.Input;

namespace Player
{
    public class PlayerLook : MonoBehaviour 
    {
        [SerializeField] private InputReader input;
        //感度
        [SerializeField] private float sensX;
        [SerializeField] private float sensY;

        private Vector2 lookValue;
        private float mouseX;
        private float mouseY;

        private float multiplier;

        private float rotationX;
        private float rotationY;

        void Awake() 
        {
            input.OnLook.Subscribe(value => Look(value)).AddTo(this);
        }

        private void OnLook(InputAction.CallbackContext obj) 
        {
            var value = obj.ReadValue<Vector2>();
            rotationY += value.x * sensX * multiplier;
            rotationX -= value.y * sensY * multiplier;

            rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        }

        // Start is called before the first frame update
        void Start() 
        {
            rotationY = this.transform.rotation.eulerAngles.y;
            //rotationX = this.transform.rotation.x;

            multiplier = 0.1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update() 
        {
            Quaternion deffRot = Quaternion.AngleAxis(rotationX, Vector3.right);
            deffRot = Quaternion.AngleAxis(rotationY, transform.up);
            transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
        }

        void Look(Vector2 value) 
        {
            lookValue = value;
            rotationY += value.x * sensX * multiplier;
            rotationX -= value.y * sensY * multiplier;

            rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        }
    }
}

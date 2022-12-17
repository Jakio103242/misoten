using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;
using Game.Input;

namespace Game.Player
{
    public class PlayerLook : MonoBehaviour 
    {
        [SerializeField] private InputReader input;
        //感度
        [SerializeField] private float sensX;
        [SerializeField] private float sensY;
        [SerializeField] private float rotateSpeed;

        [SerializeField] private Transform eye;
        [SerializeField] private PlayerMovements movements;

        private float multiplier;

        private float eyeRotationX;
        private float rotationY;

        private Quaternion targetRot;
        private Vector3 tmpVelocity;

        void Awake() 
        {
            input.OnLook.Subscribe(value => RotateEye(value)).AddTo(this);
        }

        void Start() 
        {
            eyeRotationX = eye.localRotation.x;

            multiplier = 0.1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            tmpVelocity = movements.Velocity;
        }

        void Update() 
        {
            if(tmpVelocity.sqrMagnitude > 0.5f * 0.5f)
            {
                targetRot = Quaternion.LookRotation(tmpVelocity.normalized, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
            }
            tmpVelocity = new Vector3(movements.Velocity.x, 0, movements.Velocity.z);
        }

        void RotateEye(Vector2 value) 
        {
            eyeRotationX -= value.y * sensY * multiplier;

            eyeRotationX = Mathf.Clamp(eyeRotationX, -90f, 90f);

            eye.localRotation = Quaternion.AngleAxis(eyeRotationX, Vector3.right);
        }
    }
}

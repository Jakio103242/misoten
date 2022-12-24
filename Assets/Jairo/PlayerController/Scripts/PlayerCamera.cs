using UnityEngine;

namespace Game.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        private const float CameraMovementThreshold = 0.01f;
        
        [SerializeField] private Transform cameraTarget = default;
        [SerializeField] private Vector2 cameraAngleOverride = Vector2.zero;
        [SerializeField] private float topClamp = 70.0f;
        [SerializeField] private float bottomClamp = -30.0f;
        [SerializeField, Range(0.01F, 50.0F)] protected float sensibilityX = 2.0F; 
        [SerializeField, Range(0.01F, 50.0F)] protected float sensibilityY = 2.0F; 
        [SerializeField] private float timeToUnlockCameraAfterLoad = 0.5F;
        [Header("Look At")] 
        [SerializeField] private Transform lookAtTransform = default;
        [SerializeField] private float forcedLookAtSmooth = 2F;
        [SerializeField] private Vector3 forwardLookAt = new (0, 0, 5);
        
        private Vector2 cameraInput;
        private float cameraTargetYaw;
        private float cameraTargetPitch;
        private bool isUnlocked;
        private PlayerController playerController;
        private Vector3 dampVelocity;
        
        public Vector2 Input { set => cameraInput = value; }

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {
            if (!isUnlocked && Time.timeSinceLevelLoad > timeToUnlockCameraAfterLoad)
                isUnlocked = true;
            ProcessCameraRotation();
            
            if (playerController.IsLookAtForced)
                lookAtTransform.position = Vector3.Slerp(lookAtTransform.position, playerController.ForcedTargetPosition, forcedLookAtSmooth * Time.deltaTime);
            else
                lookAtTransform.localPosition = Vector3.Slerp(lookAtTransform.localPosition, forwardLookAt, forcedLookAtSmooth * Time.deltaTime);
        }
        
        private void ProcessCameraRotation()
        {
            if (cameraInput.sqrMagnitude >= CameraMovementThreshold && isUnlocked)
            {
                cameraTargetYaw += cameraInput.x * sensibilityX * Time.deltaTime;
                cameraTargetPitch -= cameraInput.y * sensibilityY * Time.deltaTime;
            }
            cameraTargetYaw = ClampAngle(cameraTargetYaw, float.MinValue, float.MaxValue);
            cameraTargetPitch = ClampAngle(cameraTargetPitch, bottomClamp, topClamp);
            cameraTarget.rotation = Quaternion.Euler(cameraTargetPitch + cameraAngleOverride.x, cameraTargetYaw + cameraAngleOverride.y, 0.0f);
        }
        
        private float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}
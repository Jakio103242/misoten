using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        static private readonly int InputMagnitudeAnimParameter = Animator.StringToHash("InputMagnitude");
        static private readonly int InputDirectionAnimParameter = Animator.StringToHash("InputDirection");
        static private readonly int RotationDirectionAnimParameter = Animator.StringToHash("RotationDirection");
        static private readonly int IsRightLegUpAnimParameter = Animator.StringToHash("IsRightLegUp");

        [Header("Player Rotation")]
        [SerializeField] private float rotationSmooth = 0.25F;
        [SerializeField] private float forcedRotationSpeed = 6.0F;

        private CharacterController controller;
        private Animator animator;
        private float ySpeed;
        private Vector2 movementInput;
        private Transform leftFoot;
        private Transform rightFoot;
        private Transform cameraTransform;
        private float currentRotationVelocity;
        private float inputMagnitude;
        private float unlockRotationAt;

        public bool IsLookAtForced => Time.time <= unlockRotationAt;
        public Vector3 ForcedTargetPosition { get; private set; }
        public Vector2 MovementInput { set => movementInput = value; }
        public bool IsGrounded { get; private set; }
        public int MovementLockers { get; set; }
        public bool IsLocked => MovementLockers > 0;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            animator.applyRootMotion = true;
            leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
            rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);
            cameraTransform = Camera.main != null ? Camera.main.transform : Camera.current.transform;
        }

        private void Update()
        {
            IsGrounded = controller.isGrounded;

            if (MovementLockers < 0)
                MovementLockers = 0;

            bool canMove = !IsLocked;
            Vector3 inputDirection = canMove ? new (movementInput.x, 0, movementInput.y) : Vector3.zero;
            inputMagnitude = Mathf.Clamp01(inputDirection.magnitude);
            animator.SetFloat(InputMagnitudeAnimParameter, inputMagnitude, 0.05F, Time.deltaTime);
            ySpeed += Physics.gravity.y * Time.deltaTime;
            animator.SetBool(IsRightLegUpAnimParameter, rightFoot.position.y > leftFoot.position.y);
            Vector3 facing = transform.rotation * Vector3.forward;

            if (inputMagnitude > 0.1F)
            {
                float inputDirAngle = Vector3.SignedAngle(facing, transform.TransformDirection(inputDirection), Vector3.up);
                float lastAngle = animator.GetFloat(InputDirectionAnimParameter);
                if (lastAngle < -90 && inputDirAngle > 90)
                    animator.SetFloat(InputDirectionAnimParameter, 180);
                if (lastAngle > 90 && inputDirAngle < -90)
                    animator.SetFloat(InputDirectionAnimParameter, -180);
                animator.SetFloat(RotationDirectionAnimParameter, 0, 0.1F, Time.deltaTime);
                animator.SetFloat(InputDirectionAnimParameter, inputDirAngle, 0.1F, Time.deltaTime);
            }
        }

        private void OnAnimatorMove()
        {
            Vector3 movement = animator.deltaPosition;
            movement.y = ySpeed * Time.deltaTime;
            controller.Move(movement);

            if (!IsLookAtForced)
            {
                if (inputMagnitude > 0.1F)
                    transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, cameraTransform.eulerAngles.y, ref currentRotationVelocity, rotationSmooth);
                else
                    transform.rotation = animator.rootRotation;
            }
            else
            {
                Vector3 lookAtDirection = (ForcedTargetPosition - transform.position).normalized;
                lookAtDirection.y = 0;
                if (lookAtDirection != Vector3.zero)
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAtDirection), Time.deltaTime * forcedRotationSpeed);
            }
        }
        
        public void ForceRotateToLookAt(Vector3 targetPosition, float duration = 0.1F)
        {
            ForcedTargetPosition = targetPosition;
            unlockRotationAt = Time.time + duration;
        }
    }
}
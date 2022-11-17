using UnityEngine;
using UniRx;
using Game.Input;

namespace Player 
{
    public class PlayerMovements : MonoBehaviour 
    {
        // state
        enum State 
        {
            None,
            Walk,
            Dash
        }
        [SerializeField] InputReader input;

        [SerializeField] private State state;

        // check ground
        private bool grounded;
        [SerializeField] Transform groundCheck;
        [SerializeField] float groundDistance;
        [SerializeField] LayerMask groundMask;

        // move parameter
        private Vector3 moveValue;
        private Vector3 velocity;
        public Vector3 Velocity => velocity;
        [SerializeField] private float dashMag;
        private float speedMag;
        private float verticalAcceleration;
        private Vector3 moveAcceleration;
        [SerializeField] float walkSpeed;
        [SerializeField] private float timeToMaxSpeed;
        private float accelerationMag;
        [SerializeField] float gravity;
        private CharacterController controller;

        private Animator animator;

        void Awake()
        {
            input.OnMove.Subscribe(value => Move(value)).AddTo(this);
            input.OnDash.Subscribe(_ => Dash()).AddTo(this);
            input.OnWalk.Subscribe(_ => Walk()).AddTo(this);
        }
    
        void Start() 
        {
            controller = GetComponent<CharacterController>();

            animator = GetComponent<Animator>();

            accelerationMag = walkSpeed / timeToMaxSpeed;
            velocity = Vector3.zero;
            speedMag = 1.0f;
            verticalAcceleration = 0.0f;
            grounded = false;
            state = State.None;
        }
    
        void Update() 
        {
            moveAcceleration = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * new Vector3(moveValue.x, 0, moveValue.y);
            velocity += moveAcceleration.normalized * accelerationMag * Time.deltaTime * dashMag;

            switch(state) 
            {
                case State.None:
                    velocity = Vector3.zero;

                    animator.Play("Idle");
                    break;
                case State.Walk:
                case State.Dash:
                    var maxSpeed = walkSpeed * speedMag;
                    if(velocity.sqrMagnitude > maxSpeed * maxSpeed) 
                    {
                        velocity = moveAcceleration.normalized * maxSpeed;
                    }

                    animator.Play("Walk");
                    break;
                default:
                    break;
            }

            // 上下方向の速度
            if (grounded) 
            {
                velocity.y = 0.0f;
            }
            else 
            {
                verticalAcceleration += (gravity * Time.deltaTime);
                velocity.y += verticalAcceleration;
            }
            // 動かす
            controller.Move(velocity * Time.deltaTime);

            CheckGrounded();
        }

        private void CheckGrounded() 
        {
            var endPoint = groundCheck.position + (-groundCheck.up * groundDistance);
            grounded = Physics.Linecast(groundCheck.position, endPoint, groundMask);
        }

        private void Move(Vector2 value) 
        {
            moveValue = value;
            moveValue.Normalize();
            if(moveValue.sqrMagnitude > 0.0f)
            {
                if(state == State.Dash) return;
                else
                {
                    state = State.Walk;
                }
            }
            else
            {
                state = State.None;
            }
        }

        private void Dash() 
        {
            if(state == State.Dash) return;
            speedMag = dashMag;
            state = State.Dash;
        }
        private void Walk() 
        {
            speedMag = 1.0f;
            state = State.Walk;
        }
    }
}
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
        [SerializeField] private float dashMag;
        private float speedMag;
        private float jumpedMag;
        private float jumpAcceleration;
        private Vector3 moveAcceleration;
        [SerializeField] float walkSpeed;
        [SerializeField] private float timeToMaxSpeed;
        private float magAcceleration;
        [SerializeField] float jumpSpeed;
        [SerializeField] float gravity;
        [SerializeField] private bool jumped;
        private CharacterController controller;

        void Awake() 
        {
            input.OnMove.Subscribe(value => Move(value)).AddTo(this);
            input.OnDash.Subscribe(_ => Dash()).AddTo(this);
            input.OnWalk.Subscribe(_ => Walk()).AddTo(this);
            //input.OnJump.Subscribe(_ => Jump()).AddTo(this);
        }
    
        void Start() 
        {
            controller = GetComponent<CharacterController>();

            magAcceleration = walkSpeed / timeToMaxSpeed;
            velocity = Vector3.zero;
            speedMag = 1.0f;
            jumpAcceleration = 0.0f;
            jumpedMag = 1.0f;
            grounded = false;
            jumped = false;
            state = State.None;
        }
    
        void Update() 
        {
            // 前後左右の速度
            var forward = transform.forward;
            var right = transform.right;
            var direction = forward * moveValue.y + right * moveValue.x;
            moveAcceleration = new Vector3(direction.x, 0, direction.z);
            velocity += moveAcceleration.normalized * magAcceleration * Time.deltaTime * dashMag  * jumpedMag;

            switch(state) 
            {
                case State.None:
                    velocity = Vector3.zero;
                    break;
                case State.Walk:
                case State.Dash:
                    var maxSpeed = walkSpeed * speedMag * jumpedMag;
                    if(velocity.sqrMagnitude > maxSpeed * maxSpeed) {
                        velocity = moveAcceleration.normalized * maxSpeed;
                    }
                    break;
                default:
                    break;
            }

            // 上下方向の速度
            if (!jumped && grounded) 
            {
                velocity.y = 0.0f;
            }
            else 
            {
                jumpAcceleration += (gravity * Time.deltaTime);
                velocity.y += jumpAcceleration;
            }
            // 動かす
            controller.Move(velocity * Time.deltaTime);

            CheckGrounded();
        }

        private void CheckGrounded() 
        {
            var endPoint = groundCheck.position + (-groundCheck.up * groundDistance);
            grounded = Physics.Linecast(groundCheck.position, endPoint, groundMask);
            if(jumped && grounded && controller.velocity.y < 0) 
            {
                ExitJump();
            }
        }

        void ExitJump() 
        {
            jumped = false;
            jumpAcceleration = 0.0f;
            jumpedMag = 1.0f;
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

        private void Jump() 
        {
            if(!jumped) 
            {
                jumped = true;
                jumpAcceleration = jumpSpeed;
                jumpedMag = 0.5f;
            }
        }
    }
}
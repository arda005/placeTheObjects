using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CaseProject
{
    /// <summary>
    /// Handles movement of the FPS character.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementManager : MonoBehaviour, IRestartable
    {
        public static CharacterMovementManager Instance { get; private set; }

        private CharacterController controller;

        /// <summary>
        /// Current speed of the character.
        /// </summary>
        public Vector3 CurrentSpeed { get; private set; }

        /// <summary>
        /// If player jumped and still rising up.
        /// </summary>
        private bool isGoingUp = false;

        /// <summary>
        /// Initial position of the character. We are using it for restarting the game.
        /// </summary>
        private Vector3 firstPosition;

        #region UNITY_INSPECTOR
        /// <summary>
        /// Movement speed of the character.
        /// </summary>
        [SerializeField] private float speed = 0.2f;

        /// <summary>
        /// Jump speed of the character.
        /// </summary>
        [SerializeField] private float jumpSpeed = 5f;

        /// <summary>
        /// Gravity for the character
        /// </summary>
        [SerializeField] private float gravity = 0.2f;

        /// <summary>
        /// The maximum distance for ground check.
        /// </summary>
        [SerializeField] private float groundCheckDistance = 0.2f;

        /// <summary>
        /// We are using this offset for able to jump before character
        /// touch the ground. It is small offset but if we dont use it
        /// players think game restricting their moves.
        /// </summary>
        [SerializeField] private float groundCheckOffset = 0.3f;

        /// <summary>
        /// The point for calculating if player hit floor.
        /// </summary>
        [SerializeField] private Transform groundCheck;
        #endregion

        private void Awake()
        {
            Init();
        }

        /// <summary>
        /// Does first assignments and starts the class.
        /// </summary>
        private void Init()
        {
            Instance = this;
            controller = GetComponent<CharacterController>();
            firstPosition = transform.position;
        }

        void Start()
        {

        }

        void Update()
        {
            if (GameManager.Instance.IsPaused) return;
            GravityUpdate();
            SetMove();
        }

        /// <summary>
        /// Updates movement of the character.
        /// </summary>
        private void SetMove()
        {
            float verticalSpeedTemp = CurrentSpeed.y;
            CurrentSpeed = GetMoveInput().normalized * speed;
            CurrentSpeed = new Vector3(CurrentSpeed.x, verticalSpeedTemp, CurrentSpeed.z);
            controller.Move(CurrentSpeed);
        }

        /// <summary>
        /// Returns direction for the movement.
        /// </summary>
        /// <returns>The direction of the input.</returns>
        private Vector3 GetMoveInput()
        {
            var moveAmount = Vector3.zero;
            if (Keyboard.current.wKey.isPressed)
            {
                moveAmount += transform.forward;
            }

            if (Keyboard.current.sKey.isPressed)
            {
                moveAmount -= transform.forward;
            }

            if (Keyboard.current.dKey.isPressed)
            {
                moveAmount += transform.right;
            }

            if (Keyboard.current.aKey.isPressed)
            {
                moveAmount -= transform.right;
            }

            return moveAmount;
        }

        /// <summary>
        /// Updates gravity.
        /// </summary>
        private void GravityUpdate()
        {
            var hitDistance = IsGrounded();

            //If character not hitting floor.
            if (hitDistance > groundCheckDistance)
            {
                isGoingUp = false;
                CurrentSpeed = new Vector3(CurrentSpeed.x, CurrentSpeed.y - gravity * 0.001f, CurrentSpeed.z);
            }

            //We are letting players jump if ground check distance less than
            //"groundCheckDistance" + "groundCheckOffset" but we are setting their vertical speed 0 if they are closer
            //the floor than "groundCheckDistance". It gives extra time to jump to players and
            //makes gameplay more smooth.
            if (hitDistance < groundCheckDistance + groundCheckOffset)
            {
                bool isJumped = JumpCheck();
                if (!isJumped)
                {
                    if (!isGoingUp && hitDistance < groundCheckDistance)
                    {
                        CurrentSpeed = new Vector3(CurrentSpeed.x, 0, CurrentSpeed.z);
                    }
                }
            }
        }

        /// <summary>
        /// Returns if character hits the floor.
        /// </summary>
        /// <returns>If character hits the floor.</returns>
        private float IsGrounded()
        {
            var didHit = Physics.Raycast(groundCheck.transform.position, -groundCheck.transform.up, out RaycastHit hit);
            if(didHit)
            {
                return hit.distance;
            }

            return Mathf.Infinity;
        }

        /// <summary>
        /// Calls every frame that players able to jump.
        /// </summary>
        /// <returns>Did player jump</returns>
        private bool JumpCheck()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                isGoingUp = true;
                CurrentSpeed = new Vector3(CurrentSpeed.x, jumpSpeed * 0.001f, CurrentSpeed.z); ;
                return true;
            }

            return false;
        }

        public void OnRestart()
        {
            transform.position = firstPosition;
        }
    }
}
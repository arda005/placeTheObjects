using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CaseProject
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementManager : MonoBehaviour
    {
        public static CharacterMovementManager Instance { get; private set; }

        private CharacterController controller;

        public Vector3 CurrentSpeed { get; private set; }

        private bool justJumped = false;



        #region UNITY_INSPECTOR
        [SerializeField] private float speed = 0.2f;
        [SerializeField] private float jumpSpeed = 5f;
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

        private void SetMove()
        {
            float verticalSpeedTemp = CurrentSpeed.y;
            CurrentSpeed = GetMoveAmount().normalized * speed;
            CurrentSpeed = new Vector3(CurrentSpeed.x, verticalSpeedTemp, CurrentSpeed.z);
            controller.Move(CurrentSpeed);
        }

        private Vector3 GetMoveAmount()
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

        private void GravityUpdate()
        {
            var hitDistance = IsGrounded();

            if (hitDistance > groundCheckDistance)
            {
                justJumped = false;
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
                    if (!justJumped && hitDistance < groundCheckDistance)
                    {
                        CurrentSpeed = new Vector3(CurrentSpeed.x, 0, CurrentSpeed.z);
                    }
                }
            }
        }

        private float IsGrounded()
        {
            var didHit = Physics.Raycast(groundCheck.transform.position, -groundCheck.transform.up, out RaycastHit hit);
            if(didHit)
            {
                return hit.distance;
            }

            return Mathf.Infinity;
        }

        private bool JumpCheck()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                justJumped = true;
                CurrentSpeed = new Vector3(CurrentSpeed.x, jumpSpeed * 0.001f, CurrentSpeed.z); ;
                return true;
            }

            return false;
        }
    }
}
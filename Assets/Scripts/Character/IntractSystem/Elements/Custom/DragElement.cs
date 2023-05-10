using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.Intractable
{
    /// <summary>
    /// The objects that player can drag or lift up.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class DragElement : IntractableElement
    {
        /// <summary>
        /// Rigidbody component of the object.
        /// </summary>
        public Rigidbody ElementRigidbody { get; protected set; }

        /// <summary>
        /// BoxCollider component of the object.
        /// </summary>
        private BoxCollider boxCollider;

        /// <summary>
        /// The multiplier for the the force players apply the intrancting objects
        /// with the movement of their characters.
        /// </summary>
        private const float movementForceMultiplier = 550f;

        /// <summary>
        /// The multiplier for the the force players apply the intrancting objects
        /// with the rotation of the camera of ther characters.
        /// </summary>
        private const float rotationForceMultiplier = 8f;

        protected override void Awake()
        {
            base.Awake();
            ElementRigidbody = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();
            IsHoldable = true;
        }

        protected override void Start()
        {
            base.Start();
            var gameManager = GameManager.Instance;
            gameManager.OnGamePaused.AddListener(() => { ElementRigidbody.isKinematic = true; });
            gameManager.OnGameUnpaused.AddListener(() => { ElementRigidbody.isKinematic = false; });
        }

        public override void BeginIntract()
        {
            base.BeginIntract();

            boxCollider.enabled = false;

            if (IsHoldable)
                ElementRigidbody.isKinematic = true;
        }

        public override void EndIntract()
        {
            base.EndIntract();

            boxCollider.enabled = true;

            if (IsHoldable)
            {
                ElementRigidbody.isKinematic = false;

                ApplyForceAfterIntract();
            }
        }

        /// <summary>
        /// Applys force the holding object after intract with movement
        /// speed of the character and angular speed of the camera of the character.
        /// Note: We are not actually applying force we are just adding velocity.
        /// </summary>
        private void ApplyForceAfterIntract()
        {
            var movementManager = CharacterMovementManager.Instance;
            var rotationManager = CharacterCameraManager.Instance;

            ElementRigidbody.velocity = movementManager.CurrentSpeed * movementForceMultiplier;

            ElementRigidbody.velocity += (movementManager.transform.right *
                rotationManager.CurrentRotationSpeed.y * rotationForceMultiplier) +
                 (rotationManager.CharacterCamera.transform.up *
                rotationManager.CurrentRotationSpeed.x * rotationForceMultiplier);
        }
    }
}
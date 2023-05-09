using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.Intractable
{
    [RequireComponent(typeof(Rigidbody))]
    public class DragElement : IntractableElement
    {
        public Rigidbody ElementRigidbody { get; protected set; }

        private BoxCollider boxCollider;

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

                ElementRigidbody.velocity = CharacterMovementManager.Instance.CurrentSpeed * 550f;

                ElementRigidbody.velocity += (CharacterCameraManager.Instance.transform.right *
                    CharacterCameraManager.Instance.CurrentRotationSpeed.y * 8f) +
                     (CharacterCameraManager.Instance.CharacterCamera.transform.up *
                    CharacterCameraManager.Instance.CurrentRotationSpeed.x * 8f);
            }
        }
    }
}
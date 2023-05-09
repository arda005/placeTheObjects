using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CaseProject
{
    public class CharacterCameraManager : MonoBehaviour
    {
        public static CharacterCameraManager Instance { get; private set; }

        public Camera CharacterCamera { get; private set; }

        public Vector3 CurrentRotationSpeed { get; private set; }

        #region UNITY_INSPECTOR
        [SerializeField] private float mouseSensivity = 0.15f;
        #endregion

        private void Awake()
        {
            Instance = this;
            CharacterCamera = GetComponentInChildren<Camera>();
        }

        void Start()
        {
            GameManager.Instance.OnGameStarted.AddListener(() =>
            {
                Destroy(CharacterCamera.GetComponent<Animator>());
            });
        }

 
        void Update()
        {
            UpdateCharacterRotaion();
        }

        private void UpdateCharacterRotaion()
        {
            if (GameManager.Instance.IsPaused) return;

            SetXRotaion();
            SetYRotaion();
        }

        private void SetXRotaion()
        {
            var xRotationDelta = Mouse.current.delta.x.ReadValue() * mouseSensivity;

            transform.Rotate(0, xRotationDelta, 0);

            CurrentRotationSpeed = new Vector3(currentCameraRotation.x,
    xRotationDelta, currentCameraRotation.z);
        }

        /// <summary>
        /// We are using this variable for access raw rotation value of the camera.
        /// It is usefull for clamp the camera rotaion.
        /// </summary>
        private Vector3 currentCameraRotation;
        private void SetYRotaion()
        {
            var yRotationDelta = Mouse.current.delta.y.ReadValue() * mouseSensivity;
            var newYRotation = currentCameraRotation.x + yRotationDelta;

            currentCameraRotation = new Vector3(Mathf.Clamp(newYRotation, -90, 75),
           currentCameraRotation.y, currentCameraRotation.z);

            CurrentRotationSpeed = new Vector3(yRotationDelta,
                CurrentRotationSpeed.y, CurrentRotationSpeed.z);

            CharacterCamera.transform.localRotation = Quaternion.Euler(-currentCameraRotation.x, 0f, 0f);

        }
    }
}
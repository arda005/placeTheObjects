using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CaseProject
{
    /// <summary>
    /// Handles camera movement of the FPS character.
    /// </summary>
    public class CharacterCameraManager : MonoBehaviour, IRestartable
    {
        public static CharacterCameraManager Instance { get; private set; }

        /// <summary>
        /// Camera of the character.
        /// </summary>
        public Camera CharacterCamera { get; private set; }

        /// <summary>
        /// Current rotation speed for camera.
        /// </summary>
        public Vector3 CurrentRotationSpeed { get; private set; }

        /// <summary>
        /// Initital rotation values for character. We are using it for restarting the game.
        /// </summary>
        private Vector3 firstRotaion;

        /// <summary>
        /// Initital rotation values for character camera. We are using it for restarting the game.
        /// </summary>
        private Vector3 firstCameraRotaion;

        #region UNITY_INSPECTOR
        /// <summary>
        /// Mouse sensivity for camera rotation.
        /// </summary>
        [SerializeField] private float mouseSensivity = 0.15f;
        #endregion

        private void Awake()
        {
            Instance = this;
            CharacterCamera = GetComponentInChildren<Camera>();
            firstRotaion = transform.eulerAngles;
            firstCameraRotaion = CharacterCamera.transform.eulerAngles;
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

        /// <summary>
        /// Updates rotation of camera every frame.
        /// </summary>
        private void UpdateCharacterRotaion()
        {
            if (GameManager.Instance.IsPaused) return;

            SetXRotaion();
            SetYRotaion();
        }

        /// <summary>
        /// Sets the rotation with X axis of the mouse.
        /// </summary>
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

        /// <summary>
        /// Sets the rotation with Y axis of the mouse.
        /// </summary>
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

        public void OnRestart()
        {
            transform.eulerAngles = firstRotaion;
            CharacterCamera.transform.eulerAngles = firstCameraRotaion;
        }
    }
}
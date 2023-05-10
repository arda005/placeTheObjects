using CaseProject.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

namespace CaseProject.Intractable
{
    /// <summary>
    /// Controls player's intraction with intractable objects in the game world.
    /// </summary>
    public class CharacterIntractManager : MonoBehaviour
    {
        /// <summary>
        /// The object that player currenlt intraction. Exmp: The objects that player holding.
        /// </summary>
        public IntractableElement CurrentIntractedObject { get; private set; } = null;

        /// <summary>
        /// The distance that player can hold the objects.
        /// </summary>
        private float maxIntractDistance = 3f;

        /// <summary>
        /// After player hold the objects how far they are staying.
        /// It is not the distance that player intract object with!
        /// </summary>
        private float maxHoldingDistance = 1.25f;

        /// <summary>
        /// When players holding an object and get close the obstacles such
        /// as walls the intracted object will be behind the wall eventually.
        /// This is the distance that how can intracted object go far from camera.
        /// Can`t be higher than maxHoldingDistance.
        /// </summary>
        public float currentHoldingDistance;

        /// <summary>
        /// Crosshair manager.
        /// </summary>
        private CrosshairModel crosshairModel;

        private void Awake()
        {
            crosshairModel = FindObjectOfType<CrosshairModel>();
        }

        void Start()
        {

        }

        void Update()
        {
            if (GameManager.Instance.IsPaused) return;

            DrawRay();
            CheckCurrentIntractedObject();
        }

        /// <summary>
        /// Draws Raycast and calls related functions for intracting.
        /// </summary>
        private void DrawRay()
        {
            var chracterCameraTr = CharacterCameraManager.Instance.CharacterCamera.transform;

            var allHits = Physics.RaycastAll(chracterCameraTr.position,
                chracterCameraTr.forward);

            currentHoldingDistance = maxHoldingDistance;
            foreach(var hit in allHits)
            {
                var intractableElement = hit.transform.GetComponent<IntractableElement>();
                var isIntractable = intractableElement != null;

                if (isIntractable)
                {
                    bool intract = CurrentIntractedObject == null &&
                        Mouse.current.leftButton.wasPressedThisFrame;

                    if (intract)
                        SetIntractedObject(intractableElement);
                }
                else
                {
                    var distance = Vector3.Distance(chracterCameraTr.position, hit.point);
                    if (distance < currentHoldingDistance)
                        currentHoldingDistance = distance;
                }
            }

            HoldCurrentObject();
        }

        /// <summary>
        /// If player incracting with an object right not, this function
        /// either ends it or updates intracting state.
        /// </summary>
        private void CheckCurrentIntractedObject()
        {
            if (CurrentIntractedObject == null) return;

            if(Mouse.current.leftButton.wasReleasedThisFrame)
            {
                TryDropCurrentObject();
            }
            else
            {
                UpdateCurrentIntractedElement();
            } 
        }

        /// <summary>
        /// If player is holding an intracting objects ends intracting with it
        /// </summary>
        public void TryDropCurrentObject()
        {
            if (CurrentIntractedObject == null) return;

            CurrentIntractedObject.EndIntract();
            CurrentIntractedObject = null;

            crosshairModel.SetCrossHairActive(true);
        }

        /// <summary>
        /// Sets the currently intracting object.
        /// </summary>
        /// <param name="intractableElement">Object to set</param>
        public void SetIntractedObject(IntractableElement intractableElement)
        {
            if (!intractableElement.IsIntractable)
                return;

            CurrentIntractedObject = intractableElement;
            intractableElement.BeginIntract();

            crosshairModel.SetCrossHairActive(false);
        }

        /// <summary>
        /// Updates currently intracting objects for this frame.
        /// </summary>
        private void UpdateCurrentIntractedElement()
        {
            CurrentIntractedObject.OnIntract();
        }

        /// <summary>
        /// Keeps currently interacting objects at a distance from the camera.
        /// </summary>
        private void HoldCurrentObject()
        {
            if (CurrentIntractedObject == null)
                return;

            if (!CurrentIntractedObject.IsHoldable)
                return;

            var chracterCameraTr = CharacterCameraManager.Instance.CharacterCamera.transform;

            CurrentIntractedObject.transform.position = chracterCameraTr.position +
                    chracterCameraTr.forward * currentHoldingDistance;
        }
    }
}
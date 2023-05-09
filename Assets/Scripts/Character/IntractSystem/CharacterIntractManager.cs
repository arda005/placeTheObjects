using CaseProject.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

namespace CaseProject.Intractable
{
    public class CharacterIntractManager : MonoBehaviour
    {
        public IntractableElement CurrentIntractedObject { get; private set; } = null;

        private CrosshairModel crosshairModel;

        private float maxDistance = 1.25f;
        public float currentDistance;

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

        private void DrawRay()
        {
            var chracterCameraTr = CharacterCameraManager.Instance.CharacterCamera.transform;

            var allHits = Physics.RaycastAll(chracterCameraTr.position,
                chracterCameraTr.forward);

            currentDistance = maxDistance;
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
                    if (distance < currentDistance)
                        currentDistance = distance;
                }
            }

            HoldCurrentObject();
        }

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

        public void TryDropCurrentObject()
        {
            if (CurrentIntractedObject == null) return;

            CurrentIntractedObject.EndIntract();
            CurrentIntractedObject = null;

            crosshairModel.SetCrossHairActive(true);
        }

        public void SetIntractedObject(IntractableElement intractableElement)
        {
            if (!intractableElement.IsIntractable)
                return;

            CurrentIntractedObject = intractableElement;
            intractableElement.BeginIntract();

            crosshairModel.SetCrossHairActive(false);
        }

        private void UpdateCurrentIntractedElement()
        {
            CurrentIntractedObject.OnIntract();
        }

        private void HoldCurrentObject()
        {
            if (CurrentIntractedObject == null)
                return;

            if (!CurrentIntractedObject.IsHoldable)
                return;

            var chracterCameraTr = CharacterCameraManager.Instance.CharacterCamera.transform;

            CurrentIntractedObject.transform.position = chracterCameraTr.position +
                    chracterCameraTr.forward * currentDistance;
        }
    }
}
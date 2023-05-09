using CaseProject.Intractable;
using CaseProject.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.Level
{
    public class FirstLevelTargetObjects : DragElement
    {
        public bool IsInTarget { get; private set; }

        [SerializeField] private TargetObjectInformation TargetObjectInformation;

        [SerializeField] private FirstLevelTargetObjects other;

        private Vector3 firsPosition;

        [SerializeField] private float minHeight = -5f;

        protected override void Awake()
        {
            base.Awake();
            firsPosition = transform.position;
        }

        protected override void Update()
        {
            base.Update();

            if(minHeight > transform.position.y && IsIntractable)
            {
                StartCoroutine(ReturnFirstPosition());
            }
        }

        public override void BeginIntract()
        {
            base.BeginIntract();
            UpdateCurrentTargetInformationUI(TargetObjectInformation);
        }

        public override void EndIntract()
        {
            base.EndIntract();
            UpdateCurrentTargetInformationUI(null);
        }

        private void UpdateCurrentTargetInformationUI(TargetObjectInformation targetObjectInformation)
        {
            var targetInfoController = FindAnyObjectByType<CurrentObjectInformationController>();
            if (targetInfoController == null) return;
            
            var targetInfoModel = (CurrentObjectInformationModel)targetInfoController.Model;
            targetInfoModel.SetTargetInformation(targetObjectInformation);
            targetInfoController.UpdateView();
        }

        public void OnCollisionEnter(Collision collision)
        {
            var targetArea = collision.gameObject.GetComponent<FirstLevelTargetArea>();
            if (targetArea == null) return;

            if (IsInTarget) return;

            IsInTarget = true;

            if (other.IsInTarget)
                StartCoroutine(other.ReturnFirstPosition());

            FindObjectOfType<FirstLevelLogic>().IncreasePlacedObjectCount();
        }

        public void OnCollisionExit(Collision collision)
        {
            var targetArea = collision.gameObject.GetComponent<FirstLevelTargetArea>();
            if (targetArea == null) return;

            if (!IsInTarget) return;

            IsInTarget = false;

            FindObjectOfType<FirstLevelLogic>().DecreasePlacedObjectCount();
        }

        #region RETURN_ANIMATION_PROPERTIES
        
        //Durations
        private const float returnAnimFastDuration = 0.1f;
        private const float returnAnimNormalDuration = 0.25f;
        private const float returnAnimSlowDuration = 0.5f;

        //Move
        private const float returnAnimFirstRiseUpAmount = 0.5f;
        private const float returnAnimSecondRiseUpAmount = 0.1f;
        private const float returnAnimComeBackAmount = 0.15f;
        private const float returnAnimDropHeight = 0.5f;

        //Scale
        private const float returnAnimScaleUpRatio = 1.5f;
        private const float returnAnimScaleDownRatio = 0f;
        #endregion
        public IEnumerator ReturnFirstPosition()
        {
            IsIntractable = false;
            ElementRigidbody.isKinematic = true;

            var firstScale = transform.localScale;

            yield return transform.DOMoveY(transform.position.y + returnAnimFirstRiseUpAmount, returnAnimNormalDuration).WaitForCompletion();
            transform.DOMoveY(transform.position.y + returnAnimSecondRiseUpAmount, returnAnimNormalDuration);
            yield return transform.DOScale(firstScale * returnAnimScaleUpRatio, returnAnimNormalDuration).WaitForCompletion();
            transform.DOMoveY(transform.position.y - returnAnimComeBackAmount, returnAnimFastDuration);
            yield return transform.DOScale(firstScale * returnAnimScaleDownRatio, returnAnimFastDuration).WaitForCompletion();

            transform.position = firsPosition + Vector3.up * returnAnimDropHeight;
            transform.localScale = firstScale;
            yield return transform.DOMove(firsPosition, returnAnimNormalDuration).WaitForCompletion();
     

            IsIntractable = true;
            ElementRigidbody.isKinematic = false;
           }
    }
}
using CaseProject.Audio;
using CaseProject.Intractable;
using CaseProject.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.Level
{
    /// <summary>
    /// The objects that players place for the first level.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class FirstLevelTargetObject : DragElement, IRestartable
    {
        private AudioSource audioSource;

        /// <summary>
        /// If this pbject seleclted before.
        /// </summary>
        public bool IsSelected { get; private set; } = false;

        /// <summary>
        /// If this object or the other pair of this object selected before.
        /// </summary>
        public bool IsThisOrOtherSelected
        {
            get
            {
                return IsSelected || other.IsSelected;
            }
        }

        /// <summary>
        /// If this object in the target area.
        /// </summary>
        public bool IsInTarget { get; private set; }

        /// <summary>
        /// Initial position of the object. We are using this for restarting the game.
        /// </summary>
        private Vector3 firsPosition;

        /// <summary>
        /// Initial rotation of the object. We are using this for restarting the game.
        /// </summary>
        private Vector3 firsRotation;

        #region UNITY_INSPECTOR
        /// <summary>
        /// The scriptable object that keeps information about this object.
        /// </summary>
        [Header("Target Object")]
        [SerializeField] internal TargetObjectInformation TargetObjectInformation;

        /// <summary>
        /// The other pair of this object.
        /// </summary>
        [SerializeField] protected FirstLevelTargetObject other;

        /// <summary>
        /// The minumum height that object can go down.
        /// If the objects goes down lower than this, it will go back 
        /// it`s first position. We are using this for preventing 
        /// players stuck in same level because they threw away target objects.
        /// </summary>
        private const float minHeight = -5f;
        #endregion

        protected override void Awake()
        {
            base.Awake();

            audioSource = GetComponent<AudioSource>();

            firsPosition = transform.position;
            firsRotation = transform.eulerAngles;
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
            SetInTarget(false);

            IsSelected = true;
        }

        public override void EndIntract()
        {
            base.EndIntract();
            UpdateCurrentTargetInformationUI(null);
        }

        /// <summary>
        /// Adds this objects information in target information UI
        /// </summary>
        /// <param name="targetObjectInformation">Target object information</param>
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

            SetInTarget(true);

        }

        public void OnCollisionExit(Collision collision)
        {
            var targetArea = collision.gameObject.GetComponent<FirstLevelTargetArea>();
            if (targetArea == null) return;

            SetInTarget(false);
        }

        /// <summary>
        /// Set object in target.
        /// </summary>
        /// <param name="inTarget">in target or not.</param>
        private void SetInTarget(bool inTarget)
        {
            if (IsInTarget == inTarget) return;

            IsInTarget = inTarget;

            if (inTarget)
                PlacedInTargetEvents();
            else
                RemovedFromTargetEvents();
        }

        /// <summary>
        /// Call back for placing in target.
        /// </summary>
        private void PlacedInTargetEvents()
        {
            var firsLevel = FindObjectOfType<FirstLevelLogic>();
            var placedObjectsView = FindObjectOfType<PlacedObjectsView>(true);

            if (other.IsInTarget)
                StartCoroutine(other.ReturnFirstPosition());

            firsLevel.IncreasePlacedObjectCount();
            placedObjectsView.AddElement(this, other);

            AudioManager.Instance.PlayPlacedInTartegClip(audioSource);
        }

        /// <summary>
        /// Call back for removing from target.
        /// </summary>
        private void RemovedFromTargetEvents()
        {
            var firsLevel = FindObjectOfType<FirstLevelLogic>();
            var placedObjectsView = FindObjectOfType<PlacedObjectsView>(true);

            firsLevel.DecreasePlacedObjectCount();
            placedObjectsView.RemoveElement(TargetObjectInformation);
        }

        ///Animation proporties for return animation.
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
        /// <summary>
        /// Return first position animation
        /// </summary>
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

        /// <summary>
        /// Returns target object information for this object.
        /// </summary>
        /// <returns></returns>
        public TargetObjectInformation GetTargetInformation()
        {
            return TargetObjectInformation;
        }

        public void OnRestart()
        {
            transform.position = firsPosition;
            transform.eulerAngles = firsRotation;
            IsSelected = false;
            IsInTarget = false;
        }
    }
}
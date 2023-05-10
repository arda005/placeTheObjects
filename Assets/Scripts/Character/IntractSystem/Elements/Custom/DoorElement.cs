using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.Intractable
{
    /// <summary>
    /// The door that end the level.
    /// </summary>
    public class DoorElement : IntractableElement, IRestartable
    {
        /// <summary>
        /// How much going to rotate when it is opened.
        /// </summary>
        private const float DoorOpenAmount = 20f;

        /// <summary>
        /// The animation duration of the door during opening rotation.
        /// </summary>
        private const float DoorAnimDuration = 0.5f;

        /// <summary>
        /// Initial rotation of the door. 
        /// We are using it when restarting the game.
        /// </summary>
        private Vector3 firstRotation;

        protected override void Awake()
        {
            base.Awake();
            firstRotation = transform.localEulerAngles;
        }

        public override void EndIntract()
        {
            var endRotaion = new Vector3(
                transform.localEulerAngles.x,
                transform.localEulerAngles.y, DoorOpenAmount);

            transform.DORotate(endRotaion, DoorAnimDuration).
                onComplete = EndIntractAfterAnimation;
        }

        public void OnRestart()
        {
            transform.eulerAngles = firstRotation;
        }

        /// <summary>
        /// The callback of the EndIntract function.
        /// </summary>
        private void EndIntractAfterAnimation()
        {
            base.EndIntract();
            GameManager.Instance.PassCurrentLevel();
        }
    }
}
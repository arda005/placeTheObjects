using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    /// <summary>
    /// Model of crosshair.
    /// </summary>
    public class CrosshairModel : BaseModel
    {
        /// <summary>
        /// Radius of crosshair.
        /// </summary>
        public float Radius { get; protected set; } = 50f;
        
        /// <summary>
        /// Alpha of crosshair.
        /// </summary>
        public float Alpha { get; protected set; } = 0.5f;

        protected override void Start()
        {
            base.Start();

            var gameManager = GameManager.Instance;
            gameManager.OnGamePaused.AddListener(OnPause);
            gameManager.OnGameUnpaused.AddListener(OnUnpause);

            SetCrossHairActive(true);
        }

        /// <summary>
        /// Sets radius of crosshair.
        /// </summary>
        private void SetRadiues(float newRadius)
        {
            Radius = newRadius;
        }

        /// <summary>
        /// Sets alpha of crosshair.
        /// </summary>
        private void SetAlpha(float newAlpha)
        {
            Alpha = newAlpha;
        }

        /// <summary>
        /// Sets crosshair active or deactive.
        /// </summary>
        public void SetCrossHairActive(bool isActive)
        {
            if (isActive)
            {
                SetAlpha(0.5f);
                SetRadiues(25);
                Controller.UpdateView();
            }
            else
            {
                SetAlpha(1f);
                SetRadiues(15);
                Controller.UpdateView();
            }
        }

        private void OnPause()
        {
            SetRadiues(0);
            SetAlpha(0f);
            Controller.UpdateView();
        }

        private void OnUnpause()
        {
            SetCrossHairActive(true);
            Controller.UpdateView();
        }
    }
}
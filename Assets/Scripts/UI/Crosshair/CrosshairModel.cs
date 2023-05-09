using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    public class CrosshairModel : BaseModel
    {
        public float Radius { get; protected set; } = 50f;
        public float Alpha { get; protected set; } = 0.5f;

        protected override void Start()
        {
            base.Start();

            var gameManager = GameManager.Instance;
            gameManager.OnGamePaused.AddListener(OnPause);
            gameManager.OnGameUnpaused.AddListener(OnUnpause);

            SetCrossHairActive(true);
        }

        private void SetRadiues(float newRadius)
        {
            Radius = newRadius;
        }

        private void SetAlpha(float newAlpha)
        {
            Alpha = newAlpha;
        }

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
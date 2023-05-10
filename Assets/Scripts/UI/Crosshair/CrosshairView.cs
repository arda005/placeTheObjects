using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace CaseProject.UI
{
    /// <summary>
    /// View of crosshair.
    /// </summary>
    public class CrosshairView : BaseView
    {
        /// <summary>
        /// The radius animation duration on radius change.
        /// </summary>
        private const float radiusAnimationDuration = 0.25f;
        
        /// <summary>
        /// Updates radius of crosshair.
        /// </summary>
        public void UpdateRadius(float radius)
        {
            viewRectTransform.DOSizeDelta(Vector2.one * radius, radiusAnimationDuration);
        }

        /// <summary>
        /// The alpha animation duration on alpha change.
        /// </summary>
        private const float alphaAnimationDuration = 0.25f;

        /// <summary>
        /// Updates alpha of crosshair.
        /// </summary>
        public void UpdateAlpha(float alpha)
        {
            var image = GetComponent<Image>();
            image.DOFade(alpha, alphaAnimationDuration);
        }
    }
}

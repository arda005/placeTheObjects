using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace CaseProject.UI
{
    public class CrosshairView : BaseView
    {
        private const float radiusAnimationDuration = 0.25f;
        public void UpdateRadius(float radius)
        {
            viewRectTransform.DOSizeDelta(Vector2.one * radius, radiusAnimationDuration);
        }

        private const float alphaAnimationDuration = 0.25f;
        public void UpdateAlpha(float alpha)
        {
            var image = GetComponent<Image>();
            image.DOFade(alpha, alphaAnimationDuration);
        }
    }
}

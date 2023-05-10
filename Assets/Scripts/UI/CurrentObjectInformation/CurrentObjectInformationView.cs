using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CaseProject.UI
{
    /// <summary>
    /// View for Current Object Information Area
    /// </summary>
    public class CurrentObjectInformationView : BaseView
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text titleText;

        [Header("Active Values")]
        [SerializeField] private Color ActiveColor;

        [Header("Deactive Values")]
        [SerializeField] private Sprite deactiveSprite;
        [SerializeField] private Color DeactiveColor;

        protected override void Awake()
        {
            base.Awake();
            iconImage.preserveAspect = true;
        }

        /// <summary>
        /// Updates Icon
        /// </summary>
        public void UpdateIcon(Sprite iconSprite)
        {
            iconImage.sprite = iconSprite;
            iconImage.color = ActiveColor;
        }

        /// <summary>
        /// Updates Title
        /// </summary>
        public void UpdateTitleText(string title)
        {
            titleText.text = title;
        }


        /// <summary>
        /// Clears UI
        /// </summary>
        public void ClearUI()
        {
            iconImage.sprite = deactiveSprite;
            iconImage.color = DeactiveColor;
            titleText.text = string.Empty;
        }
    }
}
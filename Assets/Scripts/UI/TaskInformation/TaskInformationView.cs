using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CaseProject.UI
{
    /// <summary>
    /// View for tast information UI.
    /// </summary>
    public class TaskInformationView : BaseView
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;

        /// <summary>
        /// Updates title and descreption texts.
        /// </summary>
        public void UpdateTexts(string title, string decreption)
        {
            titleText.text = title;
            descriptionText.text = decreption;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CaseProject.UI
{
    public class TaskInformationView : BaseView
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;

        public void UpdateTexts(string title, string decreption)
        {
            titleText.text = title;
            descriptionText.text = decreption;
        }
    }
}
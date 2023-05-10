using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CaseProject.UI
{
    /// <summary>
    /// View for convarsation pop ups.
    /// A conversation pop up contains one title and descpreption and a button.
    /// It may conation another button if the information of this button specified.
    /// </summary>
    public class ConversationPopupView : PopupView
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descreptionText;

        [SerializeField] private AdvancedButton acceptButton;
        [SerializeField] private AdvancedButton cancelButton;

        /// <summary>
        /// Updates title.
        /// </summary>
        public void UpdateTitle(string title)
        {
            titleText.text = title;
        }

        /// <summary>
        /// Udpates descreption.
        /// </summary>
        public void UpdateDescreption(string descreption)
        {
            descreptionText.text = descreption;
        }

        /// <summary>
        /// Updates accept button.
        /// </summary>
        public void UpdateAcceptButton(string text, UnityEvent onClick)
        {
            UpdateButton(acceptButton, text, onClick);
        }

        /// <summary>
        /// Updates cancel button.
        /// </summary>
        public void UpdateCancelButton(string text, UnityEvent onClick)
        {
            UpdateButton(cancelButton, text, onClick);
        }

        /// <summary>
        /// Updates a button.
        /// </summary>
        private void UpdateButton(AdvancedButton button , string text, UnityEvent onClick)
        {
            if(onClick == null)
            {
                button.gameObject.SetActive(false);
                return;
            }

            button.SetText(text);
            button.OnClick.AddListener(() => { onClick.Invoke(); });
        }
    }
}
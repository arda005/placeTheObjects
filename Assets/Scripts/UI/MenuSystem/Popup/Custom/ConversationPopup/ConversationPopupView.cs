using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CaseProject.UI
{
    public class ConversationPopupView : PopupView
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descreptionText;

        [SerializeField] private AdvancedButton acceptButton;
        [SerializeField] private AdvancedButton cancelButton;

        public void UpdateTitle(string title)
        {
            titleText.text = title;
        }

        public void UpdateDescreption(string descreption)
        {
            descreptionText.text = descreption;
        }

        public void UpdateAcceptButton(string text, UnityEvent onClick)
        {
            UpdateButton(acceptButton, text, onClick);
        }

        public void UpdateCancelButton(string text, UnityEvent onClick)
        {
            UpdateButton(cancelButton, text, onClick);
        }

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CaseProject.UI
{
    public class ConversationPopupModel : PopupModel
    {
        public string Title { get; protected set; }
        public string Descreption { get; protected set; }
 
        public string AcceptButtonText { get; protected set; }
        protected const string defaultAcceptText = "Okay";
        public UnityEvent OnAccept { get; protected set; }  = new UnityEvent();

        public string CancelButtonText { get; protected set; }
        protected const string defaultCancelText = "Cancel";
        public UnityEvent OnCancel { get; protected set; } = new UnityEvent();

        public void Init(string title, string descreption, UnityEvent onAccept, UnityEvent onCancel, string acceptButtonText, string cancelButtonText)
        {
            Title = title;
            Descreption = descreption;
            AcceptButtonText = acceptButtonText;
            OnAccept = onAccept;
            CancelButtonText = cancelButtonText;
            OnCancel = onCancel;
        }

        public void Init(string title, string descreption, UnityEvent onAccept, UnityEvent onCancel)
        {
            Init(title, descreption, onAccept, onCancel, defaultAcceptText, defaultCancelText);
        }

        public void Init(string title, string descreption, UnityEvent onAccept)
        {
            Init(title, descreption, onAccept, null, defaultAcceptText, defaultCancelText);
        }

        public void Init(string title, string descreption)
        {
            Init(title, descreption, OnAccept, null, defaultAcceptText, defaultCancelText);
        }
    }
}
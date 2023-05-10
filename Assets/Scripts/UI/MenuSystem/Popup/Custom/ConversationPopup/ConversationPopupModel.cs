using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CaseProject.UI
{
    /// <summary>
    /// Model for convarsation pop ups.
    /// A conversation pop up contains one title and descpreption and a button.
    /// It may conation another button if the information of this button specified.
    /// </summary>
    public class ConversationPopupModel : PopupModel
    {
        /// <summary>
        /// Title of the pop up
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// Descreption of the pop up.
        /// </summary>
        public string Descreption { get; protected set; }
 
        /// <summary>
        /// The text of accept button.
        /// </summary>
        public string AcceptButtonText { get; protected set; }

        /// <summary>
        /// The default text of accept button. It will be used
        /// if text will not be specified.
        /// </summary>
        protected const string defaultAcceptText = "Okay";

        /// <summary>
        /// Callbacks for accept button.
        /// </summary>
        public UnityEvent OnAccept { get; protected set; }  = new UnityEvent();

        /// <summary>
        /// The text of cancel button.
        /// </summary>
        public string CancelButtonText { get; protected set; }

        /// <summary>
        /// The default text of cancel button. It will be used
        /// if text will not be specified.
        /// </summary>
        protected const string defaultCancelText = "Cancel";

        /// <summary>
        /// Callbacks for cancel button.
        /// </summary>
        public UnityEvent OnCancel { get; protected set; } = new UnityEvent();

        /// <summary>
        /// Initiliazes the object.
        /// </summary>
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
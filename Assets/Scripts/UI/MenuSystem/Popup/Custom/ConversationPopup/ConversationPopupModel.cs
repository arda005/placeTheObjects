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
        public UnityAction OnAccept { get; protected set; }  = null;

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
        public UnityAction OnCancel { get; protected set; } = null;

        /// <summary>
        /// Initiliazes the object.
        /// </summary>
        public void Init(string title, string descreption, UnityAction onAccept, UnityAction onCancel, string acceptButtonText, string cancelButtonText)
        {
            Title = title;
            Descreption = descreption;
            AcceptButtonText = acceptButtonText;
            OnAccept = onAccept;
            CancelButtonText = cancelButtonText;
            OnCancel = onCancel;
        }

        public void Init(string title, string descreption, UnityAction onAccept, UnityAction onCancel)
        {
            Init(title, descreption, onAccept, onCancel, defaultAcceptText, defaultCancelText);
        }

        public void Init(string title, string descreption, UnityAction onAccept)
        {
            //We passed empty function for onCancel because if we pass null it will 
            //deactivate the button. We want to show it but have no other capabilty than
            //closing the menu.
            Init(title, descreption, onAccept, () => { }, defaultAcceptText, defaultCancelText);
        }

        public void Init(string title, string descreption)
        {
            Init(title, descreption, () => { }, null, defaultAcceptText, defaultCancelText);
        }
    }
}
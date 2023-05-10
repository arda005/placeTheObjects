using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    /// <summary>
    /// Controller for convarsation pop ups.
    /// A conversation pop up contains one title and descpreption and a button.
    /// It may conation another button if the information of this button specified.
    /// </summary>
    public class ConversationPopupController : PopupController
    {
        public override void UpdateView()
        {
            base.UpdateView();

            var popupView = (ConversationPopupView)View;
            var popupModel = (ConversationPopupModel)Model;

            popupView.UpdateTitle(popupModel.Title);
            popupView.UpdateDescreption(popupModel.Descreption);
            popupView.UpdateAcceptButton(popupModel.AcceptButtonText, popupModel.OnAccept);
            popupView.UpdateCancelButton(popupModel.CancelButtonText, popupModel.OnCancel);
        }
    }
}
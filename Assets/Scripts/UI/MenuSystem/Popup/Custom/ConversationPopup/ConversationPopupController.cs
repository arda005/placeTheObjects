using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
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
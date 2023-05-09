using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    public class MainMenuController : FullScreenController
    {
        public void StartGame()
        {
            GameManager.Instance.StartGame();
            Close();
        }

        public void OnClickAbout()
        {
            var popupController = MenuManager.Instance.OpenMenu<ConversationPopupController>(true);
            var popupModel = (ConversationPopupModel)popupController.Model;

            var model = (MainMenuModel)Model;

            popupModel.Init(model.aboutPopupTitle, model.aboutPopupDescription, model.aboutPopupOnAccepted);
        }
    }
}
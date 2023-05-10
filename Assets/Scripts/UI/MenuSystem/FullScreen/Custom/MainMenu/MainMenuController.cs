using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    /// <summary>
    /// Controller of main menu
    /// </summary>
    public class MainMenuController : FullScreenController
    {
        /// <summary>
        /// Starts the game.
        /// </summary>
        public void StartGame()
        {
            GameManager.Instance.StartGame();
            Close();
        }

        /// <summary>
        /// Opens about pop up menu.
        /// </summary>
        public void OnClickAbout()
        {
            var popupController = MenuManager.Instance.OpenMenu<ConversationPopupController>(true);
            var popupModel = (ConversationPopupModel)popupController.Model;

            var model = (MainMenuModel)Model;

            popupModel.Init(model.aboutPopupTitle, model.aboutPopupDescription, model.aboutPopupOnAccepted);
        }
    }
}
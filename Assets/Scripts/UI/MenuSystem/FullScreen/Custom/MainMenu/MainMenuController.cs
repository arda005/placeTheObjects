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

            popupModel.Init(model.aboutPopupTitle, model.aboutPopupDescription);
        }

        /// <summary>
        /// Opens the develepor`s linkedIn page.
        /// </summary>
        public void OnClickLinedInButton()
        {
            var model = (MainMenuModel)Model;
            Application.OpenURL(model.linkedInUrl);
        }

        /// <summary>
        /// When player clicks return to the dekstop button.
        /// Will be assigned in Unity Inspector.
        /// </summary>
        public void ReturnToDekstopButtonEvent()
        {
            var title = "Do you want to return to dektstop";
            var descreption = "Do you want to close the game end return to the dekstop?";

            var editorExplanation = string.Empty;

#if UNITY_EDITOR
            editorExplanation = "\nYou are playing the game in Unity Editor right now." +
                " When you accept this, you will exit playmode!";
#endif

            var exitPopup = MenuManager.Instance.OpenMenu<ConversationPopupController>();
            var exitPopupModel = (ConversationPopupModel)exitPopup.Model;
            exitPopupModel.Init(title, descreption + editorExplanation, ReturnToTheDekstopMenu);
        }

        private void ReturnToTheDekstopMenu()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }

        /// <summary>
        /// When player clicks return to the retart the game button.
        /// Will be assigned in Unity Inspector.
        /// </summary>
        public void RestarTheGameEvent()            // ************ INFORMATION! ************
        {                                           //We used almost the same function in
            GameManager.Instance.RestartTheGame();  //EndGameScreenController. It would be better
            Close();                                //if we have an object contains general functionalties
        }                                           //about the game like this. It would prevent code repating
    }                                               //there was no time to add this.
}
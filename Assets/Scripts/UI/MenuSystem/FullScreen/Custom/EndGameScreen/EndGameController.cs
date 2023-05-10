using CaseProject.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace CaseProject.UI
{
    /// <summary>
    /// Controller for end game screen.
    /// </summary>
    public class EndGameController : FullScreenController
    {
        public override void UpdateView()
        {
            base.UpdateView();
            SetTotalScore();
            CheckTargetObjects();
        }

        /// <summary>
        /// When player clicks return to the retart the game button.
        /// Will be assigned in Unity Inspector.
        /// </summary>
        public void RestarTheGameEvent()
        {
            GameManager.Instance.RestartTheGame();
            Close();
        }

        /// <summary>
        /// When player clicks return to the main menu button.
        /// Will be assigned in Unity Inspector.
        /// </summary>
        public void ReturnToTheMainMenuButtonEvent()
        {
            SceneManager.LoadScene(0);
        }

        /// <summary>
        /// Check target objects and set view with the their informations.
        /// </summary>
        private void CheckTargetObjects()
        {
            var firstLevelLogic = FindObjectOfType<FirstLevelLogic>();

            if (firstLevelLogic == null) return;
            var corrects = firstLevelLogic.CorrectObjects;

            var view = (EndGameView)View;

            foreach (var correct in corrects)
            {
                //If on of the target selected in related pair
                if (correct.IsThisOrOtherSelected)
                {
                    //Then we are cheking if correct one in the target.
                    if (correct.IsInTarget)
                        view.CreateResultElement(correct, true);
                    else //If not then it is not accaptable.
                        view.CreateResultElement(correct.Other, false);
                }
                else //If none of the objects selected in the pair it means we 
                {    //did not give or take score from user. But we are showing both of the objects with 0 score
                    view.CreateResultElement((FirstLevelTargetObject)correct); // in the end game screen.
                    view.CreateResultElement((FirstLevelTargetObject)correct.Other);
                }
            }
        }

        /// <summary>
        /// Sets total score.
        /// </summary>
        private void SetTotalScore()
        {
            var levelLogic = FindObjectOfType<LevelLogic>();
            if (levelLogic == null)
            {
                Debug.LogError("There is no level logic in scene!");
                return;
            }

            ((EndGameView)View).SetTotalScore(levelLogic.Score);
        }
    }
}
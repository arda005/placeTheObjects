using CaseProject.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        public void RestarTheGame()
        {
            GameManager.Instance.RestartTheGame();
            Close();
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

                if (correct.IsThisOrOtherSelected)
                {
                    if (correct.IsSelected)
                        view.CreateResultElement(correct);
                    else
                        view.CreateResultElement(correct.Other);
                }
                else
                {
                    view.CreateResultElement((FirstLevelTargetObject)correct);
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
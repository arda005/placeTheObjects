using CaseProject.Level;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CaseProject.UI
{
    /// <summary>
    /// View for end game screen.
    /// </summary>
    public class EndGameView : FullScreenView
    {
        [SerializeField] private TMP_Text totalScoreText;
        
        /// <summary>
        /// The parent objects to create result elements in.
        /// </summary>
        [SerializeField] private RectTransform reultElementsContent;
        
        /// <summary>
        /// The prefab of result elements.
        /// </summary>
        [SerializeField] private TargetObjectsResultElement resultElement;

        /// <summary>
        /// Correct color for result elements for using some part of it`s UI.
        /// </summary>
        [SerializeField] private Color resultElementCorrectColor = Color.green;

        /// <summary>
        /// Wrong color for result elements for using some part of it`s UI.
        /// </summary>
        [SerializeField] private Color resultElementWrongColor = Color.red;

        /// <summary>
        /// Normal color for result elements for using some part of it`s UI.
        /// </summary>
        [SerializeField] private Color resultElementNormalColor = Color.black;

        /// <summary>
        /// Sets total score text.
        /// </summary>
        public void SetTotalScore(int totalScore)
        {
            totalScoreText.text = totalScore.ToString();
        }

        /// <summary>
        /// Creates correct reult element.
        /// </summary>
        public void CreateResultElement(FirstLevelTargetObjectCorrect correctObject)
        {
            var score = FirstLevelLogic.CorrectPlacementScore;
            CreateResultElement().Init(correctObject.TargetObjectInformation.title, score,
                resultElementCorrectColor, correctObject.TargetObjectInformation.iconSprite);
        }

        /// <summary>
        /// Creates wrong result element.
        /// </summary>
        public void CreateResultElement(FirstLevelTargetObjectWrong wrongObject)
        {
            var score = FirstLevelLogic.WrongPlacementScore;
            CreateResultElement().Init(wrongObject.TargetObjectInformation.title, score, 
                resultElementWrongColor, wrongObject.TargetObjectInformation.iconSprite);
        }
        
        /// <summary>
        /// Creates result element.
        /// </summary>
        public void CreateResultElement(FirstLevelTargetObject targetObject)
        {
            var score = 0;
            CreateResultElement().Init(targetObject.TargetObjectInformation.title, score,
                resultElementNormalColor, targetObject.TargetObjectInformation.iconSprite);
        }

        /// <summary>
        /// Creates result element.
        /// </summary>
        private TargetObjectsResultElement CreateResultElement()
        {
            return Instantiate(resultElement, reultElementsContent);
        }
    }
}
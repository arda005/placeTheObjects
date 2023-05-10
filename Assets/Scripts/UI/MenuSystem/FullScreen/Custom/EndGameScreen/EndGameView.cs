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
        /// Creates result element.
        /// </summary>
        public void CreateResultElement(FirstLevelTargetObject targetObject, int score, Color color)
        {
            var title = targetObject.TargetObjectInformation.title;
            var sprite = targetObject.TargetObjectInformation.iconSprite;

            CreateResultElement().Init(title, score, color, sprite);
        }

        /// <summary>
        /// Creates result element.
        /// </summary>
        public void CreateResultElement(FirstLevelTargetObject targetObject, bool isCorrect)
        {
            var score = isCorrect ? FirstLevelLogic.CorrectPlacementScore : FirstLevelLogic.WrongPlacementScore;
            var color = isCorrect ? resultElementCorrectColor : resultElementWrongColor;
            CreateResultElement(targetObject, score, color);
        }

        /// <summary>
        /// Creates result element.
        /// </summary>
        public void CreateResultElement(FirstLevelTargetObject targetObject)
        {
            var score = 0;
            var color = resultElementNormalColor;
            CreateResultElement(targetObject, score, color);
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
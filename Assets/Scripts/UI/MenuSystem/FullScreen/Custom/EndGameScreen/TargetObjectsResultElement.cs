using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CaseProject.UI
{
    /// <summary>
    /// Element fot using in end game screen target objects result.
    /// </summary>
    public class TargetObjectsResultElement : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text scoreText;

        /// <summary>
        /// Initiliazes the object.
        /// </summary>
        public void Init(string title, int score, Color scoreColor, Sprite iconSprite)
        {
            iconImage.sprite = iconSprite;
            titleText.text = title;
            scoreText.text = score.ToString();
            scoreText.color = scoreColor;
        }

        /// <summary>
        /// Initiliazes the object.
        /// </summary>
        public void Init(string title, int score, Sprite iconSprite)
        {
            Init(title, score, Color.black, iconSprite);
        }
    }
}

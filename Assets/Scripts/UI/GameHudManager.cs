using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    /// <summary>
    /// Controls HUD of the game.
    /// </summary>
    public class GameHudManager : MonoBehaviour
    {
        public static GameHudManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// Shows the hud.
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the hud.
        /// </summary>
        public void Hide() 
        {
            gameObject.SetActive(false);
        }
    }
}
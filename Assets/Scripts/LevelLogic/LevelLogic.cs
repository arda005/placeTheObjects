using CaseProject.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace CaseProject.Level
{
    /// <summary>
    /// Base class for levels in the game.
    /// </summary>
    public class LevelLogic : MonoBehaviour
    {
        /// <summary>
        /// The current score.
        /// </summary>
        public int Score { get; protected set; }

        /// <summary>
        /// The next level after this.
        /// </summary>
        private LevelLogic nextLevel;

        protected virtual void Awake(){ }

        protected virtual void Start() { }

        protected virtual void OnEnable() { }

        protected virtual void OnDisable() { }

        protected virtual void OnDestroy() { }

        protected virtual void Update() { }

        /// <summary>
        /// Triggers when level end.
        /// </summary>
        public virtual void OnLevelEnd()
        {
            if(nextLevel == null)
            {
                OnGameEnded();
                return;
            }

            GameManager.Instance.SetLevel(nextLevel);
        }

        /// <summary>
        /// Trigger when player completes all levels end finishes the game.
        /// </summary>
        private void OnGameEnded()
        {
            MenuManager.Instance.OpenMenu<EndGameController>(true);
            Debug.Log("THE LEVEL IS ENDED");
            Debug.Log($"POINT: {Score}");
        }

        /// <summary>
        /// Updates progress of the level. This function soudl be called
        /// in an update function in a manager class while this level is playing.
        /// </summary>
        public virtual void CheckLevel() { }

        /// <summary>
        /// Updated task information UI for this level.
        /// </summary>
        public virtual void UpdateTaskInformationUI() { }
    }
}
using CaseProject.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace CaseProject.Level
{
    public class LevelLogic : MonoBehaviour
    {
        public float Point { get; protected set; }

        private LevelLogic nextLevel;

        protected virtual void Awake(){ }

        protected virtual void Start() { }

        protected virtual void OnEnable() { }

        protected virtual void OnDisable() { }

        protected virtual void OnDestroy() { }

        protected virtual void Update() { }

        public virtual void OnLevelEnd()
        {
            if(nextLevel == null)
            {
                OnGameEnded();
                return;
            }

            GameManager.Instance.SetLevel(nextLevel);
        }

        private void OnGameEnded()
        {
            MenuManager.Instance.OpenMenu<MainMenuController>(true);
            Debug.Log("THE LEVEL IS ENDED");
            Debug.Log($"POINT: {Point}");
        }

        public virtual void CheckLevel() { }

        public virtual void UpdateTaskInformationUI() { }
    }
}
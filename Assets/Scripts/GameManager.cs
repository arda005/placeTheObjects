using CaseProject.Level;
using CaseProject.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace CaseProject
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public bool IsGameStarted { get; private set; } = false;
        public readonly UnityEvent OnGameStarted = new UnityEvent();

        public bool IsPaused { get; private set; } = false;

        public readonly UnityEvent OnGamePaused = new UnityEvent();

        public readonly UnityEvent OnGameUnpaused = new UnityEvent();

        public LevelLogic CurrentLevel { get; private set; }
        [SerializeField] private LevelLogic firstLevel;

        private void Awake()
        {
            Instance = this;
            CurrentLevel = firstLevel;
        }

        //We are using this delay for pausing the game after
        //every other Start() events in the game are triggered.
        IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            PauseGame();
        }

        void Update()
        {
            PauseKeyUpdate();
            CheckCurrentLevel();
        }

        public void StartGame()
        {
            IsGameStarted= true;
            OnGameStarted.Invoke();
            UnpuaseGame();
        }

        public void PauseGame()
        {
            IsPaused = true;
            Cursor.lockState = CursorLockMode.None;

            OnGamePaused.Invoke();
        }

        public void UnpuaseGame()
        {
            IsPaused = false;
            Cursor.lockState = CursorLockMode.Locked;

            OnGameUnpaused.Invoke();
        }

        private void PauseKeyUpdate()
        {
            if (IsPaused) return;

            if(Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                PauseGame();
                MenuManager.Instance.OpenMenu<MainMenuController>(true);
            }
        }

        public void SetLevel(LevelLogic level)
        {
            CurrentLevel = level;
        }

        private void CheckCurrentLevel()
        {
            if (CurrentLevel == null) return;

            CurrentLevel.CheckLevel();
        }

        public void PassCurrentLevel()
        {
            if (CurrentLevel == null) return;

            CurrentLevel.OnLevelEnd();
            PauseGame();
        }
    }
}
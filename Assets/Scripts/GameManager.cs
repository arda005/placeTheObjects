using CaseProject.Level;
using CaseProject.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace CaseProject
{
    /// <summary>
    /// Controls general game funtionalities such as Restart, Puase,
    /// Pass level, End Game etc...
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        /// <summary>
        /// If game is started or not
        /// </summary>
        public bool IsGameStarted { get; private set; } = false;

        /// <summary>
        /// Triggers when game is started.
        /// </summary>
        public readonly UnityEvent OnGameStarted = new UnityEvent();

        /// <summary>
        /// If game is paused or not
        /// </summary>
        public bool IsPaused { get; private set; } = false;

        /// <summary>
        /// Triggers when game is paused
        /// </summary>
        public readonly UnityEvent OnGamePaused = new UnityEvent();

        /// <summary>
        /// Triggers when game is unpaused
        /// </summary>
        public readonly UnityEvent OnGameUnpaused = new UnityEvent();

        /// <summary>
        /// Current level which playing
        /// </summary>
        public LevelLogic CurrentLevel { get; private set; }

        // *** INFORMATION ***
        //There are no other levels in the case project
        //but I thought the design of the level system
        //shoul include this capibalty. Eventough we didnt use it
        //the game design as able to play more than one level.

        /// <summary>
        /// First level of the game
        /// </summary>
        [SerializeField] private LevelLogic firstLevel;

        private void Awake()
        {
            Init();

            OnGameUnpaused.AddListener(GameHudManager.Instance.Show);
            OnGamePaused.AddListener(GameHudManager.Instance.Hide);
        }

        /// <summary>
        /// Makes some assingments and initiliaze the game.
        /// </summary>
        private void Init()
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

        /// <summary>
        /// Starts the game.
        /// </summary>
        public void StartGame()
        {
            IsGameStarted= true;
            OnGameStarted.Invoke();
            UnpuaseGame();
        }


        /// <summary>
        /// Pauses the game.
        /// </summary>
        public void PauseGame()
        {
            IsPaused = true;
            Cursor.lockState = CursorLockMode.None;

            OnGamePaused.Invoke();
        }

        /// <summary>
        /// Unpauses the game.
        /// </summary>
        public void UnpuaseGame()
        {
            IsPaused = false;
            Cursor.lockState = CursorLockMode.Locked;

            OnGameUnpaused.Invoke();
        }

        /// <summary>
        /// Checks if pause key pressed every freame.
        /// If game is already paused then this frame
        /// will be ignored.
        /// </summary>
        private void PauseKeyUpdate()
        {
            if (IsPaused) return;

            if(Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                PauseGame();
                MenuManager.Instance.OpenMenu<MainMenuController>(true);
            }
        }

        /// <summary>
        /// Sets the current level of the game.
        /// </summary>
        /// <param name="level">The level that going to be set</param>
        public void SetLevel(LevelLogic level)
        {
            CurrentLevel = level;
        }

        /// <summary>
        /// Check if current level passed or not.
        /// </summary>
        private void CheckCurrentLevel()
        {
            if (CurrentLevel == null) return;

            CurrentLevel.CheckLevel();
        }

        /// <summary>
        /// Passes the current level.
        /// </summary>
        public void PassCurrentLevel()
        {
            if (CurrentLevel == null) return;

            CurrentLevel.OnLevelEnd();
            PauseGame();
        }

        /// <summary>
        /// Restarts the game.
        /// </summary>
        public void RestartTheGame()
        {
            var restartObjects = FindAllRestartables();

            foreach (var restartObject in restartObjects)
            {
                restartObject.OnRestart();
            }

            StartGame();
        }

        /// <summary>
        /// Finds all gameobjects has IRestartable interface.
        /// </summary>
        /// <returns></returns>
        private List<IRestartable> FindAllRestartables()
        {
            var restartables = new List<IRestartable>();

            var allGameObjects = FindObjectsOfType<GameObject>();

            foreach(var gameObject in allGameObjects)
            {
                var restartable = gameObject.GetComponent<IRestartable>();

                if (restartable != null)
                    restartables.Add(restartable);
            }

            return restartables;
        }
    }
}
using CaseProject.UI;
using UnityEngine;

namespace CaseProject.Level
{
    /// <summary>
    /// The first level of the game.
    /// </summary>
    public class FirstLevelLogic : LevelLogic
    {
        /// <summary>
        /// The correct objects
        /// </summary>
        public FirstLevelTargetObjectCorrect[] CorrectObjects { get; private set; }
        
        /// <summary>
        /// The wrong objects.
        /// </summary>
        public FirstLevelTargetObjectWrong[] WrongObjects { get; private set; }

        /// <summary>
        /// Total placed objects count.
        /// </summary>
        [HideInInspector] public int TotalPlacedObjectCount { get; private set; } = 0;

        /// <summary>
        /// The maximum objects that player should place.
        /// </summary>
        [SerializeField] private const int maxPlaceCount = 4;

        [Header("Level Descreption")]
        [TextArea(2, 5)]
        [SerializeField] private string levelTitle;
        [TextArea(2, 5)]
        [SerializeField] private string levelDescreption;
        [TextArea(2, 5)]
        [SerializeField] private string enoughObjectPlacedDesreption;

        /// <summary>
        /// The score that players achive after every correct placement.
        /// </summary>
        public const int CorrectPlacementScore = 5;

        /// <summary>
        /// The score that players lose after every wrong placement.
        /// </summary>
        public const int WrongPlacementScore = -5;

        protected override void Awake()
        {
            base.Awake();
            CorrectObjects = FindObjectsOfType<FirstLevelTargetObjectCorrect>();
            WrongObjects = FindObjectsOfType<FirstLevelTargetObjectWrong>();
        }

        protected override void Start()
        {
            base.Start();
            UpdateTaskInformationUI();
        }

        /// <summary>
        /// Updates level every frame.
        /// </summary>
        public override void CheckLevel()
        {
            base.CheckLevel();

            Score = 0;
            foreach (var correct in CorrectObjects)
            {
                if (correct.IsThisOrOtherSelected)
                {
                    if (correct.IsInTarget)
                        Score += CorrectPlacementScore;
                    else
                        Score += WrongPlacementScore;
                }
            }
        }

        public override void UpdateTaskInformationUI()
        {
            base.UpdateTaskInformationUI();

            var taskController = FindObjectOfType<TaskInformationContoller>(true);

            if (taskController == null) return;

            var taskModel = (TaskInformationModel)taskController.Model;

            taskModel.SetTaskInformation(levelTitle, GetLevelDescreption());

            taskController.UpdateView();
        }

        /// <summary>
        /// The key that going to replace with placementCount.
        /// We are using it in unity inpector. And replacing it with the value of variable.
        /// </summary>
        private const string totalPlacedObjectValueKey = "{{totalPlacedObejct}}";
        private string GetLevelDescreption()
        {
            var leftObjectsCount = maxPlaceCount - TotalPlacedObjectCount;

            var selectedTextTemplate = GetDescreptionTemplate(leftObjectsCount);
            
            return selectedTextTemplate.Replace(totalPlacedObjectValueKey, leftObjectsCount.ToString());
        }

        /// <summary>
        /// Return correct desreption template for current progress of the level.
        /// </summary>
        /// <param name="leftPlaceCount">How much more objects players should place.</param>
        /// <returns>Task descreption.</returns>
        private string GetDescreptionTemplate(int leftPlaceCount)
        {
            if (leftPlaceCount == 0)
            {
                return enoughObjectPlacedDesreption;
            }
            else
            {
                return levelDescreption;
            }
        }

        /// <summary>
        /// Increases placed object count.
        /// </summary>
        public void IncreasePlacedObjectCount()
        {
            TotalPlacedObjectCount++;
            UpdateTaskInformationUI();
        }

        /// <summary>
        /// Decreases placed object count.
        /// </summary>
        public void DecreasePlacedObjectCount()
        {
            TotalPlacedObjectCount--;
            UpdateTaskInformationUI();
        }
    }
}
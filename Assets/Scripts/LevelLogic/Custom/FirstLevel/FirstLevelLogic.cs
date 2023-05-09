using CaseProject.UI;
using UnityEngine;

namespace CaseProject.Level
{
    public class FirstLevelLogic : LevelLogic
    {
        [SerializeField] private FirstLevelTargetObjects[] correctObjects;
        [SerializeField] private FirstLevelTargetObjects[] wrongObjects;

        [HideInInspector] public int TotalPlacedObjectCount { get; private set; } = 0;
        public const int maxPlaceCount = 4;

        [Header("Level Descreption")]
        [TextArea(2, 5)]
        [SerializeField] private string levelTitle;
        [TextArea(2, 5)]
        [SerializeField] private string levelDescreption;
        [TextArea(2, 5)]
        [SerializeField] private string enoughObjectPlacedDesreption;

        public override void CheckLevel()
        {
            base.CheckLevel();

            Point = 0;
            foreach(var correct in correctObjects)
            {
                if (correct.IsInTarget)
                    Point += 50;
            }

            foreach (var wrong in wrongObjects)
            {
                if (wrong.IsInTarget)
                    Point -= 30;
            }
        }

        public override void UpdateTaskInformationUI()
        {
            base.UpdateTaskInformationUI();

            var taskController = FindObjectOfType<TaskInformationContoller>();

            if (taskController == null) return;

            var taskModel = (TaskInformationModel)taskController.Model;

            taskModel.SetTaskInformation(levelTitle, GetLevelDescreption());

            taskController.UpdateView();
        }

        private const string totalPlacedObjectValueKey = "{{totalPlacedObejct}}";
        private string GetLevelDescreption()
        {
            var leftObjectsCount = maxPlaceCount - TotalPlacedObjectCount;

            var selectedTextTemplate = GetDescreptionTemplate(leftObjectsCount);
            
            return selectedTextTemplate.Replace(totalPlacedObjectValueKey, leftObjectsCount.ToString());
        }

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

        public void IncreasePlacedObjectCount()
        {
            TotalPlacedObjectCount++;
            UpdateTaskInformationUI();
        }

        public void DecreasePlacedObjectCount()
        {
            TotalPlacedObjectCount--;
            UpdateTaskInformationUI();
        }
    }
}
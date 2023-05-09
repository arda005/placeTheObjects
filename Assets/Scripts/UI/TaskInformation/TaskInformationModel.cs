using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    public class TaskInformationModel : BaseModel
    {
        public string TaskTitle { get; private set; }
        public string TaskDescription { get; private set;}

        public void SetTaskInformation(string taskTitle, string taskDescription)
        {
            this.TaskTitle = taskTitle;
            this.TaskDescription = taskDescription;
        }
    }
}
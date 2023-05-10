using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    /// <summary>
    /// Model for tast information UI.
    /// </summary>
    public class TaskInformationModel : BaseModel
    {
        /// <summary>
        /// Title of current task.
        /// </summary>
        public string TaskTitle { get; private set; }

        /// <summary>
        /// Descreption of current task.
        /// </summary>
        public string TaskDescription { get; private set;}


        /// <summary>
        /// Sets the task informations.
        /// </summary>
        public void SetTaskInformation(string taskTitle, string taskDescription)
        {
            this.TaskTitle = taskTitle;
            this.TaskDescription = taskDescription;
        }
    }
}
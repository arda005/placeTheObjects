using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    /// <summary>
    /// Controller for tast information UI.
    /// </summary>
    public class TaskInformationContoller : BaseController
    {
        public override void UpdateView()
        {
            base.UpdateView();

            var view = (TaskInformationView)View;
            var model = (TaskInformationModel)Model;
            view.UpdateTexts(model.TaskTitle, model.TaskDescription);
        }
    }
}

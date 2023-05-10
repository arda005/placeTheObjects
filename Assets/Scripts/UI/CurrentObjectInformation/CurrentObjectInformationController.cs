using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    /// <summary>
    /// Controller for Current Object Information Area
    /// </summary>
    public class CurrentObjectInformationController : BaseController
    {
        public override void UpdateView()
        {
            base.UpdateView();

            var model = (CurrentObjectInformationModel)Model;
            var view = (CurrentObjectInformationView)View;

            if (model.TargetObjectInformation == null)
            {
                view.ClearUI();
                return;
            }

            view.UpdateIcon(model.TargetObjectInformation.iconSprite);
            view.UpdateTitleText(model.TargetObjectInformation.title);
        }
    }
}
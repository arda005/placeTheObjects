using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    /// <summary>
    /// Controller of crosshair.
    /// </summary>
    public class CrosshairController : BaseController
    {
        protected override void AssignModelAndView()
        {
            base.AssignModelAndView();
        }

        public override void UpdateView()
        {
            base.UpdateView();

            CrosshairView crosshairView = (CrosshairView)View;
            CrosshairModel crosshairModel = (CrosshairModel)Model;

            crosshairView.UpdateRadius(crosshairModel.Radius);
            crosshairView.UpdateAlpha(crosshairModel.Alpha);
        }
    }
}
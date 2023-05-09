using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    public class PopupController : MenuController
    {
        public override void Close()
        {
            MenuManager.Instance.CheckPopupContentActivity();
            base.Close();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    /// <summary>
    /// Controller class for pop ups.
    /// </summary>
    public class PopupController : MenuController
    {
        /// <summary>
        /// Closes the pop up.
        /// </summary>
        public override void Close()
        {
            MenuManager.Instance.UpdatePopupContentActivity();
            base.Close();
        }
    }
}
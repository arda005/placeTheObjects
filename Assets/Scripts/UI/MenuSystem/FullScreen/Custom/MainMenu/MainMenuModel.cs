using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CaseProject.UI
{
    /// <summary>
    /// Model of main menu
    /// </summary>
    public class MainMenuModel : FullScreenModel
    {
        /// <summary>
        /// The title for about pop up menu.
        /// </summary>
        [TextArea(2,5)]
        public string aboutPopupTitle;

        /// <summary>
        /// The descreption for about pop up menu.
        /// </summary>
        [TextArea(2, 20)]
        public string aboutPopupDescription;

        /// <summary>
        /// When about pop up menu accepted.
        /// </summary>
        public UnityEvent aboutPopupOnAccepted;
    }
}
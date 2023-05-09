using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CaseProject.UI
{
    public class MainMenuModel : FullScreenModel
    {
        [TextArea(2,5)]
        public string aboutPopupTitle;
        [TextArea(2, 20)]
        public string aboutPopupDescription;
        public UnityEvent aboutPopupOnAccepted;
    }
}
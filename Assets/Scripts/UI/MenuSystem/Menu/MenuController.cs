using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    /// <summary>
    /// Base controller class for menus.
    /// </summary>
    public class MenuController : BaseController
    {
        public virtual void Close()
        {
            Destroy(gameObject);
        }
    }
}
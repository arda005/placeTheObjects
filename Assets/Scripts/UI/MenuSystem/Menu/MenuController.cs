using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    public class MenuController : BaseController
    {
        public virtual void Close()
        {
            Destroy(gameObject);
        }
    }
}
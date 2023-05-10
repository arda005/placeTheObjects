using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{    /// <summary>
     /// Base class for model of MVC.
     /// </summary>
    public class BaseModel : MonoBehaviour
    {
        /// <summary>
        /// Controller for this model.
        /// </summary>
        public BaseController Controller { get; private set; }
        protected virtual void Awake() 
        {
            Controller = GetComponent<BaseController>();
        }

        protected virtual void Start() { }

        protected virtual void OnEnable() { }

        protected virtual void OnDisable() { }

        protected virtual void OnDestroy() { }

        protected virtual void Update() { }
    }
}
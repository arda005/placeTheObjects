using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    /// <summary>
    /// Base class for View of MVC.
    /// </summary>
    public class BaseView : MonoBehaviour
    {
        /// <summary>
        /// Recttransform component of the object.
        /// </summary>
        public RectTransform viewRectTransform { get; private set; }

        /// <summary>
        /// Controller for this view.
        /// </summary>
        public BaseController Controller { get; private set; }

        protected virtual void Awake() 
        {
            viewRectTransform = GetComponent<RectTransform>();
            Controller = GetComponent<BaseController>();
        }

        protected virtual void Start() { }

        protected virtual void OnEnable() { }

        protected virtual void OnDisable() { }

        protected virtual void OnDestroy() { }

        protected virtual void Update() { }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    /// <summary>
    /// Base class for controller of MVC.
    /// </summary>
    public class BaseController : MonoBehaviour
    {
        /// <summary>
        /// Model of this controller.
        /// </summary>
        public BaseModel Model { get; private set; }

        /// <summary>
        /// View of this controller.
        /// </summary>
        public BaseView View { get; private set; }

        protected virtual void Awake() { AssignModelAndView(); }

        protected virtual void Start() { UpdateView(); }

        protected virtual void OnEnable() { }

        protected virtual void OnDisable() { }

        protected virtual void OnDestroy() { }

        protected virtual void Update() { }

        /// <summary>
        /// Updates view.
        /// </summary>
        public virtual void UpdateView() { }

        /// <summary>
        /// Assigsn view and model.
        /// </summary>
        protected virtual void AssignModelAndView() 
        {
            Model = GetComponent<BaseModel>();
            View= GetComponent<BaseView>();
        }
    }
}
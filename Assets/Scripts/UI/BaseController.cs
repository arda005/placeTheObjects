using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    public class BaseController : MonoBehaviour
    {
        public BaseModel Model { get; private set; }
        public BaseView View { get; private set; }

        protected virtual void Awake() { AssignModelAndView(); }

        protected virtual void Start() { UpdateView(); }

        protected virtual void OnEnable() { }

        protected virtual void OnDisable() { }

        protected virtual void OnDestroy() { }

        protected virtual void Update() { }

        public virtual void UpdateView() { }

        protected virtual void AssignModelAndView() 
        {
            Model = GetComponent<BaseModel>();
            View= GetComponent<BaseView>();
        }
    }
}
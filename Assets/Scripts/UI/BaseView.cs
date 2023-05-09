using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    public class BaseView : MonoBehaviour
    {
        public RectTransform viewRectTransform { get; private set; }

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
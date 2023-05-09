using UnityEngine;

namespace CaseProject.Intractable
{
    public class IntractableElement : MonoBehaviour, IIntractableElement
    {
        [SerializeField] public bool IsHoldable { get; protected set; } = false;

        public bool IsIntractable { get; protected set; } = true;

        protected virtual void Awake() { }

        protected virtual void OnEnable() { }

        protected virtual void OnDisable() { }

        protected virtual void OnDestroy() { }

        protected virtual void Start() { }

        protected virtual void Update() { }

        public virtual void BeginIntract() { }

        public virtual void OnIntract() { }

        public virtual void EndIntract() { }

        public void SetIntractable(bool value) { IsIntractable = value; }
    }
}
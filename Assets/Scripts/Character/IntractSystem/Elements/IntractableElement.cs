using UnityEngine;

namespace CaseProject.Intractable
{
    /// <summary>
    /// Base class for intractable objects.
    /// </summary>
    public class IntractableElement : MonoBehaviour, IIntractableElement
    {
        /// <summary>
        /// If this object holdable or not.
        /// If it is not holdable that means players wont be
        /// lift them up during intracting process. Exmp: The door.
        /// </summary>
        [SerializeField] public bool IsHoldable { get; protected set; } = false;

        /// <summary>
        /// If this object currently intractable.
        /// </summary>
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

        /// <summary>
        /// Sets intractable state.
        /// </summary>
        /// <param name="value">New intractable state</param>
        public void SetIntractable(bool value) { IsIntractable = value; }
    }
}
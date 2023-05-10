using CaseProject.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    /// <summary>
    /// View for placed object information UI.
    /// </summary>
    public class PlacedObjectsView : BaseView
    {
        /// <summary>
        /// The prefab that are going to create after placing objects.
        /// </summary>
        public PlacedObjectsElement elementPrefab;

        /// <summary>
        /// Contaings created elements.
        /// </summary>
        private readonly List<PlacedObjectsElement> elements = new List<PlacedObjectsElement>();

        /// <summary>
        /// Creates a new element.
        /// </summary>
        public void AddElement(FirstLevelTargetObject targetObject1, FirstLevelTargetObject targetObject2)
        {
            var createdElement = Instantiate(elementPrefab, viewRectTransform);

            var firstInfo = targetObject1.GetTargetInformation();
            var secondInfo = targetObject2.GetTargetInformation();

            createdElement.Init(firstInfo.iconSprite, secondInfo.iconSprite, firstInfo);

            elements.Add(createdElement);
        }

        /// <summary>
        /// Destroys specified element.
        /// </summary>
        public void RemoveElement(TargetObjectInformation element)
        {
            var deletingElement = elements.Find(x => x.TargetObjectInformation.Equals(element));

            if (deletingElement == null) return;

            elements.Remove(deletingElement);
            Destroy(deletingElement.gameObject);
        }
    }
}
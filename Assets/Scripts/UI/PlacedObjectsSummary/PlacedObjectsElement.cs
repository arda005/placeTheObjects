using CaseProject.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CaseProject.UI
{
    /// <summary>
    /// The object for placed objects information UI. Will be used as prefab.
    /// </summary>
    public class PlacedObjectsElement : MonoBehaviour
    {
        /// <summary>
        /// The image of placed object.
        /// </summary>
        [SerializeField] private Image firstObjectImage;

        /// <summary>
        /// The image of other pair of placed object.
        /// </summary>
        [SerializeField] private Image secondObjectImage;

        /// <summary>
        /// Target object information of the placed object.
        /// </summary>
        public TargetObjectInformation TargetObjectInformation { get; private set; }

        public void Init(Sprite firstIcon, Sprite secondIcon, TargetObjectInformation information)
        {
            firstObjectImage.sprite = firstIcon;
            secondObjectImage.sprite = secondIcon;
            TargetObjectInformation = information;
        }
    }
}
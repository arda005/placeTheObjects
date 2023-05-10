using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject
{
    [CreateAssetMenu(fileName ="TargetObject", menuName ="Case Project/Target Object Information")]
    public class TargetObjectInformation : ScriptableObject
    {
        /// <summary>
        /// Icon sprite for this object. We are using it in UI
        /// </summary>
        public Sprite iconSprite;

        /// <summary>
        /// Title for this object. We are using it in UI.
        /// </summary>
        public string title;
    }
}
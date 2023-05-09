using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject
{
    [CreateAssetMenu(fileName ="TargetObject", menuName ="Case Project/Target Object Information")]
    public class TargetObjectInformation : ScriptableObject
    {
        public Sprite iconSprite;
        public string title;
    }
}
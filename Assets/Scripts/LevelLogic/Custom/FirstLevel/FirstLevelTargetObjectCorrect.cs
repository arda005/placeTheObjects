using CaseProject.Intractable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.Level
{
    public class FirstLevelTargetObjectCorrect : FirstLevelTargetObject
    {
        /// <summary>
        /// Returns other pair of this object.
        /// </summary>
        public FirstLevelTargetObjectWrong Other { get { return (FirstLevelTargetObjectWrong)other; } }
    }
}
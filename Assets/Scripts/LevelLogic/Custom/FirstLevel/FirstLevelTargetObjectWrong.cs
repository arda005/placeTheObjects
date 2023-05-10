using CaseProject.Intractable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.Level
{
    public class FirstLevelTargetObjectWrong : FirstLevelTargetObject
    {       
        /// <summary>
        /// Returns other pair of this object.
        /// </summary>
        public FirstLevelTargetObjectCorrect Other { get { return (FirstLevelTargetObjectCorrect)other; } }
    }
}
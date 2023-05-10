using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    /// <summary>
    /// Model for Current Object Information Area
    /// </summary>
    public class CurrentObjectInformationModel : BaseModel
    {
        /// <summary>
        /// Target object information.
        /// </summary>
        public TargetObjectInformation TargetObjectInformation { get; protected set; }

        /// <summary>
        /// Sets target object information.
        /// </summary>
        public void SetTargetInformation(TargetObjectInformation targetObjectInformation)
        {
            TargetObjectInformation = targetObjectInformation;
        }
    }
}
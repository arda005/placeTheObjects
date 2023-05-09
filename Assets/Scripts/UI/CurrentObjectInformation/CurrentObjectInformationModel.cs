using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.UI
{
    public class CurrentObjectInformationModel : BaseModel
    {
        public TargetObjectInformation TargetObjectInformation { get; protected set; }

        public void SetTargetInformation(TargetObjectInformation targetObjectInformation)
        {
            TargetObjectInformation = targetObjectInformation;
        }
    }
}
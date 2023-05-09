using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject.Intractable
{
    public class DoorElement : IntractableElement
    {
        public override void EndIntract()
        {
            transform.DORotate(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -75), 0.65f).onComplete = () =>
            {
                base.EndIntract();
                GameManager.Instance.PassCurrentLevel();
            };
        }
    }
}
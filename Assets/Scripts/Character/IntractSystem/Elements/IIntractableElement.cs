using CaseProject.Intractable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIntractableElement
{
    public void BeginIntract();
    public void OnIntract();
    public void EndIntract();
}

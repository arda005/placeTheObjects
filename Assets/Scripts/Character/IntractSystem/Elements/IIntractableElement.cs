using CaseProject.Intractable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIntractableElement
{
    /// <summary>
    /// Begins intracting with this object.
    /// </summary>
    public void BeginIntract();

    /// <summary>
    /// Updates current intract.
    /// </summary>
    public void OnIntract();

    /// <summary>
    /// Ends intracting with this object.
    /// </summary>
    public void EndIntract();
}

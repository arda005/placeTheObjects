using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaseProject
{
    public interface IRestartable
    {
        /// <summary>
        /// Calls from GameManager when game is restarted.
        /// </summary>
        public void OnRestart();
    }
}
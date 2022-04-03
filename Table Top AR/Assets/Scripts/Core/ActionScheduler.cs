using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction currentAction;

        public void StartAction(IAction action)
        {
            if (action == currentAction)
            {
                return;
            }
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;
        }
    }
}

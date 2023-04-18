using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TableTopAR.Core.GenericInput;

namespace TableTopAR.Core
{
    public interface IRayCastable
    {
        CursorType GetCursorType();
        bool HandleRaycast(GenericInput callingInput);
    }
}
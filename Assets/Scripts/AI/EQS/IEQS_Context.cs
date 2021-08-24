﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ai.EQS
{
    public interface IEQS_Context
    {
        List<IEQS_ContextElement> GetContextElements();
        IEQS_ContextFilter GetFilter();
        //Fast filtered
        IEQS_ContextElement GetNearest(string path, Vector3 position);
    }
}
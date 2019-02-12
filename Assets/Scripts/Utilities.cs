﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static Transform GetHighestParent(Transform start)
    {
        Transform current = start;
        while (current.parent != null)
        {
            current = current.parent;
        }

        return current;
    }
}

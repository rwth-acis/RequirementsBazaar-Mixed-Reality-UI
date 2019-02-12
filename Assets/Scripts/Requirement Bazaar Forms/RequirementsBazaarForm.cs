using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementsBazaarForm : MonoBehaviour
{
    public virtual void Close()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        PageClosed?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler PageClosed;
}

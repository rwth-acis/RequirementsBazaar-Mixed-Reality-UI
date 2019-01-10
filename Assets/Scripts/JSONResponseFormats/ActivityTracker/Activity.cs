using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Activity
{
    public int id;
    public string creationDate;
    public string activityAction;
    public string origin;
    public string dataUrl;
    public string dataType;
    public string dataFrontendUrl;
    public string parentDataUrl;
    public string parentDataType;
    public string userUrl;
    public AdditionalObject additionalObject;
}

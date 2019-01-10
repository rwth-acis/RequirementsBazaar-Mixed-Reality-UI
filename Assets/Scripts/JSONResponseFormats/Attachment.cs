using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attachment
{
    public int id;
    public string name;
    public string description;
    public string mimeType;
    public string identifier;
    public string fileUrl;
    public int requirementId;
    public User creator;
    public string creationDate;
    public string lastUpdatedDate;
}

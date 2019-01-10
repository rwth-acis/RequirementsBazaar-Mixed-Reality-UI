﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Comment
{
    public int id;
    public string message;
    public int replyToComment;
    public int requirementId;
    public User creator;
    public string creationDate;
    public string lastUpdatedDate;
}

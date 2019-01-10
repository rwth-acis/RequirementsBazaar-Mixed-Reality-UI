using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Requirement
{
    public int id;
    public string name;
    public string description;
    public int projectId;
    public User creator;
    public Category[] categories;
    public string creationDate;
    public int numberOfComments;
    public int numberOfAttachments;
    public int numberofFollowers;
    public int upVotes;
    public int downVotes;
    public string userVoted;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public int id;
    public string userName;
    public string firstName;
    public string lastName;
    public bool admin;
    public long las2peerId;
    public string profileImage;
    public bool emailLeadSubscription;
    public bool emailFollowSubscription;
    public string creationDate;
    public string lastUpdatedDate;
    public string lastLoginDate;
}

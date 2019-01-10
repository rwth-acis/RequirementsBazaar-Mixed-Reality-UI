using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RequirementContributors
{
    public User creator;
    public User leadDeveloper;
    public User[] developers;
    public User[] commentCreator;
    public User[] attachmentCreator;
}

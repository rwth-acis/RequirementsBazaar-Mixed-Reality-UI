using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AdditionalObject
{
    public UserObjectField user;
    public ObjectField project;
    public ObjectField category;
    public ObjectField requirement;
	
}

[Serializable]
public class UserObjectField
{
    public int id;
}

[Serializable]
public class ObjectField
{
    public int id;
    public string name;
}

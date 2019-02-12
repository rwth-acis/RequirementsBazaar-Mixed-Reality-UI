using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Org.Requirements_Bazaar.DataModel
{

    [Serializable]
    public class Category
    {
        public int id;
        public string name;
        public string description;
        public string projectId;
        public User leader;
        public string creationDate;
        public string lastUpdatedDate;
        public int numberOfRequirements;
        public int numberOfFollowers;
        public bool isFollower;
    }

}
using Org.Requirements_Bazaar.API;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Org.Requirements_Bazaar.DataModel
{

    [Serializable]
    public class Project
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private bool visibility;
        [SerializeField] private int defaultCategoryId;
        [SerializeField] private User leader;
        [SerializeField] private string creationDate;
        [SerializeField] private string lastUpdatedDate;
        [SerializeField] private int numberOfCategories;
        [SerializeField] private int numberOfRequirements;
        [SerializeField] private int numberOfFollowers;
        [SerializeField] private bool isFollower;

        public Project(string name, string description, bool visibility, User leader)
        {
            this.name = name;
            this.description = description;
            this.visibility = visibility;
            this.leader = leader;
        }

        #region Properties

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public bool Visibility
        {
            get
            {
                return visibility;
            }

            set
            {
                visibility = value;
            }
        }

        public int DefaultCategoryId
        {
            get
            {
                return defaultCategoryId;
            }

            set
            {
                defaultCategoryId = value;
            }
        }

        public User Leader
        {
            get
            {
                return leader;
            }

            set
            {
                leader = value;
            }
        }

        public string CreationDate
        {
            get
            {
                return creationDate;
            }
        }

        public string LastUpdatedDate
        {
            get
            {
                return lastUpdatedDate;
            }
        }

        public int NumberOfCategories
        {
            get
            {
                return numberOfCategories;
            }
        }

        public int NumberOfRequirements
        {
            get
            {
                return numberOfRequirements;
            }
        }

        public int NumberOfFollowers
        {
            get
            {
                return numberOfFollowers;
            }
        }

        public bool IsFollower
        {
            get
            {
                return isFollower;
            }

            set
            {
                isFollower = value;
            }
        }

        #endregion
    }
}

using Org.Requirements_Bazaar.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Org.Requirements_Bazaar.Serialization
{

    /// <summary>
    /// Used to serialize post data because the API does not accept a fully-serialized requirement
    /// </summary>
    [Serializable]
    public class JsonCreateRequirement
    {
        [SerializeField] private int projectId;
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private Category[] categories;

        public JsonCreateRequirement(int projectId, string name, string description, Category[] categories)
        {
            this.projectId = projectId;
            this.name = name;
            this.description = description;
            this.categories = categories;
        }

        public int ProjectId
        {
            get
            {
                return projectId;
            }

            set
            {
                projectId = value;
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

        public Category[] Categories
        {
            get
            {
                return categories;
            }

            set
            {
                categories = value;
            }
        }
    }

}
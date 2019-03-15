using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Org.Requirements_Bazaar.DataModel
{

    [Serializable]
    public class Attachment
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private string mimeType;
        [SerializeField] private string identifier;
        [SerializeField] private string fileUrl;
        [SerializeField] private int requirementId;
        [SerializeField] private User creator;
        [SerializeField] private string creationDate;
        [SerializeField] private string lastUpdatedDate;

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

        public string MimeType
        {
            get
            {
                return mimeType;
            }

            set
            {
                mimeType = value;
            }
        }

        public string Identifier
        {
            get
            {
                return identifier;
            }
        }

        public string FileUrl
        {
            get
            {
                return fileUrl;
            }

            set
            {
                fileUrl = value;
            }
        }

        public int RequirementId
        {
            get
            {
                return requirementId;
            }

            set
            {
                requirementId = value;
            }
        }

        public User Creator
        {
            get
            {
                return creator;
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

        #endregion

        public Attachment(int requirementId, string name, string description, string fileUrl)
        {
            this.requirementId = requirementId;
            this.name = name;
            this.description = description;
            this.fileUrl = fileUrl;
        }
    }

}
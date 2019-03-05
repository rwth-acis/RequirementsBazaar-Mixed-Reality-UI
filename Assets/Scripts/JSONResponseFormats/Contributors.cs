using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Org.Requirements_Bazaar.DataModel
{

    [Serializable]
    public class Contributors
    {

        [SerializeField] private User leader;
        [SerializeField] private User[] requirementCreator;
        [SerializeField] private User[] leadDeveloper;
        [SerializeField] private User[] developers;
        [SerializeField] private User[] attachmentCreator;

        #region Properties

        public User Leader
        {
            get
            {
                return leader;
            }
        }

        public User[] RequirementCreator
        {
            get
            {
                return requirementCreator;
            }
        }

        public User[] LeadDeveloper
        {
            get
            {
                return leadDeveloper;
            }
        }

        public User[] Developers
        {
            get
            {
                return developers;
            }
        }

        public User[] AttachmentCreator
        {
            get
            {
                return attachmentCreator;
            }
        }

        #endregion
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Org.Requirements_Bazaar.DataModel
{

    [Serializable]
    public class RequirementContributors
    {
        public User creator;
        public User leadDeveloper;
        public User[] developers;
        public User[] commentCreator;
        public User[] attachmentCreator;
    }

}

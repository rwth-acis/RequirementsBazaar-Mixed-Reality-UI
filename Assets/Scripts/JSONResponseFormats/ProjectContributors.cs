using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Org.Requirements_Bazaar.DataModel
{

    [Serializable]
    public class Contributors
    {

        public User leader;
        public User[] requirementCreator;
        public User[] leadDeveloper;
        public User[] developers;
        public User[] attachmentCreator;
    }

}
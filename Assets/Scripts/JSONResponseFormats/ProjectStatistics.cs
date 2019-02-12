using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Org.Requirements_Bazaar.DataModel
{

    [Serializable]
    public class Statistics
    {
        public int numberOfProjects;
        public int numberOfCategories;
        public int numberOfRequirements;
        public int numberOfComments;
        public int numberOfAttachments;
        public int numberOfVotes;
    }

}

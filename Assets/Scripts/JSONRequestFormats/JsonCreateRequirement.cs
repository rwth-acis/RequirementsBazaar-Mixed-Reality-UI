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
    public class JsonCreateRequirement : MonoBehaviour
    {
        public int projectId;
        public string name;
        public string description;
        public Category[] categories;
    }

}
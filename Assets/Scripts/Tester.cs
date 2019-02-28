using Org.Requirements_Bazaar.API;
using Org.Requirements_Bazaar.AR_VR_Forms;
using Org.Requirements_Bazaar.DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Org.Requirements_Bazaar.Common
{

    public class Tester : MonoBehaviour
    {
        // Use this for initialization
        async void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Posting");
                Requirement res = await RequirementsBazaar.CreateRequirement(399, "Test Requirement 2", "posted from Unity code");
                Debug.Log("name: " + res.name);
                Debug.Log("Id: " + res.id);
                Debug.Log("Proj Id: " + res.projectId);
            }
        }
    }

}

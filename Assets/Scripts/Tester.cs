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
                Requirement[] reqs = await RequirementsBazaar.GetCategoryRequirements(751);
                foreach(Requirement req in reqs)
                {
                    Debug.Log(req.Name + "; " + req.UserVoted);
                }
                Debug.Log("Posting");
            }
        }
    }
}

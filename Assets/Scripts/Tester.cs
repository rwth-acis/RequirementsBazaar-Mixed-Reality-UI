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
                //Requirement req = await RequirementsBazaar.GetRequirement(2101);
                //req.Description = "updated description";
                //Requirement res = await RequirementsBazaar.UpdateRequirement(req);

                await RequirementsBazaar.CreateRequirement(400, "Requirement 2", "created requirement with new upload format");
            }
        }
    }

}

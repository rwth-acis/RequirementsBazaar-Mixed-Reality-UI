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

                Project proj = await RequirementsBazaar.GetProject(400);
                proj.visibility = false;
                Project res = await RequirementsBazaar.UpdateProject(proj);

                //req.description = "changed in Unity code";
                //Requirement res = await RequirementsBazaar.UpdateRequirement(req);
                //Debug.Log(res.name);
                //Debug.Log(res.description);
            }
        }
    }

}

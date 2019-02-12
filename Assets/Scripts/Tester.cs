using Org.Requirements_Bazaar.AR_VR_Forms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Org.Requirements_Bazaar.Common
{

    public class Tester : MonoBehaviour
    {
        public RequirementsBazaarCreateEditRequirementForm form;

        // Use this for initialization
        async void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Fetching");
                form.SetUpEditMode(1931);
            }
        }
    }

}

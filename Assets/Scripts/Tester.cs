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

                // 1387

                Comment comment = new Comment("comment which is not a reply", 2101);
                Comment res = await RequirementsBazaar.CreateComment(comment);

                //Comment comment = await RequirementsBazaar.GetComment(1387);
                //Debug.Log(comment.ReplyToComment);
            }
        }
    }

}

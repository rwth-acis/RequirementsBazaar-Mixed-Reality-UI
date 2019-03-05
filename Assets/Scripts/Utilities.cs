using Org.Requirements_Bazaar.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Org.Requirements_Bazaar.Common
{

    public static class Utilities
    {
        public static Transform GetHighestParent(Transform start)
        {
            Transform current = start;
            while (current.parent != null)
            {
                current = current.parent;
            }

            return current;
        }

        public static Dictionary<string, string> GetStandardHeaders()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(AuthorizationManager.Instance.AccessToken))
            {
                headers.Add("Authorization", "Bearer " + AuthorizationManager.Instance.AccessToken);
            }
            return headers;
        }
    }

}

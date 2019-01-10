using Microsoft.MixedReality.Toolkit.Core.Utilities.WebRequestRest;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class ActivityTracker
{
    private const string baseUrl = "https://requirements-bazaar.org/activities/";

    public static async Task<Activity[]> GetActivities(int limit = 10, int before = -1, int after = -1)
    {
        string url = baseUrl;
        url += "?before=" + before.ToString();
        url += "&after=" + after.ToString();
        url += "&limit=" + limit.ToString();
        url += "&fillChildElements=false";

        Response response = await Rest.GetAsync(url);

        if (!response.Successful)
        {
            Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
            return null;
        }
        else
        {
            string json = JsonHelper.EncapsulateInWrapper(response.ResponseBody);
            Activity[] activities = JsonHelper.FromJson<Activity>(json);
            return activities;
        }
    }
}

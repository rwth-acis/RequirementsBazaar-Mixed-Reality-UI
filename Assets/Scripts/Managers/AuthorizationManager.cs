using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Org.Requirements_Bazaar.Managers
{

    public class AuthorizationManager : Singleton<AuthorizationManager>
    {
        [SerializeField]
        private string clientId = "c4ced10f-ce0f-4155-b6f7-a4c40ffa410c";
        [SerializeField]
        private string debugToken;
        private string accessToken;

        const string learningLayersAuthorizationEndpoint = "https://api.learning-layers.eu/o/oauth2/authorize";
        const string learningLayersTokenEndpoint = "https://api.learning-layers.eu/o/oauth2/token";
        const string learningLayersUserInfoEndpoint = "https://api.learning-layers.eu/o/oauth2/userinfo";

        const string scopes = "openid%20profile%20email";

        public string AccessToken { get { return accessToken; } }

        private void Start()
        {
            // skip the login by using the debug token
            if (Application.isEditor)
            {
                if (string.IsNullOrEmpty(accessToken))
                {
                    accessToken = debugToken;
                    AddAccessTokenToHeader();
                }
                CheckAccessToken();
            }
        }

        private void OnLogin(UnityWebRequest result)
        {
            if (result.responseCode == 200)
            {
                string json = result.downloadHandler.text;
                Debug.Log("Login worked: " + json);
            }
            else
            {
                Debug.Log("Login Check error: " + result.error);
            }
        }

        private void AddAccessTokenToHeader()
        {
            if (RestManager.Instance.StandardHeader.ContainsKey("Authorization"))
            {
                RestManager.Instance.StandardHeader["Authorization"] = "Bearer " + accessToken;
            }
            else
            {
                RestManager.Instance.StandardHeader.Add("Authorization", "Bearer " + accessToken);
            }
        }

        private void CheckAccessToken()
        {
            RestManager.Instance.GET(learningLayersUserInfoEndpoint + "?access_token=" + accessToken, OnLogin);
        }
    }

}
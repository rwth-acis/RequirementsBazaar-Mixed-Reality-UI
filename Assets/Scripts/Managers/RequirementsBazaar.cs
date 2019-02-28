using Microsoft.MixedReality.Toolkit.Core.Utilities.WebRequestRest;
using Org.Requirements_Bazaar.Common;
using Org.Requirements_Bazaar.DataModel;
using Org.Requirements_Bazaar.Managers;
using Org.Requirements_Bazaar.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Org.Requirements_Bazaar.API
{

    public static class RequirementsBazaar
    {
        private const string baseUrl = "https://requirements-bazaar.org/bazaar/";

        #region projects

        /// <summary>
        /// Retrieves all projecs on the given page.
        /// </summary>
        /// <param name="page">The page nubmer of the project list</param>
        /// <param name="per_page">The number of projects on one page</param>
        /// <param name="mode">Decides how the project list should be sorted</param>
        /// <returns>An array of projects which are listed on the requested page number</returns>
        public static async Task<Project[]> GetProjects(int page, int per_page = 10, ProjectSortingMode mode = ProjectSortingMode.DEFAULT)
        {
            string url = baseUrl + "projects?page=" + page.ToString()
                + "&per_page=" + per_page.ToString();
            if (mode != ProjectSortingMode.DEFAULT)
            {
                url += "&sort=" + mode.ToString().ToLower();
            }

            Response response = await Rest.GetAsync(url);
            if (!response.Successful)
            {
                Debug.LogError(response.ResponseBody);
                return null;
            }
            else
            {
                string json = JsonHelper.EncapsulateInWrapper(response.ResponseBody);
                Project[] projectList = JsonHelper.FromJson<Project>(json);
                return projectList;
            }
        }

        public static async Task<Project> CreateProject(Project toCreate)
        {
            string url = baseUrl + "projects/";

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            string json = JsonUtility.ToJson(toCreate);

            Response respone = await Rest.PostAsync(url, json, headers);
            if (!respone.Successful)
            {
                Debug.LogError(respone.ResponseBody);
                return null;
            }
            else
            {
                Project resultProject = JsonUtility.FromJson<Project>(respone.ResponseBody);
                return resultProject;
            }
        }

        /// <summary>
        /// Creates a new project
        /// </summary>
        /// <param name="project">The project object which should be created on the Requirements Bazaar</param>
        /// <returns>The created project</returns>
        //public static async Task<Project> CreateProject(Project project)
        //{
        //    string url = baseUrl + "projects";
        //    string json = JsonUtility.ToJson(project);
        //    Response response = await Rest.PostAsync(url, json);
        //    Debug.Log(response);
        //    return null;
        //}

        /// <summary>
        /// Retrieves a particular project by its given id
        /// </summary>
        /// <param name="projectId">The id of the project</param>
        /// <returns>The project with the given Id.</returns>
        public static async Task<Project> GetProject(int projectId)
        {
            string url = baseUrl + "projects/" + projectId.ToString();

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.GetAsync(url, headers);
            if (!response.Successful)
            {
                Debug.LogError(response.ResponseBody);
                return null;
            }
            else
            {
                Project project = JsonUtility.FromJson<Project>(response.ResponseBody);
                return project;
            }
        }

        public static async Task<Project> UpdateProject(Project toUpdate)
        {
            string url = baseUrl + "projects/" + toUpdate.id;

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            string json = JsonUtility.ToJson(toUpdate);

            Response response = await Rest.PutAsync(url, json, headers);
            if (!response.Successful)
            {
                Debug.LogError(response.ResponseBody);
                return null;
            }
            else
            {
                Project project = JsonUtility.FromJson<Project>(response.ResponseBody);
                return project;
            }
        }

        /// <summary>
        /// Retrieves the categories of a project
        /// </summary>
        /// <param name="projectId">The id of the project which contains the categories</param>
        /// <param name="page">The page number of the requirements list</param>
        /// <param name="per_page">The number of categories on one page</param>
        /// <param name="searchFilter">A search query string</param>
        /// <param name="sortingMode">How the requirements should be sorteds</param>
        /// <returns></returns>
        public static async Task<Category[]> GetProjectCategories
            (int projectId, int page = 0, int per_page = 10, string searchFilter = "",
            ProjectSortingMode sortingMode = ProjectSortingMode.DEFAULT)
        {
            string url = baseUrl + "projects/" + projectId.ToString() + "/categories?page=" + page.ToString()
                + "&per_page=" + per_page.ToString();
            if (sortingMode != ProjectSortingMode.DEFAULT)
            {
                url += "&sort=" + sortingMode.ToString().ToLower();
            }
            if (searchFilter != "")
            {
                searchFilter = CleanString(searchFilter);
                url += "&search=" + searchFilter;
            }

            Response response = await Rest.GetAsync(url);
            if (!response.Successful)
            {
                Debug.LogError(response.ResponseBody);
                return null;
            }
            else
            {
                string json = JsonHelper.EncapsulateInWrapper(response.ResponseBody);
                Category[] categoryList = JsonHelper.FromJson<Category>(json);
                return categoryList;
            }
        }

        /// <summary>
        /// Gets the contributors of a project
        /// </summary>
        /// <param name="projectId">The id of the project</param>
        /// <returns>Contributors object which contains lists contributors divided by their different roles</returns>
        public static async Task<Contributors> GetProjectContributors(int projectId)
        {
            string url = baseUrl + "projects/" + projectId.ToString() + "/contributors";

            Response response = await Rest.GetAsync(url);
            if (!response.Successful)
            {
                Debug.LogError(response.ResponseBody);
                return null;
            }
            else
            {
                Contributors contributors = JsonUtility.FromJson<Contributors>(response.ResponseBody);
                return contributors;
            }
        }

        /// <summary>
        /// Gets the followers of a project
        /// </summary>
        /// <param name="projectId">The id of the project</param>
        /// <param name="page">The page number of the list divided into pages</param>
        /// <param name="per_page">The number of entries that one page should contain</param>
        /// <returns>An arrow of the followers of a project</returns>
        public static async Task<User[]> GetProjectFollowers(int projectId, int page = 0, int per_page = 10)
        {
            string url = baseUrl + "projects/" + projectId.ToString()
                + "/followers?per_page=" + per_page.ToString();
            if (page > 0)
            {
                url += "&page=" + page.ToString();
            }

            Response response = await Rest.GetAsync(url);
            if (!response.Successful)
            {
                Debug.LogError(response.ResponseBody);
                return null;
            }
            else
            {
                string json = JsonHelper.EncapsulateInWrapper(response.ResponseBody);
                User[] followers = JsonHelper.FromJson<User>(json);
                return followers;
            }
        }

        public static async Task<Project> FollowProject(int projectId)
        {
            string url = baseUrl + "projects/" + projectId.ToString() + "/followers";

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.PostAsync(url, headers);
            if (!response.Successful)
            {
                Debug.LogError(response.ResponseBody);
                return null;
            }
            else
            {
                Project project = JsonUtility.FromJson<Project>(response.ResponseBody);
                return project;
            }
        }

        public static async Task<Requirement[]> GetProjectRequirements
            (int projectId, int page = 0, int per_page = 10, string search = "",
            RequirementState filterState = RequirementState.ALL, RequirementsSortingMode sortMode = RequirementsSortingMode.DEFAULT)
        {
            string url = baseUrl + "projects/" + projectId.ToString() + "/requirements";
            url += "?state=" + filterState.ToString().ToLower();
            url += "&per_page=" + per_page.ToString();

            if (page > 0)
            {
                url += "&page=" + page.ToString();
            }
            if (search != "")
            {
                search = CleanString(search);
                url += "&search=" + search;
            }
            if (sortMode != RequirementsSortingMode.DEFAULT)
            {
                url += "&sort=" + sortMode.ToString().ToLower();
            }

            Response response = await Rest.GetAsync(url);
            if (!response.Successful)
            {
                Debug.LogError(response.ResponseBody);
                return null;
            }
            else
            {
                string json = JsonHelper.EncapsulateInWrapper(response.ResponseBody);
                Requirement[] requirements = JsonHelper.FromJson<Requirement>(json);
                return requirements;
            }
        }

        public static async Task<Statistics> GetProjectStatistics(int projectId)
        {
            return await GetProjectStatistics(projectId, false, DateTime.Now);
        }

        public static async Task<Statistics> GetProjectStatistics(int projectId, DateTime since)
        {
            return await GetProjectStatistics(projectId, true, since);
        }

        private static async Task<Statistics> GetProjectStatistics(int projectId, bool useSince, DateTime since)
        {
            string url = baseUrl + "projects/" + projectId.ToString() + "/statistics";

            if (useSince)
            {
                string sinceParam = since.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
                url += "?since=" + sinceParam;
            }

            Response response = await Rest.GetAsync(url);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                Statistics statistics = JsonUtility.FromJson<Statistics>(response.ResponseBody);
                return statistics;
            }
        }

        #endregion

        #region Requirements

        public static async Task<Requirement> CreateRequirement(int projectId, string name, string description, int[] categoryIds = null)
        {
            string url = baseUrl + "requirements/";

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Category[] cats = null;

            // convert the supplied category ids to actual category data
            if (categoryIds != null)
            {
                cats = new Category[categoryIds.Length];
                for (int i=0; i<categoryIds.Length;i++)
                {
                    cats[i] = await GetCategory(categoryIds[i]);
                }
            }
            else // if no category was supplied: look for the default category and put it in there
            {
                Project proj = await GetProject(projectId);
                cats = new Category[1];
                cats[0] = await GetCategory(proj.defaultCategoryId);
            }

            JsonCreateRequirement toCreate = new JsonCreateRequirement() { projectId = projectId, name = name, description = description, categories = cats };

            string json = JsonUtility.ToJson(toCreate);

            Response response = await Rest.PostAsync(url, json, headers);
            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                Requirement requirement = JsonUtility.FromJson<Requirement>(response.ResponseBody);
                return requirement;
            }
        }

        public static async Task<Requirement> GetRequirement(int requirementId)
        {
            string url = baseUrl + "requirements/" + requirementId.ToString();

            Response response = await Rest.GetAsync(url);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                Requirement requirement = JsonUtility.FromJson<Requirement>(response.ResponseBody);
                return requirement;
            }
        }

        public static async Task<Attachment[]> GetRequirementAttachments(int requirementId, int page = 0, int per_page = 10)
        {
            string url = baseUrl + "requirements/" + requirementId + "/attachments?per_page=" + per_page.ToString();
            if (page > 0)
            {
                url += "&page=" + page.ToString();
            }


            Response response = await Rest.GetAsync(url);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                string json = JsonHelper.EncapsulateInWrapper(response.ResponseBody);
                Attachment[] attachments = JsonHelper.FromJson<Attachment>(json);
                return attachments;
            }
        }

        public static async Task<Comment[]> GetRequirementComments(int requirementId, int page = 0, int per_page = 10)
        {
            string url = baseUrl + "requirements/" + requirementId.ToString() + "/comments?per_page=" + per_page.ToString();
            if (page > 0)
            {
                url += "&page=" + page.ToString();
            }

            Response response = await Rest.GetAsync(url);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                string json = JsonHelper.EncapsulateInWrapper(response.ResponseBody);
                Comment[] comments = JsonHelper.FromJson<Comment>(json);
                return comments;
            }
        }

        public static async Task<RequirementContributors> GetRequirementContributors(int requirementId)
        {
            string url = baseUrl + "requirements/" + requirementId.ToString() + "/contributors";

            Response response = await Rest.GetAsync(url);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                RequirementContributors contributors = JsonUtility.FromJson<RequirementContributors>(response.ResponseBody);
                return contributors;
            }
        }

        public static async Task<User[]> GetRequirementDevelopers(int requirementId, int page = 0, int per_page = 10)
        {
            string url = baseUrl + "requirements/" + requirementId.ToString() + "/developers";
            url += "?per_page=" + per_page.ToString();

            if (page > 0)
            {
                url += "&page=" + page.ToString();
            }

            Response response = await Rest.GetAsync(url);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                string json = JsonHelper.EncapsulateInWrapper(response.ResponseBody);
                User[] developers = JsonHelper.FromJson<User>(json);
                return developers;
            }
        }

        public static async Task<User[]> GetRequirementFollowers(int requirementId, int page = 0, int per_page = 10)
        {
            string url = baseUrl + "requirements/" + requirementId.ToString() + "/followers";

            url += "?per_page=" + per_page.ToString();

            if (page > 0)
            {
                url += "&page=" + page.ToString();
            }

            Response response = await Rest.GetAsync(url);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                string json = JsonHelper.EncapsulateInWrapper(response.ResponseBody);
                User[] followers = JsonHelper.FromJson<User>(json);
                return followers;
            }
        }

        public static async Task<Statistics> GetRequirementStatistics(int requirementId)
        {
            return await GetRequirementStatistics(requirementId, false, DateTime.Now);
        }

        public static async Task<Statistics> GetRequirementStatistics(int requirementId, DateTime since)
        {
            return await GetRequirementStatistics(requirementId, true, since);
        }

        private static async Task<Statistics> GetRequirementStatistics(int requirementId, bool useSince, DateTime since)
        {
            string url = baseUrl + "requirements/" + requirementId.ToString() + "/statistics";

            if (useSince)
            {
                string sinceParam = since.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
                url += "?since=" + sinceParam;
            }

            Response response = await Rest.GetAsync(url);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                Statistics statistics = JsonUtility.FromJson<Statistics>(response.ResponseBody);
                return statistics;
            }
        }

        #endregion

        #region Comments

        public static async Task<Comment> GetComment(int commentId)
        {
            string url = baseUrl + "comments/" + commentId.ToString();

            Response response = await Rest.GetAsync(url);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                Comment comment = JsonUtility.FromJson<Comment>(response.ResponseBody);
                return comment;
            }
        }

        #endregion

        #region Categories

        public static async Task<Category> GetCategory(int categoryId)
        {
            string url = baseUrl + "categories/" + categoryId.ToString();

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.GetAsync(url, headers);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                Category category = JsonUtility.FromJson<Category>(response.ResponseBody);
                return category;
            }
        }

        public static async Task<Contributors> GetCategoryContributors(int categoryId)
        {
            string url = baseUrl + "categories/" + categoryId.ToString() + "/contributors";

            Response response = await Rest.GetAsync(url);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                Contributors contributors = JsonUtility.FromJson<Contributors>(response.ResponseBody);
                return contributors;
            }
        }

        public static async Task<User[]> GetCategoryFollowers(int categoryId, int page = 0, int per_page = 10)
        {
            string url = baseUrl + "categories/" + categoryId.ToString() + "/followers";

            Response response = await Rest.GetAsync(url);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                string json = JsonHelper.EncapsulateInWrapper(response.ResponseBody);
                User[] followers = JsonHelper.FromJson<User>(json);
                return followers;
            }
        }

        public static async Task<Requirement[]> GetCategoryRequirements
            (int categoryId, int page = 0, int per_page = 10, string search = "",
            RequirementState filterState = RequirementState.ALL, RequirementsSortingMode sortMode = RequirementsSortingMode.DEFAULT)
        {
            string url = baseUrl + "categories/" + categoryId.ToString() + "/requirements";
            url += "?state=" + filterState.ToString().ToLower();
            url += "&per_page=" + per_page.ToString();

            if (page > 0)
            {
                url += "&page=" + page.ToString();
            }
            if (search != "")
            {
                search = CleanString(search);
                url += "&search=" + search;
            }
            if (sortMode != RequirementsSortingMode.DEFAULT)
            {
                url += "&sort=" + sortMode.ToString().ToLower();
            }

            Response response = await Rest.GetAsync(url);
            if (!response.Successful)
            {
                Debug.LogError(response.ResponseBody);
                return null;
            }
            else
            {
                string json = JsonHelper.EncapsulateInWrapper(response.ResponseBody);
                Requirement[] requirements = JsonHelper.FromJson<Requirement>(json);
                return requirements;
            }
        }

        public static async Task<Statistics> GetCategoryStatistics(int requirementId)
        {
            return await GetCategoryStatistics(requirementId, false, DateTime.Now);
        }

        public static async Task<Statistics> GetCategoryStatistics(int requirementId, DateTime since)
        {
            return await GetCategoryStatistics(requirementId, true, since);
        }

        private static async Task<Statistics> GetCategoryStatistics(int requirementId, bool useSince, DateTime since)
        {
            string url = baseUrl + "categories/" + requirementId.ToString() + "/statistics";

            if (useSince)
            {
                string sinceParam = since.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
                url += "?since=" + sinceParam;
            }

            Response response = await Rest.GetAsync(url);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                Statistics statistics = JsonUtility.FromJson<Statistics>(response.ResponseBody);
                return statistics;
            }
        }

        #endregion

        #region Attachments

        public static async Task<Attachment> GetAttachment(int attachmentId)
        {
            string url = baseUrl + "attachments/" + attachmentId.ToString();

            Response response = await Rest.GetAsync(url);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                Attachment attachment = JsonUtility.FromJson<Attachment>(response.ResponseBody);
                return attachment;
            }
        }

        #endregion

        #region Users

        public static async Task<User> GetUser()
        {
            string url = baseUrl + "users/me";

            Response response = await Rest.GetAsync(url);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                User me = JsonUtility.FromJson<User>(response.ResponseBody);
                return me;
            }
        }

        public static async Task<User> GetUser(int userId)
        {
            string url = baseUrl + "users/" + userId.ToString();

            Response response = await Rest.GetAsync(url);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                User user = JsonUtility.FromJson<User>(response.ResponseBody);
                return user;
            }
        }

        #endregion

        #region Default

        public static async Task<Statistics> GetGlobalStatistics()
        {
            return await GetGlobalStatistics(false, DateTime.Now);
        }

        public static async Task<Statistics> GetGlobalStatistics(DateTime since)
        {
            return await GetGlobalStatistics(true, since);
        }

        private static async Task<Statistics> GetGlobalStatistics(bool useSince, DateTime since)
        {
            string url = baseUrl + "statistics";

            if (useSince)
            {
                string sinceParam = since.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
                url += "?since=" + sinceParam;
            }

            Response response = await Rest.GetAsync(url);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                Statistics statistics = JsonUtility.FromJson<Statistics>(response.ResponseBody);
                return statistics;
            }
        }

        public static async Task<APIVersion> GetAPIVersion()
        {
            string url = baseUrl + "version";

            Response response = await Rest.GetAsync(url);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                APIVersion version = JsonUtility.FromJson<APIVersion>(response.ResponseBody);
                return version;
            }
        }

        #endregion

        /// <summary>
        /// Cleans a string of potenitally dangerous symbols, e.g. to avoid open redirection injection in parameters
        /// </summary>
        /// <returns></returns>
        private static string CleanString(string s)
        {
            s = s.Replace("/", "");
            s = s.Replace("\\", "");
            s = s.Replace(":", "");
            s = s.Replace("&", "");
            s = s.Replace("§", "");
            s = s.Replace("$", "");
            return s;
        }
    }

    public enum ProjectSortingMode
    {
        DEFAULT, NAME, LAST_ACTIVITY, REQUIREMENT, FOLLOWER
    }

    public enum RequirementsSortingMode
    {
        DEFAULT, DATE, LAST_ACTIVITY, NAME, VOTE, COMMENT, FOLLOWER, REALIZED
    }

    public enum RequirementState
    {
        ALL, OPEN, REALIZED
    }

}
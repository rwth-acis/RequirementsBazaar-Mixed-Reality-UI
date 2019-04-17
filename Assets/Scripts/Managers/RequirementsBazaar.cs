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

        #region Projects

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

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.GetAsync(url, headers);
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

        /// <summary>
        /// Creates the given project object on the server
        /// </summary>
        /// <param name="toCreate">The project to create.</param>
        /// <returns>The set up project.
        /// Please use this result for further computations (instead of the toCreate object) since the server filled out all fields, e.g. an id was assigned</returns>
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

        /// <summary>
        /// Updates the project by uploading it to the server
        /// Recommended usage: First retrieve the project object using GetProject, then change its values and then pass it to this method.
        /// </summary>
        /// <param name="toUpdate">The project which should be updated on the server.</param>
        /// <returns>The resulting, changed project as it has been stored on the server.</returns>
        public static async Task<Project> UpdateProject(Project toUpdate)
        {
            string url = baseUrl + "projects/" + toUpdate.Id;

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

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.GetAsync(url, headers);
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

            // does not require authorization

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

            // doesn not requrie authorization

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

        /// <summary>
        /// When called, the user will follow the project
        /// </summary>
        /// <param name="projectId">The id of the project to follow</param>
        /// <returns>The project which has been followed</returns>
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

        /// <summary>
        /// Gets the statistics for the given project
        /// </summary>
        /// <param name="projectId">The project which should be analyzed</param>
        /// <returns></returns>
        public static async Task<Statistics> GetProjectStatistics(int projectId)
        {
            return await GetProjectStatistics(projectId, false, DateTime.Now);
        }

        /// <summary>
        /// Gets the statistics for the given project starting at a point in the past until the present
        /// </summary>
        /// <param name="projectId">The project which should be analyzed</param>
        /// <param name="since">Starting point from which onwards the statistics should be counted</param>
        /// <returns></returns>
        public static async Task<Statistics> GetProjectStatistics(int projectId, DateTime since)
        {
            return await GetProjectStatistics(projectId, true, since);
        }

        /// <summary>
        /// Combination method for the GetProjectStatistics calls with and without a "since" parameter
        /// Gets the project statistics according to the settings whether a starting point should be used or not
        /// </summary>
        /// <param name="projectId">The project which should be analyzed</param>
        /// <param name="useSince">True if the since parameter should be regarded, else the statistics from the complete past will be fetched</param>
        /// <param name="since">Starting point from which onwards the statistics should be counted (only regarded if useSince is true)</param>
        /// <returns>The project statistics</returns>
        private static async Task<Statistics> GetProjectStatistics(int projectId, bool useSince, DateTime since)
        {
            string url = baseUrl + "projects/" + projectId.ToString() + "/statistics";

            if (useSince)
            {
                string sinceParam = since.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
                url += "?since=" + sinceParam;
            }

            // does not require authorization

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

        /// <summary>
        /// Creates and posts a new requirement
        /// </summary>
        /// <param name="projectId">The id of the project where the requirement will be posted</param>
        /// <param name="name">The name/title of the requirement</param>
        /// <param name="description">The description of the requirement</param>
        /// <param name="categories">Categories in the project</param>
        /// <returns>The resulting requirement as it was saved on the server</returns>
        public static async Task<Requirement> CreateRequirement(int projectId, string name, string description, Category[] categories = null)
        {
            string url = baseUrl + "requirements/";

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            // check if categories were supplied; if no: look for the default category and it in there
            if (categories == null)
            {
                Project proj = await GetProject(projectId);
                categories = new Category[1];
                categories[0] = await GetCategory(proj.DefaultCategoryId);
            }

            UploadableRequirement toCreate = new UploadableRequirement(name, description, projectId, categories);

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

        /// <summary>
        /// Creates and posts a new requirement
        /// </summary>
        /// <param name="projectId">The id of the project where the requirement will be posted</param>
        /// <param name="name">The name/title of the requirement</param>
        /// <param name="description">The description of the requirement</param>
        /// <param name="categoryIds">An array of category ids in which the requirements will be put</param>
        /// <returns>The resulting requirement as it was saved on the server</returns>
        public static async Task<Requirement> CreateRequirement(int projectId, string name, string description, int[] categoryIds)
        {

            // convert the supplied category ids to actual category data
            Category[] cats = new Category[categoryIds.Length];
            for (int i = 0; i < categoryIds.Length; i++)
            {
                cats[i] = await GetCategory(categoryIds[i]);
            }

            return await CreateRequirement(projectId, name, description, cats);
        }

        /// <summary>
        /// Gets a specific requirement by its id
        /// </summary>
        /// <param name="requirementId">The id of the requirement which should be fetched</param>
        /// <returns>The fetched requirement</returns>
        public static async Task<Requirement> GetRequirement(int requirementId)
        {
            string url = baseUrl + "requirements/" + requirementId.ToString();

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.GetAsync(url, headers);

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

        /// <summary>
        /// Updates the specified requirement
        /// Please first query for the requirement to change, then changing its parameters and then supply it to this function
        /// </summary>
        /// <param name="toUpdate">The requirement to update</param>
        /// <returns>The updated requirement</returns>
        public static async Task<Requirement> UpdateRequirement(Requirement toUpdate)
        {
            string url = baseUrl + "requirements/" + toUpdate.Id;

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();
            // convert the requirement to a uploadable format (the statistic fields of the requirement are not recognized as input by the service)
            UploadableRequirement uploadableRequirement = toUpdate.ToUploadFormat();
            string json = JsonUtility.ToJson(uploadableRequirement);

            Response response = await Rest.PutAsync(url, json, headers);
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

        /// <summary>
        /// Gets a list of attachments which have been added to the specified requirement, divided into pages
        /// </summary>
        /// <param name="requirementId">The id of the requirement</param>
        /// <param name="page">The page number in the list</param>
        /// <param name="per_page">Specifies how many items should be on one page</param>
        /// <returns>The array of attachments on the given page of the specified requirement</returns>
        public static async Task<Attachment[]> GetRequirementAttachments(int requirementId, int page = 0, int per_page = 10)
        {
            string url = baseUrl + "requirements/" + requirementId + "/attachments?per_page=" + per_page.ToString();
            if (page > 0)
            {
                url += "&page=" + page.ToString();
            }

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.GetAsync(url, headers);

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

        /// <summary>
        /// Gets the list of comments on a specific requirement, divided into pages
        /// </summary>
        /// <param name="requirementId">The id of the requirement</param>
        /// <param name="page">The page number in the list</param>
        /// <param name="per_page">Specifies how many items should be on one page</param>
        /// <returns>The array of comments on the given page</returns>
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

        /// <summary>
        /// Returns the contributors of a requirement
        /// </summary>
        /// <param name="requirementId">The id of the requirement</param>
        /// <returns>The contributors in a dedicated contributors object format</returns>
        public static async Task<RequirementContributors> GetRequirementContributors(int requirementId)
        {
            string url = baseUrl + "requirements/" + requirementId.ToString() + "/contributors";

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.GetAsync(url, headers);

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

        /// <summary>
        /// Gets the developers of a requirement, divided into pages
        /// </summary>
        /// <param name="requirementId">The id of the requirement</param>
        /// <param name="page">The page number</param>
        /// <param name="per_page">Specifies how many items should be on one page</param>
        /// <returns>An array with the developer entries on the specified page</returns>
        public static async Task<User[]> GetRequirementDevelopers(int requirementId, int page = 0, int per_page = 10)
        {
            string url = baseUrl + "requirements/" + requirementId.ToString() + "/developers";
            url += "?per_page=" + per_page.ToString();

            if (page > 0)
            {
                url += "&page=" + page.ToString();
            }

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.GetAsync(url, headers);

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

        /// <summary>
        /// Sets the currently logged in user as a developer of the requirement
        /// </summary>
        /// <param name="requirementId">The id of the requirement</param>
        /// <returns>The requirement which the logged in user is now developing</returns>
        public static async Task<Requirement> BecomeDeveloperOfRequirement(int requirementId)
        {
            string url = baseUrl + "requirements/" + requirementId + "/developers";

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.PostAsync(url, headers);
            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                Requirement req = JsonUtility.FromJson<Requirement>(response.ResponseBody);
                return req;
            }
        }

        /// <summary>
        /// Returns the followers of a requirement
        /// </summary>
        /// <param name="requirementId">The id of the requirement</param>
        /// <param name="page">The page number in the list</param>
        /// <param name="per_page">Specifies how many items should be on one page</param>
        /// <returns>An array of users which follow the requirement (and are on the specified page)</returns>
        public static async Task<User[]> GetRequirementFollowers(int requirementId, int page = 0, int per_page = 10)
        {
            string url = baseUrl + "requirements/" + requirementId.ToString() + "/followers";

            url += "?per_page=" + per_page.ToString();

            if (page > 0)
            {
                url += "&page=" + page.ToString();
            }

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.GetAsync(url, headers);

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

        /// <summary>
        /// Makes the currently logged in user follow the requirement
        /// </summary>
        /// <param name="requirementId">The id of the requirement</param>
        /// <returns>The requirement with the given id</returns>
        public static async Task<Requirement> FollowRequirement(int requirementId)
        {
            string url = baseUrl + "requirements/" + requirementId.ToString() + "/followers";

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.PostAsync(url, headers);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                Requirement resRequirement = JsonUtility.FromJson<Requirement>(response.ResponseBody);
                return resRequirement;
            }
        }

        /// <summary>
        /// Makes the currently logged in user the lead developer of the requirement
        /// </summary>
        /// <param name="requirementId">The id of the requiremnt</param>
        /// <returns>The specified requirement</returns>
        public static async Task<Requirement> BecomeLeadDeveloperOfRequirement(int requirementId)
        {
            string url = baseUrl + "requirements/" + requirementId.ToString() + "/leaddevelopers";

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.PostAsync(url, headers);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                Requirement resRequirement = JsonUtility.FromJson<Requirement>(response.ResponseBody);
                return resRequirement;
            }
        }

        /// <summary>
        /// Gets the all-time statistics of the specified requirement
        /// </summary>
        /// <param name="requirementId">The id of the requirement</param>
        /// <returns>The statistics of the requirement</returns>
        public static async Task<Statistics> GetRequirementStatistics(int requirementId)
        {
            return await GetRequirementStatistics(requirementId, false, DateTime.Now);
        }

        /// <summary>
        /// Gets teh statistics of the specified requirement since some date
        /// </summary>
        /// <param name="requirementId">The id of the requirement</param>
        /// <param name="since">The starting point in time since when the statistics should be counted</param>
        /// <returns>The statistics of teh requirement</returns>
        public static async Task<Statistics> GetRequirementStatistics(int requirementId, DateTime since)
        {
            return await GetRequirementStatistics(requirementId, true, since);
        }

        /// <summary>
        /// Internal function for getting the statistics of a requirement by giving the option of using a since-date
        /// </summary>
        /// <param name="requirementId">The id of the requirement</param>
        /// <param name="useSince">If true, the since paramter is regarded as the starting point for the statistics</param>
        /// <param name="since">Date since when the statistics should count</param>
        /// <returns>The statistics of the requirement</returns>
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

        /// <summary>
        /// Lets the currently logged in user vote for or against a requirement
        /// </summary>
        /// <param name="requirementId">The id of the requirement</param>
        /// <param name="direction">The voting direction; by default UP</param>
        /// <returns>The requirement for which the user voted</returns>
        public static async Task<Requirement> VoteForRequirement(int requirementId, VotingDirection direction = VotingDirection.UP)
        {
            string url = baseUrl + "requirements/" + requirementId.ToString() + "/votes?direction=" + direction.ToString().ToLower();

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.PostAsync(url, headers);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                Requirement resRequirement = JsonUtility.FromJson<Requirement>(response.ResponseBody);
                return resRequirement;
            }
        }

        /// <summary>
        /// Deletes/undoes the vote of the currently logged in user one a specified requirement
        /// </summary>
        /// <param name="requirementId">The requirement where the vote should be undone</param>
        /// <returns>The updated requirement</returns>
        public static async Task<Requirement> UndoVote(int requirementId)
        {
            string url = baseUrl + "requirements/" + requirementId.ToString() + "/votes";

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.DeleteAsync(url, headers);

            if (!response.Successful)
            {
                Debug.LogError(response.ResponseCode + ": " + response.ResponseBody);
                return null;
            }
            else
            {
                Requirement resRequirement = JsonUtility.FromJson<Requirement>(response.ResponseBody);
                return resRequirement;
            }
        }

        #endregion

        #region Comments

        /// <summary>
        /// Creates a comment on the server
        /// </summary>
        /// <param name="toPost">The comment which should be posted to the requirement</param>
        /// <returns>The created comment</returns>
        public static async Task<Comment> CreateComment(Comment toPost)
        {
            string url = baseUrl + "comments/";

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            string json;
            if (toPost.IsReplyingToOtherComment)
            {
                json = JsonUtility.ToJson(toPost);
            }
            else
            {
                NoReplyComment converted = toPost.ToNoReplyComment();
                json = JsonUtility.ToJson(converted);
            }

            Response response = await Rest.PostAsync(url, json, headers);

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

        /// <summary>
        /// Gets a specific comment by its id
        /// </summary>
        /// <param name="commentId">The id of the comment</param>
        /// <returns>The comment</returns>
        public static async Task<Comment> GetComment(int commentId)
        {
            string url = baseUrl + "comments/" + commentId.ToString();

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.GetAsync(url, headers);

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

        /// <summary>
        /// Creates a category on the server
        /// </summary>
        /// <param name="toCreate">The category to create</param>
        /// <returns>The created category</returns>
        public static async Task<Category> CreateCategory(Category toCreate)
        {
            string url = baseUrl + "categories/";

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            string json = JsonUtility.ToJson(toCreate);

            Response response = await Rest.PostAsync(url, json, headers);

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

        /// <summary>
        /// Gets a category by its ID
        /// </summary>
        /// <param name="categoryId">The ID of the category</param>
        /// <returns>The category</returns>
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

        /// <summary>
        /// Updates a given category.
        /// Recommended usage: Download the category using GetCategory, then change its properties and then pass it to this method to update it on the server.
        /// </summary>
        /// <param name="toUpdate">The category to update</param>
        /// <returns>The updated category as it was saved on the server</returns>
        public static async Task<Category> UpdateCategory(Category toUpdate)
        {
            string url = baseUrl + "categories/" + toUpdate.Id.ToString();

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            string json = JsonUtility.ToJson(toUpdate);

            Response response = await Rest.PutAsync(url, json, headers);

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

        /// <summary>
        /// Gets the contributors to a specific category
        /// </summary>
        /// <param name="categoryId">The ID of the category</param>
        /// <returns>The contributors for this category</returns>
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

        /// <summary>
        /// Returns the users which are following a category
        /// The list of users is divided into pages and the method will only return an array of the users on the specified page.
        /// </summary>
        /// <param name="categoryId">The ID of the category</param>
        /// <param name="page">The page number: Specifies which chunk of the list should be returned</param>
        /// <param name="per_page">Specifies how many entries are on one page</param>
        /// <returns>A list of followers on the specified page</returns>
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

        /// <summary>
        /// Makes the currently logged in user follow the specified category
        /// </summary>
        /// <param name="categoryId">The id of the category to follow</param>
        /// <returns>The category with the specified ID</returns>
        public static async Task<Category> FollowCategory(int categoryId)
        {
            string url = baseUrl + "categories/" + categoryId.ToString() + "/followers";

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.PostAsync(url, headers);

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

        /// <summary>
        /// Gets the requirements in a category
        /// This method is able to sort and filter the requirements.
        /// </summary>
        /// <param name="categoryId">The ID to the category</param>
        /// <param name="page">Specifies the page number of the list chunks</param>
        /// <param name="per_page">Specifies how many entries should be on one page</param>
        /// <param name="search">Search expression for filtering requirements</param>
        /// <param name="filterState">Filters the requirements by their done-state</param>
        /// <param name="sortMode">Specifies how the requirements should be sorted</param>
        /// <returns>An array of requirements which match the filter(s) and which are on the specified page</returns>
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

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.GetAsync(url, headers);
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

        /// <summary>
        /// Gets the statistics of a category
        /// </summary>
        /// <param name="categoryId">The ID of the category</param>
        /// <returns>Statistics about the specified category</returns>
        public static async Task<Statistics> GetCategoryStatistics(int categoryId)
        {
            return await GetCategoryStatistics(categoryId, false, DateTime.Now);
        }

        /// <summary>
        /// Gets the statistics of a category since some date
        /// </summary>
        /// <param name="categoryId">The ID of the category</param>
        /// <param name="since">Time from which point onwards, statistics should be counted</param>
        /// <returns>Statistics from the given point in time until now</returns>
        public static async Task<Statistics> GetCategoryStatistics(int categoryId, DateTime since)
        {
            return await GetCategoryStatistics(categoryId, true, since);
        }

        /// <summary>
        /// Gets the statistics of a category
        /// Internal method which allows to specify whether or not a "since"-date should be used
        /// </summary>
        /// <param name="categoryId">The ID of the category</param>
        /// <param name="useSince">If set to true, since will be considered, else all-time statistics will be counted</param>
        /// <param name="since">Time from which point onwards, statistics should be counted</param>
        /// <returns>The statistics of the category</returns>
        private static async Task<Statistics> GetCategoryStatistics(int categoryId, bool useSince, DateTime since)
        {
            string url = baseUrl + "categories/" + categoryId.ToString() + "/statistics";

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

        public static async Task<User> GetMyUser()
        {
            string url = baseUrl + "users/me";

            Dictionary<string, string> headers = Utilities.GetStandardHeaders();

            Response response = await Rest.GetAsync(url, headers);

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
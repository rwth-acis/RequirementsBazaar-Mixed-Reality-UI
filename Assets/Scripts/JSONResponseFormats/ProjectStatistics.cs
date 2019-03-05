using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Org.Requirements_Bazaar.DataModel
{
    /// <summary>
    /// The statistics of the entire Requirements Bazaar
    /// </summary>
    [Serializable]
    public class Statistics
    {
        [SerializeField] private int numberOfProjects;
        [SerializeField] private int numberOfCategories;
        [SerializeField] private int numberOfRequirements;
        [SerializeField] private int numberOfComments;
        [SerializeField] private int numberOfAttachments;
        [SerializeField] private int numberOfVotes;

        #region Properties

        /// <summary>
        /// Counts the number of projects
        /// </summary>
        public int NumberOfProjects
        {
            get
            {
                return numberOfProjects;
            }
        }

        /// <summary>
        /// Counts the overall number of categories
        /// </summary>
        public int NumberOfCategories
        {
            get
            {
                return numberOfCategories;
            }
        }

        /// <summary>
        /// Counts the overall number of requirements
        /// </summary>
        public int NumberOfRequirements
        {
            get
            {
                return numberOfRequirements;
            }
        }

        /// <summary>
        /// Counts the overall number of comments
        /// </summary>
        public int NumberOfComments
        {
            get
            {
                return numberOfComments;
            }
        }

        /// <summary>
        /// Counts the overall number of attachments
        /// </summary>
        public int NumberOfAttachments
        {
            get
            {
                return numberOfAttachments;
            }
        }

        /// <summary>
        /// Counts the overall number of votes
        /// </summary>
        public int NumberOfVotes
        {
            get
            {
                return numberOfVotes;
            }
        }

        #endregion
    }

}

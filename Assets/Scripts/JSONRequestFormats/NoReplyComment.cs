using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Org.Requirements_Bazaar.DataModel
{

    [Serializable]
    /// <summary>
    /// A comment data structure for upload only
    /// This is used if the comment does not reply to another comment (the API expects the field replyToComment to be ommitted in this case)
    /// </summary>
    public class NoReplyComment
    {
        [SerializeField] private string message;
        [SerializeField] private int requirementId;

        #region Properties

        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }

        public int RequirementId
        {
            get
            {
                return requirementId;
            }
        }

        #endregion

        public NoReplyComment(string message, int requirementId)
        {
            this.message = message;
            this.requirementId = requirementId;
        }
    }

}

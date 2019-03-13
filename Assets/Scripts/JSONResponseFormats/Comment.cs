using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Org.Requirements_Bazaar.DataModel
{

    [Serializable]
    public class Comment
    {
        [SerializeField] private int id;
        [SerializeField] private string message;
        [SerializeField] private int replyToComment;
        [SerializeField] private int requirementId;
        [SerializeField] private User creator;
        [SerializeField] private string creationDate;
        [SerializeField] private string lastUpdatedDate;


        #region Properties

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string Message
        {
            get
            {
                return message;
            }
        }

        public int ReplyToComment
        {
            get
            {
                return replyToComment;
            }
        }

        public int RequirementId
        {
            get
            {
                return requirementId;
            }
        }

        public User Creator
        {
            get
            {
                return creator;
            }
        }

        public string CreationDate
        {
            get
            {
                return creationDate;
            }
        }

        public string LastUpdatedDate
        {
            get
            {
                return lastUpdatedDate;
            }
        }

        public bool IsReplyingToOtherComment
        {
            get { return replyToComment != 0; }
        }

        #endregion

        public Comment(string message, int requirementId) : this(message, requirementId, 0)
        {
        }

        public Comment(string message, int requirementId, int replyToComment)
        {
            this.message = message;
            this.requirementId = requirementId;
            this.replyToComment = replyToComment;
        }

        public NoReplyComment ToNoReplyComment()
        {
            if (replyToComment != 0)
            {
                Debug.LogWarning("Converted a replying comment to a NoReplyComment. This is most likely an error and should not be done.");
            }

            NoReplyComment noReplyComment = new NoReplyComment(message, requirementId);
            return noReplyComment;
        }
    }

}

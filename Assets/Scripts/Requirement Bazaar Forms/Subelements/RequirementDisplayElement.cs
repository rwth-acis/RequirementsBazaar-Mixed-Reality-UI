using Org.Requirements_Bazaar.API;
using Org.Requirements_Bazaar.Common;
using Org.Requirements_Bazaar.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Org.Requirements_Bazaar.AR_VR_Forms
{

    public class RequirementDisplayElement : MonoBehaviour
    {
        [SerializeField] private Text titleLabel;
        [SerializeField] private Text descriptionLabel;
        [SerializeField] private Text votesLabel;
        [SerializeField] private Button voteButton;
        [SerializeField] private Image voteImage;
        [SerializeField] private Sprite notVotedSprite;
        [SerializeField] private Sprite votedSprite;

        private Requirement requirement;

        private Transform canvas;

        public Requirement Requirement
        {
            get { return requirement; }
            set
            {
                requirement = value;
                UpdateDisplay();
            }
        }

        private void Start()
        {
            canvas = Utilities.GetHighestParent(transform);
        }

        private void UpdateDisplay()
        {
            Debug.Log("Updated display for req " + Requirement.Name);
            titleLabel.text = requirement.Name;
            descriptionLabel.text = requirement.Description;
            votesLabel.text = requirement.UpVotes.ToString();
            if (requirement.UserVoted == UserVoted.UP_VOTE)
            {
                voteImage.sprite = votedSprite;
            }
            // could include a case for voting down here
            // for now, no vote and voting down are both displayed with the notVotedSprite
            else
            {
                voteImage.sprite = notVotedSprite;
            }
        }

        public void OnClick()
        {
            // instantiate the next sub-page: the create/edit requirement page
            GameObject createEditPage = Instantiate(RequirementsBazaarUI.Instance.CreateEditRequirementPage);
            createEditPage.transform.position = canvas.position;
            RequirementsBazaarCreateEditRequirementForm createEditForm = createEditPage.GetComponent<RequirementsBazaarCreateEditRequirementForm>();
            createEditForm.RequirementId = Requirement.Id;
            createEditForm.PageClosed += OnInstantiatedPageClosed;

            canvas.gameObject.SetActive(false);
        }

        public async void OnVoteClick()
        {
            voteButton.interactable = false;
            if (requirement.UserVoted != UserVoted.NO_VOTE) // if user already voted => undo by voting down
            {
                // delete vote
            }
            else // if user did not yet vote => vote up; this is the standard case; no voting down possible with this UI
            {
                Requirement = await RequirementsBazaar.VoteForRequirement(Requirement.Id);
            }
            voteButton.interactable = true;
        }

        private void OnInstantiatedPageClosed(object sender, EventArgs e)
        {
            canvas.gameObject.SetActive(true);
        }
    }

}
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
            titleLabel.text = requirement.name;
            descriptionLabel.text = requirement.description;
            votesLabel.text = requirement.upVotes.ToString();
        }

        public void OnClick()
        {
            // instantiate the next sub-page: the create/edit requirement page
            GameObject createEditPage = Instantiate(RequirementsBazaarUI.Instance.CreateEditRequirementPage);
            createEditPage.transform.position = canvas.position;
            RequirementsBazaarCreateEditRequirementForm createEditForm = createEditPage.GetComponent<RequirementsBazaarCreateEditRequirementForm>();
            createEditForm.RequirementId = Requirement.id;
            createEditForm.PageClosed += OnInstantiatedPageClosed;

            canvas.gameObject.SetActive(false);
        }

        private void OnInstantiatedPageClosed(object sender, EventArgs e)
        {
            canvas.gameObject.SetActive(true);
        }
    }

}
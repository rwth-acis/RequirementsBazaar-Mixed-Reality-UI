using Org.Requirements_Bazaar.API;
using Org.Requirements_Bazaar.DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Org.Requirements_Bazaar.AR_VR_Forms
{

    public class RequirementsBazaarRequirementsInCategoryForm : RequirementsBazaarForm
    {
        [SerializeField] private int categoryId;
        public int requirementsPerPage = 4;
        [SerializeField] private RectTransform requirementDisplayElementTemplate;
        [SerializeField] private Button upButton;
        [SerializeField] private Button downButton;

        private int page = 0;
        private Requirement[] currentPage, nextPage;
        private bool instantiatedByCode = false;

        public int CategoryId
        {
            get { return categoryId; }
            set
            {
                categoryId = value;
                instantiatedByCode = true;
                UpdateDisplay();
            }
        }

        private void Start()
        {
            if (!instantiatedByCode)
            {
                UpdateDisplay();
            }
        }

        public void OnUpButtonPressed()
        {
            page--;
            UpdateDisplay();
        }

        public void OnDownButtonPressed()
        {
            if (nextPage != null && nextPage.Length > 0)
            {
                page++;
                UpdateDisplay();
            }
        }

        private async void UpdateDisplay()
        {
            upButton.enabled = false;
            downButton.enabled = false;
            ClearRequirementDisplayElements();

            // get the current page and the next page
            // the next page is required to check if we are on the last page and to enable/disable downButton accordingly
            currentPage = await RequirementsBazaar.GetCategoryRequirements(categoryId, page, requirementsPerPage);
            nextPage = await RequirementsBazaar.GetCategoryRequirements(categoryId, page + 1, requirementsPerPage);

            if (page == 0)
            {
                upButton.enabled = false;
            }
            else
            {
                upButton.enabled = true;
            }

            if (nextPage.Length == 0)
            {
                downButton.enabled = false;
            }
            else
            {
                downButton.enabled = true;
            }

            for (int i = 0; i < currentPage.Length; i++)
            {
                CreateRequirementsDisplayElement(currentPage[i]);
            }
        }

        private void CreateRequirementsDisplayElement(Requirement requirement)
        {
            Debug.Log("Creating element for " + requirement.Name + "; " + requirement.UserVoted);
            RectTransform requirementDisplayTransform = Instantiate(requirementDisplayElementTemplate, requirementDisplayElementTemplate.parent);
            requirementDisplayTransform.name = "Requirement (ID " + requirement.Id + ")";
            requirementDisplayTransform.gameObject.SetActive(true);
            RequirementDisplayElement requirementDisplayElement = requirementDisplayTransform.GetComponent<RequirementDisplayElement>();
            requirementDisplayElement.Requirement = requirement;
        }

        private void ClearRequirementDisplayElements()
        {
            foreach (RectTransform child in requirementDisplayElementTemplate.parent)
            {
                if (child != requirementDisplayElementTemplate)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}
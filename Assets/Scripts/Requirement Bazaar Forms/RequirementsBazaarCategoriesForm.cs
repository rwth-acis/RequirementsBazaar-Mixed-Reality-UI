using Org.Requirements_Bazaar.API;
using Org.Requirements_Bazaar.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Org.Requirements_Bazaar.AR_VR_Forms
{
    /// <summary>
    /// Controls the UI for displaying the categories of a project
    /// </summary>
    public class RequirementsBazaarCategoriesForm : RequirementsBazaarForm
    {
        [Tooltip("The ID of the project whose categories should be displayed")]
        [SerializeField] private int projectId;

        [Tooltip("This template will be instantiated for each displayed category")]
        [SerializeField] private RectTransform categoryDisplayTemplate;

        [Tooltip("The reference to the up button")]
        [SerializeField] private Button upButton;

        [Tooltip("The reference to the down button")]
        [SerializeField] private Button downButton;

        private int page = 0; // current page
        private const int categoriesPerPage = 4; // how many categories should be displayed per page? This value should be set according to the number of categories which can visually fit onto the form
        private Category[] currentPage, nextPage; // arrays of the categories of the current and next page
        private bool instantiatedByCode = false; // true if the form was created by code, false if it was placed in the scene by the developer

        /// <summary>
        /// The ID of the project whose categories should be displayed
        /// </summary>
        public int ProjectId
        {
            get
            {
                return projectId;
            }
            set
            {
                projectId = value;
                instantiatedByCode = true; // the project id is changed by other code when the object is instantiated
                UpdateDisplay();
            }
        }

        private void Start()
        {
            // if the developer created the form in the scene and set it up manually => update the display to fetch the data according to the configuration
            // if it was instantiated by code, this is handled at the moment where the ID configuration is set
            if (!instantiatedByCode)
            {
                UpdateDisplay();
            }
        }

        /// <summary>
        /// Called when the up button is pressed
        /// Goes one page up and updates the display accordingly
        /// </summary>
        public void OnUpButtonPressed()
        {
            page--;
            UpdateDisplay();
        }

        /// <summary>
        /// Called when the down button is pressed
        /// Goes one page down (if there is still something to display) and updates teh display accordingly
        /// </summary>
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
            // disable all interactive controls so that the user cannot invoke multiple asynchronous commands at once
            upButton.enabled = false;
            downButton.enabled = false;
            // delete the currently shown category elements so that new category display can be shown instead
            ClearCategoryDisplayElements();

            // get the current page and the next page
            // the next page is required to check if we are on the last page and to enable/disable downButton accordingly
            currentPage = await RequirementsBazaar.GetProjectCategories(projectId, page, categoriesPerPage);
            nextPage = await RequirementsBazaar.GetProjectCategories(projectId, page + 1, categoriesPerPage);

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
                CreateCategoryDisplayElement(currentPage[i]);
            }
        }

        private void CreateCategoryDisplayElement(Category category)
        {
            RectTransform categoryDisplayTransform = Instantiate(categoryDisplayTemplate, categoryDisplayTemplate.parent);
            categoryDisplayTransform.name = "Category (ID " + category.id + ")";
            categoryDisplayTransform.gameObject.SetActive(true);
            CategoryDisplayElement categoryDisplayElement = categoryDisplayTransform.GetComponent<CategoryDisplayElement>();
            categoryDisplayElement.Category = category;
        }

        private void ClearCategoryDisplayElements()
        {
            foreach (RectTransform child in categoryDisplayTemplate.parent)
            {
                if (child != categoryDisplayTemplate)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

}

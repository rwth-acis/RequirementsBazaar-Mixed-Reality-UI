using Org.Requirements_Bazaar.API;
using Org.Requirements_Bazaar.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Org.Requirements_Bazaar.AR_VR_Forms
{

    public class RequirementsBazaarCategoriesForm : RequirementsBazaarForm
    {
        [SerializeField] private int projectId;
        [SerializeField] private RectTransform categoryDisplayTemplate;
        [SerializeField] private Button upButton;
        [SerializeField] private Button downButton;

        private int page = 0;
        private const int categoriesPerPage = 4;
        private Category[] currentPage, nextPage;
        private bool instantiatedByCode = false;

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

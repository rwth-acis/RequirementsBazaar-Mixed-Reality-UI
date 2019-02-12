using Org.Requirements_Bazaar.Common;
using Org.Requirements_Bazaar.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Org.Requirements_Bazaar.AR_VR_Forms
{

    public class CategoryDisplayElement : MonoBehaviour
    {
        [SerializeField] private Text titleLabel;
        [SerializeField] private Text descriptionLabel;

        private Category category;

        private Transform canvas;

        public Category Category
        {
            get { return category; }
            set
            {
                category = value;
                UpdateDisplay();
            }
        }

        private void Start()
        {
            canvas = Utilities.GetHighestParent(transform);
        }

        private void UpdateDisplay()
        {
            titleLabel.text = category.name;
            descriptionLabel.text = category.description;
        }

        public void OnClick()
        {
            // instantiate the next subpage: the requirements in a category page
            GameObject requirementsPage = Instantiate(RequirementsBazaarUI.Instance.RequirementsPage);
            requirementsPage.transform.position = canvas.position;
            RequirementsBazaarRequirementsInCategoryForm requirementsForm = requirementsPage.GetComponent<RequirementsBazaarRequirementsInCategoryForm>();
            requirementsForm.CategoryId = Category.id;
            requirementsForm.PageClosed += OnInstantiatedPageClosed;

            canvas.gameObject.SetActive(false);
        }

        private void OnInstantiatedPageClosed(object sender, EventArgs e)
        {
            canvas.gameObject.SetActive(true);
        }
    }

}

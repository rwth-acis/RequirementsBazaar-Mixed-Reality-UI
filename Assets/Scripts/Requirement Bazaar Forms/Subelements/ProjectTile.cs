using Org.Requirements_Bazaar.DataModel;
using Org.Requirements_Bazaar.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Org.Requirements_Bazaar.AR_VR_Forms
{

    public class ProjectTile : MonoBehaviour
    {
        [SerializeField] Text titleLabel;
        [SerializeField] Text descriptionLabel;
        private Project project;

        private Transform canvas;

        public Project Project
        {
            get { return project; }
            set
            {
                project = value;
                UpdateDisplay();
            }
        }

        private void Start()
        {
            canvas = Utilities.GetHighestParent(transform);
        }

        private void UpdateDisplay()
        {
            titleLabel.text = project.name;
            descriptionLabel.text = project.description;
        }

        public void OnClick()
        {
            // instantiate the next subpage: the categories page
            GameObject categoriesPage = Instantiate(RequirementsBazaarUI.Instance.CategoriesPage);
            categoriesPage.transform.position = canvas.position;
            RequirementsBazaarCategoriesForm categoriesForm = categoriesPage.GetComponent<RequirementsBazaarCategoriesForm>();
            categoriesForm.ProjectId = Project.id;
            categoriesForm.PageClosed += OnInstantiatedPageClosed;

            canvas.gameObject.SetActive(false);
        }

        private void OnInstantiatedPageClosed(object sender, EventArgs e)
        {
            canvas.gameObject.SetActive(true);
        }
    }

}
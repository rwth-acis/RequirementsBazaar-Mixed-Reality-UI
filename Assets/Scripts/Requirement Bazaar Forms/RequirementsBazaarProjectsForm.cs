using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Org.Requirements_Bazaar.AR_VR_Forms
{

    public class RequirementsBazaarProjectsForm : RequirementsBazaarForm
    {
        [SerializeField] private RectTransform projectTileTemplate;
        [SerializeField] private Button upButton;
        [SerializeField] private Button downButton;

        private int page = 0;
        private const int projectsPerPage = 4;
        private Project[] currentPage, nextPage;

        private void Start()
        {
            UpdateDisplay();
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
            ClearProjectTiles();

            // get the current page and the next page
            // the next page is required to check if 
            currentPage = await RequirementsBazaar.GetProjects(page, projectsPerPage);
            nextPage = await RequirementsBazaar.GetProjects(page + 1, projectsPerPage);

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
                CreateProjectTile(currentPage[i]);
            }
        }

        private void CreateProjectTile(Project project)
        {
            RectTransform projectTileTransform = Instantiate(projectTileTemplate, projectTileTemplate.parent);
            projectTileTransform.name = "Project Tile (Project ID " + project + ")";
            projectTileTransform.gameObject.SetActive(true);
            ProjectTile projectTile = projectTileTransform.GetComponent<ProjectTile>();
            projectTile.Project = project;
        }

        private void ClearProjectTiles()
        {
            foreach (RectTransform child in projectTileTemplate.parent)
            {
                if (child != projectTileTemplate)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

}
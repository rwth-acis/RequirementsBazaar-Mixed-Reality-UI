using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequirementsBazaarRequirementsInCategoryForm : MonoBehaviour
{
    [SerializeField] private int categoryId;
    public int requirementsPerPage = 4;
    [SerializeField] private RectTransform requirementDisplayElementTemplate;
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;

    private int page = 0;
    private Requirement[] currentPage, nextPage;

    public int CategoryId
    {
        get { return categoryId; }
        set
        {
            categoryId = value;
            UpdateDisplay();
        }
    }

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
        ClearRequirementDisplayElements();

        // get the current page and the next page
        // the next page is required to check if 
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
        RectTransform projectTileTransform = Instantiate(requirementDisplayElementTemplate, requirementDisplayElementTemplate.parent);
        projectTileTransform.name = "Requirement (ID " + requirement.id + ")";
        projectTileTransform.gameObject.SetActive(true);
        RequirementDisplayElement projectTile = projectTileTransform.GetComponent<RequirementDisplayElement>();
        projectTile.Requirement = requirement;
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

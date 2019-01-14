using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RequirementsBazaarCreateEditRequirementForm : MonoBehaviour
{
    [SerializeField] private int projectId;
    [SerializeField] private Button submitButton;
    [SerializeField] private Dropdown categoryDropdown;
    [SerializeField] private InputField titleInputField;
    [SerializeField] private InputField descriptionInputField;
    [SerializeField] private Image attachmentImage;

    private int requirementId = -1;
    private Category[] availableCategories;

    public int RequirementId
    {
        get { return requirementId; }
        set
        {
            requirementId = value;
            if (requirementId > -1)
            {
                ShowExistingRequirement();
            }
        }
    }

    private async void ShowExistingRequirement()
    {
        if (requirementId < 0)
        {
            return;
        }

        await GetCategories();

        Requirement requirement = await RequirementsBazaar.GetRequirement(requirementId);

        titleInputField.text = requirement.name;
        descriptionInputField.text = requirement.description;
        if (availableCategories != null && requirement.categories.Length > 0)
        {
            for (int i = 0; i < availableCategories.Length; i++)
            {
                if (availableCategories[i].name == requirement.categories[0].name)
                {
                    categoryDropdown.value = i;
                    categoryDropdown.RefreshShownValue();
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("Categories have not been loaded yet but trying to set requirement category");
        }
    }

    private async Task GetCategories()
    {
        availableCategories = await RequirementsBazaar.GetProjectCategories(projectId, 0, 20);
        categoryDropdown.options = new List<Dropdown.OptionData>();
        Debug.Log(availableCategories);
        Debug.Log(availableCategories.Length);
        for (int i = 0; i < availableCategories.Length; i++)
        {
            categoryDropdown.options.Add(new Dropdown.OptionData(availableCategories[i].name));
        }
    }
}

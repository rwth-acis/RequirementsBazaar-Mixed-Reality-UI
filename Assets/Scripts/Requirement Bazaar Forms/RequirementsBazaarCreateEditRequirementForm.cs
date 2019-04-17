using Org.Requirements_Bazaar.API;
using Org.Requirements_Bazaar.DataModel;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Org.Requirements_Bazaar.AR_VR_Forms
{

    public class RequirementsBazaarCreateEditRequirementForm : RequirementsBazaarForm
    {
        [SerializeField] private int projectId = 0;
        [SerializeField] private int requirementId = -1;

        [SerializeField] private Button submitButton;
        [SerializeField] private Dropdown categoryDropdown;
        [SerializeField] private InputField titleInputField;
        [SerializeField] private InputField descriptionInputField;
        [SerializeField] private Image attachmentImage;

        private Category[] availableCategories;

        private Requirement requirement;

        private bool instantiatedByCode = false;

        private void Start()
        {
            if (!instantiatedByCode)
            {
                CheckRequirementId();
            }
        }

        public int RequirementId
        {
            get { return requirementId; }
            set
            {
                requirementId = value;
                instantiatedByCode = true;
                CheckRequirementId();
            }
        }

        public bool IsNewRequirement
        {
            get { return requirementId < 0; }
        }

        private void CheckRequirementId()
        {
            if (IsInCreateMode)
            {
                SetUpCreateMode();
            }
            else
            {
                ShowExistingRequirement();
            }
        }

        public bool IsInCreateMode
        {
            get
            {
                return requirementId < 0;
            }
        }

        public async void SetUpCreateMode()
        {
            requirementId = -1;
            await GetCategories();
        }

        public void SetUpEditMode(int requirementId)
        {
            this.requirementId = requirementId;
            ShowExistingRequirement();
        }

        private async void ShowExistingRequirement()
        {
            if (requirementId < 0)
            {
                return;
            }

            requirement = await RequirementsBazaar.GetRequirement(requirementId);
            projectId = requirement.ProjectId;

            await GetCategories();

            titleInputField.text = requirement.Name;
            descriptionInputField.text = requirement.Description;
            if (availableCategories != null && requirement.Categories.Length > 0)
            {
                for (int i = 0; i < availableCategories.Length; i++)
                {
                    if (availableCategories[i].Name == requirement.Categories[0].Name)
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
            for (int i = 0; i < availableCategories.Length; i++)
            {
                categoryDropdown.options.Add(new Dropdown.OptionData(availableCategories[i].Name));
            }
            categoryDropdown.RefreshShownValue();
        }

        public async void PostRequirement()
        {
            if (IsNewRequirement)
            {
                requirement = new Requirement();
            }
            requirement.Name = titleInputField.text;
            requirement.Description = descriptionInputField.text;
            requirement.ProjectId = projectId;
            requirement.Categories = new Category[] { availableCategories[categoryDropdown.value] };

            if (IsNewRequirement)
            {
                await RequirementsBazaar.CreateRequirement(requirement.ProjectId, requirement.Name, requirement.Description, requirement.Categories);
            }
            else
            {
                await RequirementsBazaar.UpdateRequirement(requirement);
            }

            Close();
        }
    }
}
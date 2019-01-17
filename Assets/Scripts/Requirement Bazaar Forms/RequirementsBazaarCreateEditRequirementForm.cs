using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RequirementsBazaarCreateEditRequirementForm : MonoBehaviour
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

    private void Awake()
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

    public int RequirementId
    {
        get { return requirementId; }
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
        projectId = requirement.projectId;

        await GetCategories();

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
        for (int i = 0; i < availableCategories.Length; i++)
        {
            categoryDropdown.options.Add(new Dropdown.OptionData(availableCategories[i].name));
        }
        categoryDropdown.RefreshShownValue();
    }
}

[CustomEditor(typeof(RequirementsBazaarCreateEditRequirementForm))]
[CanEditMultipleObjects]
public class CreateEditRequirementFormEditor : Editor
{
    int selectedMode = 0; // 0: new requirement, 1: existing requirement

    SerializedProperty projectId;
    SerializedProperty requirementIdProperty;
    int requirementId;
    SerializedProperty submitButton;
    SerializedProperty categoryDropdown;
    SerializedProperty titleInputField;
    SerializedProperty descriptionInputField;
    SerializedProperty attachmentImage;


    private void OnEnable()
    {
        projectId = serializedObject.FindProperty("projectId");
        requirementIdProperty = serializedObject.FindProperty("requirementId");
        submitButton = serializedObject.FindProperty("submitButton");
        categoryDropdown = serializedObject.FindProperty("categoryDropdown");
        titleInputField = serializedObject.FindProperty("titleInputField");
        descriptionInputField = serializedObject.FindProperty("descriptionInputField");
        attachmentImage = serializedObject.FindProperty("attachmentImage");

        requirementId = requirementIdProperty.intValue;

        if (requirementIdProperty.intValue != -1)
        {
            selectedMode = 1; // select existing mode
        }
        else
        {
            selectedMode = 0; // select new mode
        }
    }

    public override void OnInspectorGUI()
    {        
        selectedMode = GUILayout.SelectionGrid(selectedMode, new string[] { "Create New Requirement", "Edit Existing Requirement" }, 2);

        if (selectedMode == 0)
        {
            EditorGUILayout.PropertyField(projectId);
        }
        else
        {
            requirementId = EditorGUILayout.IntField("Requirement ID", requirementId);
        }

        EditorGUILayout.Separator();

        EditorGUILayout.PropertyField(submitButton);
        EditorGUILayout.PropertyField(categoryDropdown);
        EditorGUILayout.PropertyField(titleInputField);
        EditorGUILayout.PropertyField(descriptionInputField);
        EditorGUILayout.PropertyField(attachmentImage);


        if (selectedMode == 0)
        {
            requirementIdProperty.intValue = -1;
        }
        else
        {
            requirementIdProperty.intValue = requirementId;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
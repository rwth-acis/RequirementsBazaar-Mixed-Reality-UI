using Org.Requirements_Bazaar.AR_VR_Forms;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Org.Requirements_Bazaar.EditorUI
{

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
}

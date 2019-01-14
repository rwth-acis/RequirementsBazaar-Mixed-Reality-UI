using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectTile : MonoBehaviour
{
    [SerializeField] Text titleLabel;
    [SerializeField] Text descriptionLabel;
    private Project project;

    public Project Project
    {
        get { return project; }
        set
        {
            project = value;
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        titleLabel.text = project.name;
        descriptionLabel.text = project.description;
    }
}

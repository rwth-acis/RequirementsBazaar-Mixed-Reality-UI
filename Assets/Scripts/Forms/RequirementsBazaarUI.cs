using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementsBazaarUI : MonoBehaviour
{

    [SerializeField] private GameObject projectOverviewPage;
    [SerializeField] private GameObject categoriesPage;
    [SerializeField] private GameObject requirementsPage;
    [SerializeField] private GameObject createEditRequirementPage;

    public static RequirementsBazaarUI Instance
    {
        get; private set;
    }

    private void Start()
    {
        if (Instance != null)
        {
            Debug.LogWarning("There seem to be multiple instances of the RequirementsBazaarUI");
        }
        Instance = this;
    }

    public GameObject ProjectOverviewPage
    {
        get { return projectOverviewPage; }
    }

    public GameObject CategoriesPage
    {
        get { return categoriesPage; }
    }

    public GameObject RequirementsPage
    {
        get { return requirementsPage; }
    }

    public GameObject CreateEditRequirementPage
    {
        get { return createEditRequirementPage; }
    }
}

﻿using Org.Requirements_Bazaar.DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Org.Requirements_Bazaar.AR_VR_Forms
{

    public class RequirementDisplayElement : MonoBehaviour
    {
        [SerializeField] private Text titleLabel;
        [SerializeField] private Text descriptionLabel;
        [SerializeField] private Text votesLabel;

        private Requirement requirement;

        public Requirement Requirement
        {
            get { return requirement; }
            set
            {
                requirement = value;
                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            titleLabel.text = requirement.name;
            descriptionLabel.text = requirement.description;
            votesLabel.text = requirement.upVotes.ToString();
        }
    }

}
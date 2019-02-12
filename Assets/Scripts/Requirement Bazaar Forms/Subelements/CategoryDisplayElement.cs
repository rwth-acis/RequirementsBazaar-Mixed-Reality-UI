using Org.Requirements_Bazaar.DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Org.Requirements_Bazaar.AR_VR_Forms
{

    public class CategoryDisplayElement : MonoBehaviour
    {
        [SerializeField] private Text titleLabel;
        [SerializeField] private Text descriptionLabel;

        private Category category;

        public Category Category
        {
            get { return category; }
            set
            {
                category = value;
                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            titleLabel.text = category.name;
            descriptionLabel.text = category.description;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textBoxInteractObject : MonoBehaviour
{
    public GameObject objectClicked;
    //public GameObject textBox;
    public GameObject infoText;
    public BuildingInfo info;
    string displayText;
    string displayTextName;
    string displayTextMoney;
    string displayTextEnergy;
    string displayTextPollution;
    string displayTextDescription;
    string displayTextLevel;
    string objectLevel;
    float displayTextTTC;
    int displayTextCurrentTTC;

    void Start()
    {
        //info = objectClicked.GetComponent<BuildingInfo>();
        infoText = transform.GetChild(0).gameObject;
    }
    void Update()
    {
        if (objectClicked != null)
        {
            info = objectClicked.GetComponent<BuildingInfo>();
            displayTextName = info.displayName;
            displayTextCurrentTTC = Mathf.RoundToInt(info.currentTTC);
            displayTextTTC = info.timeToCreate;
        }
        if (gameObject.activeSelf && info != null)
        {
            switch (info.moneyCreates)
            {
                case (> 0):
                    displayTextMoney = ("Produces $" + info.moneyCreates + " every activation.");
                    break;
                case (< 0):
                    displayTextMoney = ("Costs $" + Mathf.Abs(info.moneyCreates) + " every activation.");
                    break;
                case (0):
                    displayTextMoney = null;
                    break;
            }
            switch (info.energyCreates)
            {
                case (> 0):
                    displayTextEnergy = ("Produces " + info.energyCreates + " units of energy every activation.");
                    break;
                case (< 0):
                    displayTextEnergy = ("Consumes " + Mathf.Abs(info.energyCreates) + " units of energy every activation.");
                    break;
                case (0):
                    displayTextEnergy = null;
                    break;
            }
            switch (info.pollutionCreates)
            {
                case (> 0):
                    displayTextPollution = ("Creates " + info.pollutionCreates + " units of waste every activation.\n");
                    break;
                case (< 0):
                    displayTextPollution = ("Cleans " + Mathf.Abs(info.pollutionCreates) + " units of waste every activation.\n");
                    break;
                case (0):
                    displayTextPollution = null;
                    break;
            }
            displayTextDescription = info.description;
            objectLevel = info.level.ToString();
            displayTextLevel = info.level.ToString();
        }
        displayText = ("<b>" + displayTextName + "</b>" + "\n\n" + displayTextMoney + "\n" + displayTextEnergy + "\n" + displayTextPollution + "\nActivates every " + displayTextTTC + " seconds." 
            + "\nActivating in " + displayTextCurrentTTC + " seconds." + "\n" + displayTextDescription + "\n" + "Level: " + displayTextLevel);
        /*if (displayTextMoney.Equals(0) && displayTextEnergy.Equals(0) && displayTextPollution.Equals(0) && displayTextCurrentTTC == 0)
        {
            displayText = displayTextDescription;
        }*/
        infoText.GetComponent<TextMeshProUGUI>().text = displayText;
    }
}

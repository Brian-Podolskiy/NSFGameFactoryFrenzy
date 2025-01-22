using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

//This script is for interaction with non-text box objects, mainly to create a text box.
//More tags can be used to do stuff other than create a text box.
//Make sure the object this script is on has a collider and that the gameobject for "textBox" is set correctly
public class InteractObject : MonoBehaviour
{
    public GameObject textBox;
    public GameObject infoText;
    public GameObject gameManager;
    public BuildingInfo info;
    public GameObject Fabricator;
    public GameObject SuperFabricator;
    public GameObject CoalGenerator;
    public GameObject NuclearGenerator;
    public GameObject PollutionCleaner;
    public GameObject SuperPollutionCleaner;
    public GameObject SuperFabButton;
    public GameObject SuperPolCleanButton;
    public GameObject NuclearButton;
    public int fabLvl;
    public int coalLvl;
    public int superFabLvl;
    public int nuclearGenLvl;
    public int cleanLvl;
    public int superCleanLvl;
    public bool researchAutomation;
    public bool researchStormProtection;
    public bool fabProUnlock = false;
    public bool cleanProUnlock = false;
    public bool nuclearUnlock = false;
    // Start is called before the first frame update
    void Start()
    {
        info = transform.GetComponent<BuildingInfo>();
        textBox = GameObject.Find("textBox");
        infoText = textBox.transform.GetChild(0).gameObject;
        gameManager = GameObject.Find("gameManager");
        Fabricator.GetComponent<BuildingInfo>().moneyCreates = 50;
        Fabricator.GetComponent<BuildingInfo>().level = 0;
        Fabricator.GetComponent<BuildingInfo>().pollutionCreates = 1.75f;
        Fabricator.GetComponent<BuildingInfo>().energyCreates = -4;
        Fabricator.GetComponent<BuildingInfo>().timeToCreate = 5;

        SuperFabricator.GetComponent<BuildingInfo>().moneyCreates = 120;
        SuperFabricator.GetComponent<BuildingInfo>().level = 0;
        SuperFabricator.GetComponent<BuildingInfo>().pollutionCreates = 6f;
        SuperFabricator.GetComponent<BuildingInfo>().energyCreates = -7;
        SuperFabricator.GetComponent<BuildingInfo>().timeToCreate = 5;

        CoalGenerator.GetComponent<BuildingInfo>().moneyCreates = -20;
        CoalGenerator.GetComponent<BuildingInfo>().level = 0;
        CoalGenerator.GetComponent<BuildingInfo>().pollutionCreates = 5;
        CoalGenerator.GetComponent<BuildingInfo>().energyCreates = 10;
        CoalGenerator.GetComponent<BuildingInfo>().timeToCreate = 5;

        NuclearGenerator.GetComponent<BuildingInfo>().moneyCreates = -50;
        NuclearGenerator.GetComponent<BuildingInfo>().level = 0;
        NuclearGenerator.GetComponent<BuildingInfo>().pollutionCreates = 3;
        NuclearGenerator.GetComponent<BuildingInfo>().energyCreates = 25;
        NuclearGenerator.GetComponent<BuildingInfo>().timeToCreate = 0.75f;

        PollutionCleaner.GetComponent<BuildingInfo>().moneyCreates = -20;
        PollutionCleaner.GetComponent<BuildingInfo>().level = 0;
        PollutionCleaner.GetComponent<BuildingInfo>().pollutionCreates = -4;
        PollutionCleaner.GetComponent<BuildingInfo>().energyCreates = -5;
        PollutionCleaner.GetComponent<BuildingInfo>().timeToCreate = 10;

        SuperPollutionCleaner.GetComponent<BuildingInfo>().moneyCreates = -80;
        SuperPollutionCleaner.GetComponent<BuildingInfo>().level = 0;
        SuperPollutionCleaner.GetComponent<BuildingInfo>().pollutionCreates = -20;
        SuperPollutionCleaner.GetComponent<BuildingInfo>().energyCreates = -15;
        SuperPollutionCleaner.GetComponent<BuildingInfo>().timeToCreate = 5;

        fabLvl = Fabricator.GetComponent<BuildingInfo>().level;
        coalLvl = CoalGenerator.GetComponent<BuildingInfo>().level;
        superFabLvl = SuperFabricator.GetComponent<BuildingInfo>().level;
        nuclearGenLvl = NuclearGenerator.GetComponent<BuildingInfo>().level;
        cleanLvl = PollutionCleaner.GetComponent<BuildingInfo>().level;
        superCleanLvl = SuperPollutionCleaner.GetComponent<BuildingInfo>().level;

        researchAutomation = false;
        researchStormProtection = false;
    }

    void Update()
    {

    }
    public void OnMouseDown()
    {
        Debug.Log(gameObject);
        if (gameObject.CompareTag("Background"))
        {
            //turns textbox off
            textBox.GetComponent<SpriteRenderer>().enabled = false;
            infoText.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        else if (gameObject.CompareTag("InteractBuildings"))
        {
            // TODO: make it so selecting another object in build mode menu doesn't build another one of the currently selected buildings
            switch (name)
            {
                case ("CoalT"):
                    gameManager.GetComponent<GameManagerScript>().doNotCreate = true;
                    gameManager.GetComponent<GameManagerScript>().selection = 2;
                    Debug.Log("selected 2");
                    break;
                case ("FabT"):
                    gameManager.GetComponent<GameManagerScript>().doNotCreate = true;
                    gameManager.GetComponent<GameManagerScript>().selection = 1;
                    Debug.Log("selected 1");
                    break;
                case ("PolCT"):
                    gameManager.GetComponent<GameManagerScript>().doNotCreate = true;
                    gameManager.GetComponent<GameManagerScript>().selection = 3;
                    Debug.Log("selected 3");
                    break;
                case ("SFabT"):
                    gameManager.GetComponent<GameManagerScript>().doNotCreate = true;
                    gameManager.GetComponent<GameManagerScript>().selection = 4;
                    Debug.Log("selected 4");
                    break;
                case ("SPolCT"):
                    gameManager.GetComponent<GameManagerScript>().doNotCreate = true;
                    gameManager.GetComponent<GameManagerScript>().selection = 5;
                    Debug.Log("selected 5");
                    break;
                case ("NukeT"):
                    gameManager.GetComponent<GameManagerScript>().doNotCreate = true;
                    gameManager.GetComponent<GameManagerScript>().selection = 6;
                    Debug.Log("selected 6");
                    break;
            }
        }
        else if (gameObject.CompareTag("BuildMode"))
        {
            gameManager.GetComponent<GameManagerScript>().buildModeActive = !(gameManager.GetComponent<GameManagerScript>().buildModeActive);
        }
        else if (gameObject.CompareTag("UpgradeButton"))
        {
            switch (name)
            {
                case ("FabU"):
                    Debug.Log("fab upgraded");
                    gameManager.GetComponent<GameManagerScript>().UpgradeFabs();
                    if (fabLvl < 3)
                    {
                        Debug.Log(fabLvl);
                    }
                    break;

                case ("CoalU"):
                    Debug.Log("coal upgraded");
                    gameManager.GetComponent<GameManagerScript>().UpgradeCoals();
                    if (coalLvl < 3)
                    {
                        Debug.Log(coalLvl);
                    }
                    break;

                case ("CleanU"):
                    Debug.Log("clean upgraded");
                    gameManager.GetComponent<GameManagerScript>().UpgradeClean();
                    if (cleanLvl < 3)
                    {
                        Debug.Log(cleanLvl);
                    }
                    break;

                case ("SuperFabU"):
                    Debug.Log("super fab upgraded");
                    gameManager.GetComponent<GameManagerScript>().UpgradeFabsPro();
                    if (superFabLvl < 3)
                    {
                        Debug.Log(superFabLvl);
                    }
                    break;

                case ("SuperCleanU"):
                    Debug.Log("super clean upgraded");
                    gameManager.GetComponent<GameManagerScript>().UpgradeCleanPro();
                    if (superCleanLvl < 3)
                    {
                        Debug.Log(superCleanLvl);
                    }
                    break;
                case ("NuclearU"):
                    Debug.Log("nuclear upgraded");
                    gameManager.GetComponent<GameManagerScript>().UpgradeNuclear();
                    if (nuclearGenLvl < 3)
                    {
                        Debug.Log(nuclearGenLvl);
                    }
                    break;

                case ("Upgrade Efficency"):
                    if (researchAutomation == false && (gameManager.GetComponent<GameManagerScript>().money >= 1500))
                    {
                        gameManager.GetComponent<GameManagerScript>().UpgradeTTC();
                        gameManager.GetComponent<GameManagerScript>().MoneyManager(-1500);
                        gameObject.GetComponent<TextMeshProUGUI>().text = "Automate Production" + "\n" + "\n" + "(Obtained!)";
                        researchAutomation = true;

                        //checks if it is already on
                        if (textBox.GetComponent<SpriteRenderer>().enabled == false)
                        {
                            //turns it on if it isn't
                            textBox.GetComponent<SpriteRenderer>().enabled = true;
                            infoText.GetComponent<TextMeshProUGUI>().enabled = true;
                        }
                        //moves to position, slightly up and to the side of the object
                        //automatically scales
                        textBox.transform.localScale = new Vector2(8, 5);
                        textBox.transform.position = new Vector2(transform.position.x + textBox.transform.localScale.x/2 + transform.localScale.x/2, 
                        transform.position.y + textBox.transform.localScale.y / 2 + transform.localScale.y / 2);
                        textBox.GetComponent<textBoxInteractObject>().objectClicked = gameObject;
                    }
                    break;

                case ("Storm Protection"):
                    if (researchStormProtection == false && (gameManager.GetComponent<GameManagerScript>().money >= 2000)) 
                    {
                        gameManager.GetComponent<GameManagerScript>().DisasterPrevention();
                        gameManager.GetComponent<GameManagerScript>().MoneyManager(-2000);
                        gameObject.GetComponent<TextMeshProUGUI>().text = "Storm Protection" + "\n" + "\n" + "(Obtained!)";
                        researchStormProtection = true;
                    }
                    break;

                case ("UnlockSuperFab"):
                    if (fabProUnlock == false && (gameManager.GetComponent<GameManagerScript>().money >= 2000))
                    {
                        gameManager.GetComponent<GameManagerScript>().UnlockBuilding("fabPro");
                        gameManager.GetComponent<GameManagerScript>().MoneyManager(-2000);
                        gameObject.GetComponent<TextMeshProUGUI>().text = "Unlock Super Fabricator" + "\n" + "(Obtained!)";
                        SuperFabButton.GetComponent<TextMeshProUGUI>().text = "Super Fabricator";
                        fabProUnlock = true;
                    }
                    break;

                case ("UnlockSuperClean"):
                    if (cleanProUnlock == false && (gameManager.GetComponent<GameManagerScript>().money >= 2000))
                    {
                        gameManager.GetComponent<GameManagerScript>().UnlockBuilding("cleanPro");
                        gameManager.GetComponent<GameManagerScript>().MoneyManager(-2000);
                        gameObject.GetComponent<TextMeshProUGUI>().text = "Unlock Super Pollution Cleaner" + "\n" + "(Obtained!)";
                        SuperPolCleanButton.GetComponent<TextMeshProUGUI>().text = "Super Pollution Cleaner";
                        cleanProUnlock = true;
                    }
                    break;

                case ("UnlockNuclear"):
                    if (nuclearUnlock == false && (gameManager.GetComponent<GameManagerScript>().money >= 2000))
                    {
                        gameManager.GetComponent<GameManagerScript>().UnlockBuilding("nuclear");
                        gameManager.GetComponent<GameManagerScript>().MoneyManager(-2000);
                        gameObject.GetComponent<TextMeshProUGUI>().text = "Unlock Nuclear Generator" + "\n" + "(Obtained!)";
                        NuclearButton.GetComponent<TextMeshProUGUI>().text = "Nuclear Generator";
                        nuclearUnlock = true;
                    }
                    break;
            }
        }
        else
        {
            //checks if it is already on
            if (textBox.GetComponent<SpriteRenderer>().enabled == false)
            {
                //turns it on if it isn't
                textBox.GetComponent<SpriteRenderer>().enabled = true;
                infoText.GetComponent<TextMeshProUGUI>().enabled = true;
                //infoText.SetActive(true);
            }
            //moves to position, slightly up and to the side of the object
            //automatically scales
            textBox.transform.localScale = new Vector2(8, 5);
            textBox.transform.position = new Vector2(transform.position.x + textBox.transform.localScale.x/2 + transform.localScale.x/2, 
                transform.position.y + textBox.transform.localScale.y / 2 + transform.localScale.y / 2);
            textBox.GetComponent<textBoxInteractObject>().objectClicked = gameObject;
            //textBox.transform.localScale = new Vector2(4, 5);
            //infoText.GetComponent<TextMeshProUGUI>().text = ("Produces $" + info.moneyCreates + " every " + info.timeToCreate + " seconds" + "\n Time to Create: " + info.currentTTC);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// we will use this script to control variables like the money, energy, pollution, etc
// we have multiple "manager" methods here we use to change variables, we call these every ~10-20s or so
// stuff is added here based on the number of "objects" we have to change variables (example, for every energy-producing unit, we add more energy based on that)

public class GameManagerScript : MonoBehaviour
{
    // WE NEED TO FIX THE BALANCING - HOW MUCH MONEY/ENERGY/POLLUTION STUFF GIVES
    [SerializeField] public long money;
    [SerializeField] public float energy;
    [SerializeField] public int employees;
    [SerializeField] public float pollution;
    [SerializeField] public int products;
    [SerializeField] public float totalTime;
    [SerializeField] public float currentTime = 120;
    [SerializeField] public TMP_Text moneyText;
    [SerializeField] public TMP_Text isBuilding;
    [SerializeField] public TMP_Text pollutionText;
    [SerializeField] public TMP_Text energyText;
    [SerializeField] public TMP_Text daysText;
    [SerializeField] public TMP_Text isPausedText;
    [SerializeField] public GameObject menu;
    [SerializeField] public GameObject rMenu;
    [SerializeField] public TMP_Text currentEventText;

    public GameObject gameOverBg;
    public GameObject hud;
    public GameObject TestSquare;
    public GameObject Fabricator;
    public GameObject SuperFabricator;
    public GameObject CoalGenerator;
    public GameObject NuclearGenerator;
    public GameObject PollutionCleaner;
    public GameObject SuperPollutionCleaner;
    public GameObject Background;
    public GameObject SelectBuild;
    public GameObject InteractBuilding;
    public GameObject SFXPlaying;
    public ArrayList allFabs = new ArrayList();
    public ArrayList allCoals = new ArrayList();
    public ArrayList allClean = new ArrayList();
    public ArrayList allFabPro = new ArrayList();
    public ArrayList allCleanPro = new ArrayList();
    public ArrayList allNuclear = new ArrayList();
    public Image pollutionBar;
    public bool doNotCreate = false;
    public bool buildModeActive = false;
    public bool deleteModeActive = false;
    public bool isPaused = false;
    public bool isAppear = false;
    public bool isAppearR = false;
    public bool fabProUnlock = false;
    public bool cleanProUnlock = false;
    public bool nuclearUnlock = false;
    public int moneyModifier = 1;
    bool isDisaster = false;
    bool isOutage = false;
    bool skipNext = true;
    private Camera _Camera;
    public int selection = 0;
    Vector3 MousePos;
    private float gameOverTime = 5;
    public int safeRange = 75;
    public int outageRange = 85;
    public int disasterRange = 100;

    void Start()
    {
        Vector3 MousePos = Input.mousePosition;

        _Camera = Camera.main;
        Background = GameObject.Find("Background");
        SelectBuild = GameObject.Find("BuildMode");
        InteractBuilding = GameObject.Find("InteractBuildings");
        isPausedText.enabled = isPaused;


        moneyText.text = "Money: $" + money.ToString();
        if (buildModeActive)
        {
            isBuilding.text = "Build Mode: On (A to exit)";
        }
        else
        {
            isBuilding.text = "Build Mode: Off";
        }
    }

    public void MoneyManager(long m) 
    {
       m *= moneyModifier;
       money += m;
       moneyText.text = "Money: $" + money.ToString();
    }

    public void gameOver()
    {
        hud.transform.GetChild(7).GetComponent<TextMeshProUGUI>().enabled = true;
        gameOverBg.SetActive(true);
    }

    public void EnergyManager(float en) 
    {
        //Debug.Log("energy =" + en);
        if (isOutage && en <= 0)
        {
            en = (float) (en * 1.5);
            Debug.Log("en * 1.5");
        }

        energy += en;
        energyText.text = "Power: " + energy.ToString();
    }

    public void PollutionManager(float p)
    {
        if (isDisaster && p >= 0)
        {
            p = (float) (p * 1.5);
            Debug.Log("p * 1.5");
        }
        pollution += p;

        if (pollution < 0)
        {
            pollution += (pollution * -1);
        }

        pollutionBar.fillAmount = pollution / 100f;

        pollutionText.text = "Pollution Level: " + pollution.ToString() + "%";
        //Debug.Log("pollution =" + p);
    }

    public int RandomEventSystem()
    {
        // default safeRange = 0-75, outageRange = 76-85, disasterRange = 81-100
        int randomEvent = Random.Range(0, 100);
        if (randomEvent <= safeRange)
        {
            Debug.Log(randomEvent);
            return 0;
        }
        else if (safeRange < randomEvent && randomEvent <= outageRange)
        {
            Debug.Log(randomEvent);
            return 1;
        }
        else
        {
            Debug.Log(randomEvent);
            return 2;
        }
    }

    public void DisasterPrevention()
    {
        // new values: safeRange = 0-85, outageRange = 86-90, disasterRange = 91-100 
        safeRange = 85;
        outageRange = 90;
    }

    public void UnlockBuilding(string building)
    {
        switch (building)
        {
            case ("fabPro"):
                fabProUnlock = true;
                break;
            case ("cleanPro"):
                cleanProUnlock = true;
                break;
            case ("nuclear"):
                nuclearUnlock = true;
                break;
        }
    }

    public void UpgradeFabs()
    {
        if (allFabs.Count >= 1)
        {
            foreach (GameObject Fab in allFabs)
                    {
                        //Debug.Log("This is a fab");
                        Debug.Log(Fab);
                        Fab.GetComponent<BuildingInfo>().Upgrade(25, -0.25f, 1, 100);
                    }
        }
        
    }

    public void UpgradeCoals()
    {
        if (allCoals.Count >= 1)
        {
            foreach (GameObject Coal in allCoals)
                    {
                        //Debug.Log("This is a fab");
                        Debug.Log(Coal);
                        Coal.GetComponent<BuildingInfo>().Upgrade(5, -1f, 2, 150);
                    }
        }
        
    }

    public void UpgradeClean()
    {
        if (allClean.Count >= 1)
        {
            foreach (GameObject Cln in allClean)
                    {
                       // Debug.Log("This is a fab");
                        Debug.Log(Cln);
                        Cln.GetComponent<BuildingInfo>().Upgrade(5, -1.5f, 1, 200);
                    }
        }
        
    }

    public void UpgradeFabsPro()
    {
        if (allFabPro.Count >= 1)
        {
            foreach (GameObject FabP in allFabPro)
                    {
                        //Debug.Log("This is a fab");
                        Debug.Log(FabP);
                        FabP.GetComponent<BuildingInfo>().Upgrade(40, -2f, 2, 250);
                    }
        }
        
    }

    public void UpgradeCleanPro()
    {
        if (allCleanPro.Count >= 1)
        {
            foreach (GameObject ClnP in allCleanPro)
                    {
                        //Debug.Log("This is a fab");
                        Debug.Log(ClnP);
                        ClnP.GetComponent<BuildingInfo>().Upgrade(15, -5f, 3, 500);
                    }
        }
        
    }

    public void UpgradeNuclear()
    {
        if (allNuclear.Count >= 1)
        {
            foreach (GameObject Nuke in allNuclear)
                    {
                        Debug.Log("This is a fab");
                        Debug.Log(Nuke);
                        Nuke.GetComponent<BuildingInfo>().Upgrade(10, 0f, 25, 600);
                    }
        }
        
    }

    public void UpgradeTTC()
    {
        foreach (GameObject Fab in allFabs)
        {
            Fab.GetComponent<BuildingInfo>().changeTimeToCreate(2);
        }

        foreach (GameObject FabP in allFabPro)
        {
            FabP.GetComponent<BuildingInfo>().changeTimeToCreate(2);
        }

        foreach (GameObject Coal in allCoals)
        {
            Coal.GetComponent<BuildingInfo>().changeTimeToCreate(2);
        }

        foreach (GameObject Nuclear in allNuclear)
        {
            Nuclear.GetComponent<BuildingInfo>().changeTimeToCreate(2);
        }

        foreach (GameObject Poll in allClean)
        {
            Poll.GetComponent<BuildingInfo>().changeTimeToCreate(2);
        }

        foreach (GameObject PollP in allCleanPro)
        {
            PollP.GetComponent<BuildingInfo>().changeTimeToCreate(2);
        }
    }

    public void BuildMode(bool active)
    {
        var input = Input.inputString;
        if (active)
        {

            switch (input)
            {
                case ("a"):
                    buildModeActive = !buildModeActive;
                    selection = 0;
                    break;

                case ("1"):
                    //Debug.Log(input);
                    selection = 1;
                    break;

                case ("2"):
                    //Debug.Log("2");
                    selection = 2;
                    break;

                case ("3"):
                    selection = 3;
                    break;

                case ("4"):
                    selection = 4;
                    break;

                case ("5"):
                    selection = 5;
                    break;

                case ("6"):
                    selection = 6;
                    break;

                case ("7"):
                    selection = 7;
                    break;
            }

            if (Input.GetMouseButtonDown(0) && buildModeActive/* && CompareTag("Background")*/)
            {
                switch (selection)
                {
                    case (1):

                        if ((money - Fabricator.GetComponent<BuildingInfo>().cost) < 0 || doNotCreate)
                        {
                            Debug.Log("Not enough $ or clicking Table");
                            doNotCreate = false;
                            Debug.Log(doNotCreate);
                        }
                        else
                        {
                            //Debug.Log("Clicked");
                            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            MousePos.z = 0f;
                            transform.position = MousePos;
                            //Debug.Log("Mouse Position: (" + MousePos.x + ", " + MousePos.y + ", " + MousePos.z + ")");
                            //Instantiate(Fabricator, transform.position, transform.rotation);
                            allFabs.Add(Instantiate(Fabricator, transform.position, transform.rotation));
                            MoneyManager(-Fabricator.GetComponent<BuildingInfo>().cost);
                            SFXPlaying.GetComponent<SFXPlaying>().PlaySoundBuild();
                        }
                        break;

                    case (2):
                        if ((money - CoalGenerator.GetComponent<BuildingInfo>().cost) < 0 || doNotCreate)
                        {
                            Debug.Log("Not enough $");
                            doNotCreate = false;
                            Debug.Log(doNotCreate);
                        }
                        else
                        {
                            //Debug.Log("Clicked2");
                            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            MousePos.z = 0f;
                            transform.position = MousePos;
                            //Debug.Log("Mouse Position: (" + MousePos.x + ", " + MousePos.y + ", " + MousePos.z + ")");
                            //Instantiate(CoalGenerator, transform.position, transform.rotation);
                            allCoals.Add(Instantiate(CoalGenerator, transform.position, transform.rotation));
                            MoneyManager(-CoalGenerator.GetComponent<BuildingInfo>().cost);
                            SFXPlaying.GetComponent<SFXPlaying>().PlaySoundBuild();
                        }
                        break;

                    case (3):
                        if ((money - PollutionCleaner.GetComponent<BuildingInfo>().cost) < 0 || doNotCreate)
                        {
                            Debug.Log("Not enough $");
                            doNotCreate = false;
                            Debug.Log(doNotCreate);
                        }
                        else
                        {
                            //Debug.Log("Clicked3");
                            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            MousePos.z = 0f;
                            transform.position = MousePos;
                            //Debug.Log("Mouse Position: (" + MousePos.x + ", " + MousePos.y + ", " + MousePos.z + ")");
                            //Instantiate(PollutionCleaner, transform.position, transform.rotation);
                            allClean.Add(Instantiate(PollutionCleaner, transform.position, transform.rotation));
                            MoneyManager(-PollutionCleaner.GetComponent<BuildingInfo>().cost);
                            SFXPlaying.GetComponent<SFXPlaying>().PlaySoundBuild();
                        }
                        break;

                    case (4):
                        if ((money - SuperFabricator.GetComponent<BuildingInfo>().cost) < 0 || doNotCreate || fabProUnlock == false)
                        {
                            Debug.Log("Not enough $");
                            doNotCreate = false;
                            Debug.Log(doNotCreate);
                        }
                        else
                        {
                            //Debug.Log("Clicked4");
                            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            MousePos.z = 0f;
                            transform.position = MousePos;
                            //Debug.Log("Mouse Position: (" + MousePos.x + ", " + MousePos.y + ", " + MousePos.z + ")");
                            //Instantiate(SuperFabricator, transform.position, transform.rotation);
                            allFabPro.Add(Instantiate(SuperFabricator, transform.position, transform.rotation));
                            MoneyManager(-SuperFabricator.GetComponent<BuildingInfo>().cost);
                            SFXPlaying.GetComponent<SFXPlaying>().PlaySoundBuild();
                        }
                        break;

                    case (6):
                        if ((money - NuclearGenerator.GetComponent<BuildingInfo>().cost) < 0 || doNotCreate || nuclearUnlock == false)
                        {
                            Debug.Log("Not enough $");
                            doNotCreate = false;
                            Debug.Log(doNotCreate);
                        }
                        else
                        {
                            //Debug.Log("Clicked5");
                            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            MousePos.z = 0f;
                            transform.position = MousePos;
                            //Debug.Log("Mouse Position: (" + MousePos.x + ", " + MousePos.y + ", " + MousePos.z + ")");
                            //Instantiate(NuclearGenerator, transform.position, transform.rotation);
                            allNuclear.Add(Instantiate(NuclearGenerator, transform.position, transform.rotation));
                            MoneyManager(-NuclearGenerator.GetComponent<BuildingInfo>().cost);
                            SFXPlaying.GetComponent<SFXPlaying>().PlaySoundBuild();
                        }
                        break;

                    case (5):
                        if ((money - SuperPollutionCleaner.GetComponent<BuildingInfo>().cost) < 0 || doNotCreate || cleanProUnlock == false)
                        {
                            Debug.Log("Not enough $");
                            doNotCreate = false;
                            Debug.Log(doNotCreate);
                        }
                        else
                        {
                            //Debug.Log("Clicked6");
                            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            MousePos.z = 0f;
                            transform.position = MousePos;
                            //Debug.Log("Mouse Position: (" + MousePos.x + ", " + MousePos.y + ", " + MousePos.z + ")");
                            //Instantiate(SuperPollutionCleaner, transform.position, transform.rotation);
                            allCleanPro.Add(Instantiate(SuperPollutionCleaner, transform.position, transform.rotation));
                            MoneyManager(-SuperPollutionCleaner.GetComponent<BuildingInfo>().cost);
                            SFXPlaying.GetComponent<SFXPlaying>().PlaySoundBuild();
                        }
                        break;

                    case (7):
                        //Debug.Log("Clicked7");
                        break;
                }
            }
        }
    }

    void Update()
    {
        Vector3 MousePos = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            //isPausedText.text = isPaused.ToString();
            isPausedText.enabled = isPaused;
        }

        if (isPaused == false)
        {
            if (currentTime > 0)
            {
                currentTime -= 1 * Time.deltaTime;
            }

            else
            {
                totalTime += 1;
                currentTime = 120;
                daysText.text = ("Days: " + totalTime);
                if (skipNext == false)
                {
                    int doEvent = RandomEventSystem();
                    switch (doEvent)
                    {
                        case (0):
                            Debug.Log("nothing");
                            break;
                        case (1):
                            Debug.Log("outage");
                            currentEventText.text = "Current Event: Power Outage";
                            isOutage = true;
                            skipNext = true;
                            break;
                        case (2):
                            Debug.Log("Disaster");
                            currentEventText.text = "Current Event: Natural Disaster";
                            isDisaster = true;
                            skipNext = true;
                            break;
                    }
                }
                else
                {
                    currentEventText.text = "Current Event: None!";
                    isOutage = false;
                    isDisaster = false;
                    skipNext = false;
                }
                
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                buildModeActive = !buildModeActive;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                deleteModeActive = !deleteModeActive;
            }

            switch (buildModeActive)
            {
                case (true):
                    isBuilding.text = "Build Mode On";
                    break;
                case (false):
                    isBuilding.text = "Build Mode Off";
                    break;
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                Debug.Log("Pressed U");
                isAppear = !isAppear;
            }

            if (Input.GetKeyDown(KeyCode.R)) 
            {
                Debug.Log("Pressed R");
                isAppearR = !isAppearR;
            }

            menu.SetActive(isAppear);
            rMenu.SetActive(isAppearR);

            BuildMode(buildModeActive);

            if (pollution >= 100 || (totalTime >= 31 && money < 1000000))
            {
                //Debug.Log("gameOver");
                gameOver();
                gameOverTime -= Time.deltaTime;
                Debug.Log(gameOverTime);
                if (gameOverTime <= 0)
                {
                    SceneManager.LoadScene(sceneName: "TitleScreen");
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfo : MonoBehaviour
{
    public GameObject gameManager;
    public string displayName;
    [SerializeField] public long moneyCreates; //Currently supports 3 products
    [SerializeField] public int energyCreates; //Products can be positive or negative if it costs something to produce
    [SerializeField] public float pollutionCreates; //More can be added in, but I don't think we need it
    [SerializeField] public int cost;
    [SerializeField] public int level = 0;
    [SerializeField] public string description;
    public float timeToCreate;
    public float currentTTC; //ttc is time to create
    public bool wasClicked;
    public bool isPaused = false;

    void Start()
    {
        gameManager = GameObject.Find("gameManager");
        Debug.Log("Start called");
    }

    public void Upgrade(int c, float p, int e, int m)
    {
        Debug.Log("Cost to build: " + cost + "\n" + "pollution: " + pollutionCreates + "\n" + "energy: " + energyCreates + "\n" + "level: " + level);

        if (level <= 3 && (gameManager.GetComponent<GameManagerScript>().money >= m))
        {
            switch (level)
            {
                case (0):
                    Debug.Log("upgraded");
                    level++;
                    moneyCreates += c;
                    pollutionCreates += p;
                    energyCreates += e;
                    gameManager.GetComponent<GameManagerScript>().MoneyManager(-m);
                    gameManager.GetComponent<GameManagerScript>().SFXPlaying.GetComponent<SFXPlaying>().PlaySoundUpgrade();
                    break;
                case (1):
                    level++;
                    moneyCreates += c;
                    pollutionCreates += p;
                    energyCreates += e;
                    gameManager.GetComponent<GameManagerScript>().MoneyManager(-m);
                    gameManager.GetComponent<GameManagerScript>().SFXPlaying.GetComponent<SFXPlaying>().PlaySoundUpgrade();
                    break;
                case (2):
                    level++;
                    moneyCreates += c;
                    pollutionCreates += p;
                    energyCreates += e;
                    gameManager.GetComponent<GameManagerScript>().MoneyManager(-m);
                    gameManager.GetComponent<GameManagerScript>().SFXPlaying.GetComponent<SFXPlaying>().PlaySoundUpgrade();
                    break;
            }
        }
        else
        {
            Debug.Log("max lvl");
        }
    }

    public void changeTimeToCreate (float nTTC)
    {
        timeToCreate /= nTTC;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused == false)
        {
            if (currentTTC > 0)
            {
                currentTTC -= 1 * Time.deltaTime;
            }
            else
            {
                Debug.Log("Cost to build: " + cost + "\n" + "pollution: " + pollutionCreates + "\n" + "energy: " + energyCreates + "\n");
                currentTTC = timeToCreate;
                //Debug.Log("buildinginfo " + moneyCreates);
                /*moneyCreates > 0 || (moneyCreates < 0 && gameManager.GetComponent<GameManagerScript>().money >= -moneyCreates) OLD SHITTY CODE FOR IF STATEMENT DONT USE IT*/
                if (gameManager.GetComponent<GameManagerScript>().money + moneyCreates >= 0)
                {
                    if (gameManager.GetComponent<GameManagerScript>().energy + energyCreates >= 0)
                    /*energyCreates > 0 || (energyCreates < 0 && gameManager.GetComponent<GameManagerScript>().energy >= -energyCreates) MORE OLD SHITTY CODE FOR IF STATEMENT DONT USE IT*/
                    {
                        gameManager.GetComponent<GameManagerScript>().EnergyManager(energyCreates);
                        gameManager.GetComponent<GameManagerScript>().PollutionManager(pollutionCreates);
                        //Debug.Log(transform);
                        //gameManager.GetComponent<GameManagerScript>().MoneyManager(moneyCreates);
                        if (moneyCreates > 0/* && gameManager.GetComponent<GameManagerScript>().pollution >= 50f*/)
                        {
                            if (gameManager.GetComponent<GameManagerScript>().pollution >= 50f)
                            {
                                moneyCreates = (long)(moneyCreates * 0.75f);
                            }
                            else if (gameManager.GetComponent<GameManagerScript>().pollution >= 75f)
                            {
                                moneyCreates = (long)(moneyCreates * 0.5f);
                            }
                        }
                        gameManager.GetComponent<GameManagerScript>().MoneyManager(moneyCreates);
                    }
                    else
                    {
                        Debug.Log("Need more energy!");
                    }
                }
                else
                {
                    Debug.Log("Need more money!");
                }

            }
        }
    }
}

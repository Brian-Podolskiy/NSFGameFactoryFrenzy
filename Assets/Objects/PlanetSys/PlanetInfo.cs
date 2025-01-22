using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInfo : MonoBehaviour
{
    public GameObject gameManager;
    public string displayName;
    //public long moneyCreates; //Currently supports 3 products
    //public int energyCreates; //Products can be positive or negative if it costs something to produce
    //public int pollutionCreates; //More can be added in, but I don't think we need it
    public float timeToCreate;
    public float currentTTC; //ttc is time to create
    public bool wasClicked;

    void Start()
    {
        gameManager = GameObject.Find("gameManager");
    }

    private void Update()
    {
        /*
        if (currentTTC > 0)
        {
            currentTTC -= 1 * Time.deltaTime;
        }
        else
        {
            currentTTC = timeToCreate;
            Debug.Log("buildinginfo " + moneyCreates);
            gameManager.GetComponent<GameManagerScript>().MoneyManager(moneyCreates);
            gameManager.GetComponent<GameManagerScript>().EnergyManager(energyCreates);
            gameManager.GetComponent<GameManagerScript>().PollutionManager(pollutionCreates);
        }
     */   
    }
}

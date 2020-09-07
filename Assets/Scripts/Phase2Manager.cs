using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Manager : MonoBehaviour
{
    public GameObject coalReference;
    public CoalScript coal;
    public GameObject turbineReference;
    public TurbineScript turbine;
    public GameObject oilReference;
    //public OilScript oil;
    public GameObject solarReference;
    public SolarScript solar;

    [Tooltip("The amount of happiness the population has")]
    public float happiness;
    [Tooltip("The levels of pollution currently existing")]
    public float pollutionLevels;
    [Tooltip("The multiples of pollution where if pollution beats it, will cause happiness to drop")]
    public float pollutionTollerance;
    [Tooltip("The amount of times over pollution level beats pollution Tollerance.  Used to track negative events related to polution")]
    public int pollutionTicks;
    [Tooltip("The amount of currency the player has")]
    public static float currency;
    [Tooltip("The current population")]
    public float population;
    [Tooltip("The amount the player earns at the end of each turn per population amount")]
    public float currencyPerPerson;

    [Tooltip("The amount of power needed to increase happiness")]
    public float powerNeeded;
    [Tooltip("The currentPower")]
    public float currentPower;
    [Tooltip("The amount of excess power needed to grow the population")]
    public float excessPowerGrowth;

    private void Start()
    {
        coal = coalReference.GetComponent<CoalScript>();
        turbine = turbineReference.GetComponent<TurbineScript>();
        //oil = oilReference.GetComponent<OilScript>();
        solar = solarReference.GetComponent<SolarScript>();
    }

    public void RunSimulation(int coalAmount, int turbineAmount, int oilAmount, int solarAmount)
    {
        pollutionLevels += coal.pollution * coalAmount;
        //pollutionLevels += oil.pollution * oilAmount;

        currentPower = 0;
        currentPower += coalAmount * coal.power;
        currentPower += turbineAmount * turbine.power;
        //currentPower += oilAmount * oil.power;
        currentPower += solarAmount * solar.power;

        while(currentPower > powerNeeded)
        {
            happiness++;
            currentPower -= excessPowerGrowth;
        }

        if(happiness > population)
        {
            population = happiness;
        }

        if(pollutionLevels > pollutionTollerance * pollutionTicks)
        {
            pollutionTicks++;
        }

        //Something involving punishments with pollutionTicks.

        currency += population * currencyPerPerson;

        //Random Events
    }
}

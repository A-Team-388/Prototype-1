﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Phase2Manager : MonoBehaviour
{
    public GameObject coalReference;
    public CoalScript coal;
    public GameObject turbineReference;
    public TurbineScript turbine;
    public GameObject gasReference;
    public NaturalGasScript gas;
    public GameObject solarReference;
    public SolarScript solar;

    [Tooltip("The amount of happiness the population has")]
    public int happiness;
    [Tooltip("The levels of pollution currently existing")]
    public float pollutionLevels;
    [Tooltip("The multiples of pollution where if pollution beats it, will cause happiness to drop")]
    public float pollutionTollerance;
    [Tooltip("The amount of times over pollution level beats pollution Tollerance.  Used to track negative events related to polution")]
    public int pollutionTicks;
    [Tooltip("The amount of currency the player has")]
    public static float currency;
    [Tooltip("The amount of currency the player starts with")]
    public float startingCurrency;
    [Tooltip("The current population")]
    public int population;
    [Tooltip("The amount the player earns at the end of each turn per population amount")]
    public float currencyPerPerson;

    [Tooltip("The amount of power needed to increase happiness")]
    public float powerNeeded;
    [Tooltip("The currentPower")]
    public float currentPower;
    [Tooltip("The amount of excess power needed to grow the population")]
    [Range(1, 100)]
    public float excessPowerGrowth;
    [Tooltip("The amount of power needed for every house")]
    public float powerNeededPerPerson;

    [Header("Random Event Information and UI Display")]
    public float coalMultiplier = 1f;
    public float gasMultiplier = 1f;
    public float windMultiplier = 1f;
    public float solarMultiplier = 1f;
    public float coalPercentage;
    public float coalTotal;
    public float gasPercentage;
    public float gasTotal;
    public float windPercentage;
    public float windTotal;
    public float solarPercentage;
    public float solarTotal;
    public float previousPopulation;
    public float previousPollution;
    public TextMeshProUGUI currencyAmount;
    public TextMeshProUGUI populationAmount;
    public TextMeshProUGUI environmentThing;
    public TextMeshProUGUI totalPower;
    public TextMeshProUGUI gasPower;
    public TextMeshProUGUI gasPowerPercentage;
    public TextMeshProUGUI coalPower;
    public TextMeshProUGUI coalPowerPercentage;
    public TextMeshProUGUI windPower;
    public TextMeshProUGUI windPowerPercentage;
    public TextMeshProUGUI solarPower;
    public TextMeshProUGUI solarPowerPercentage;
    public TextMeshProUGUI previousPollutionText;
    public TextMeshProUGUI previousPopulationText;

    public enum allRandomEvents { smog };
    public allRandomEvents currentEvent;

    public float smogSolarEffect = .9f;

    StartUpScript start;
    private void Start()
    {
        coal = coalReference.GetComponent<CoalScript>();
        turbine = turbineReference.GetComponent<TurbineScript>();
        gas = gasReference.GetComponent<NaturalGasScript>();
        solar = solarReference.GetComponent<SolarScript>();
        start = FindObjectOfType<StartUpScript>();
        currency = startingCurrency;
        TriggerEvent();
    }
    /*
     * Powered Houses
     * Unpowered Houses
     * Start Random Events
     */
     public void TriggerEvent()
    {
        RunSimulation(BuildMenuFunctions.coalAmount, BuildMenuFunctions.turbineAmount, BuildMenuFunctions.gasAmount, BuildMenuFunctions.solarAmount, start.houseAmount); 
    }

    public void RunSimulation(int coalAmount, int turbineAmount, int gasAmount, int solarAmount, int houseAmount)
    {
        previousPopulation = population;
        previousPopulationText.text = previousPopulation.ToString();
        previousPollution = pollutionLevels;
        previousPollutionText.text = previousPollution.ToString();
        //int unpoweredHouses = houseAmount - poweredHouses;
        powerNeeded = (houseAmount + population) * powerNeededPerPerson;
        pollutionLevels += coal.pollution * coalAmount;
        pollutionLevels += gas.pollution * gasAmount;

        windTotal = 0;
        coalTotal = 0;
        solarTotal = 0;
        gasTotal = 0;
        currentPower = 0;
        coalTotal += coalAmount * coal.power * coalMultiplier;
        windTotal += turbineAmount * turbine.power * windMultiplier;
        gasTotal += gasAmount * gas.power * gasMultiplier;
        solarTotal += solarAmount * solar.power * solarMultiplier;
        currentPower = coalTotal + windTotal + solarTotal + gasTotal;
        coalPercentage = coalTotal / currentPower;
        windPercentage = windTotal / currentPower;
        solarPercentage = solarTotal / currentPower;
        gasPercentage = gasTotal / currentPower;


        happiness = 0;
        while(currentPower > powerNeeded)
        {
            happiness++;
            currentPower -= excessPowerGrowth;
        }

        if(happiness > population)
        {
            population = happiness;

        }
        //start.houseAmount = population;

        if(pollutionLevels > pollutionTollerance * pollutionTicks)
        {
            pollutionTicks++;
        }

        //Something involving punishments with pollutionTicks.

        currency += population * currencyPerPerson;
        //currency -= coal.coalUpkeep + gasAmount.
        UpdateText();



        //Random Events   
        SmogEvent();
    }

    public void SmogEvent()
    {
        currentEvent = allRandomEvents.smog;
        solarMultiplier = smogSolarEffect;
    }

    public void UpdateText()
    {
        currencyAmount.text = currency.ToString();
        populationAmount.text = population.ToString();
        environmentThing.text = pollutionLevels.ToString();
        /*
solarPower.text = solarTotal.ToString();
coalPower.text = coalTotal.ToString();
windPower.text = windTotal.ToString();
gasPower.text = gasTotal.ToString();
totalPower.text = currentPower.ToString();
solarPowerPercentage.text = solarPercentage.ToString();
coalPowerPercentage.text = coalPercentage.ToString();
windPowerPercentage.text = windPercentage.ToString();
gasPowerPercentage.text = gasPercentage.ToString();
*/
    }
}

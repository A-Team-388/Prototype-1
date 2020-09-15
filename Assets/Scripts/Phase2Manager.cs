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
    public float happiness;
    [Tooltip("The levels of pollution currently existing")]
    public float pollutionLevels;
    [Tooltip("The multiples of pollution where if pollution beats it, will cause happiness to drop")]
    public float pollutionTollerance;
    [Tooltip("The amount of times over pollution level beats pollution Tollerance.  Used to track negative events related to polution")]
    public int pollutionTicks;
    [Tooltip("The starting amount of currency the player has")]
    public int startingCurrency;
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
<<<<<<< HEAD
    [Range(1,100)]
=======
>>>>>>> parent of fe2c9607... Fixed Crash, Placing Objects now cost currency, currency UI updates with placing objects.
    public float excessPowerGrowth;

    [Tooltip("The amount of power needed for every house")]
    public float powerNeededPerPerson = 1f;

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

    private void Start()
    {
        currency = startingCurrency;

        coal = coalReference.GetComponent<CoalScript>();
        turbine = turbineReference.GetComponent<TurbineScript>();
        gas = gasReference.GetComponent<NaturalGasScript>();
        solar = solarReference.GetComponent<SolarScript>();

        TriggerEvent();
    }
    /*
     * Powered Houses
     * Unpowered Houses
     * Start Random Events
     */
     public void TriggerEvent()
    {
        StartUpScript start = FindObjectOfType<StartUpScript>();

<<<<<<< HEAD
        RunSimulation(BuildMenuFunctions.coalAmount, BuildMenuFunctions.turbineAmount, BuildMenuFunctions.gasAmount, BuildMenuFunctions.solarAmount, StartUpScript.houseAmount); 
    }

    
    public void UpdateUi(int coalAmount, int turbineAmount, int gasAmount, int solarAmount)
    {
        currentPower = 0;
        coalTotal += coalAmount * coal.power * coalMultiplier;
        windTotal += turbineAmount * turbine.power * windMultiplier;
        gasTotal += gasAmount * gas.power * gasMultiplier;
        solarTotal += solarAmount * solar.power * solarMultiplier;
        currentPower = coalTotal + windTotal + solarTotal;
        totalPower.text = currentPower.ToString();
        currencyAmount.text = currency.ToString();
=======
        RunSimulation(BuildMenuFunctions.coalAmount, BuildMenuFunctions.turbineAmount, BuildMenuFunctions.gasAmount, BuildMenuFunctions.solarAmount, start.houseAmount); 
>>>>>>> parent of fe2c9607... Fixed Crash, Placing Objects now cost currency, currency UI updates with placing objects.
    }
    

    public void RunSimulation(int coalAmount, int turbineAmount, int gasAmount, int solarAmount, int houseAmount)
    {
        //previousPopulation = population;
        //.text = previousPopulation.ToString();
        //previousPollution = pollutionLevels;
        //previousPollutionText.text = previousPollution.ToString();
        //int unpoweredHouses = houseAmount - poweredHouses;
        powerNeeded = houseAmount * powerNeededPerPerson;
        pollutionLevels += coal.pollution * coalAmount;
        pollutionLevels += gas.pollution * gasAmount;

<<<<<<< HEAD
=======

>>>>>>> parent of fe2c9607... Fixed Crash, Placing Objects now cost currency, currency UI updates with placing objects.
        currentPower = 0;
        coalTotal += coalAmount * coal.power * coalMultiplier;
        windTotal += turbineAmount * turbine.power * windMultiplier;
        gasTotal += gasAmount * gas.power * gasMultiplier;
        solarTotal += solarAmount * solar.power * solarMultiplier;
        currentPower = coalTotal + windTotal + solarTotal;
        coalPercentage = coalTotal / currentPower;
        windPercentage = windTotal / currentPower;
        solarPercentage = solarTotal / currentPower;
        gasPercentage = gasTotal / currentPower;

        /*
        solarPower.text = solarTotal.ToString();
        coalPower.text = coalTotal.ToString();
        windPower.text = windTotal.ToString();
        gasPower.text = gasTotal.ToString();
<<<<<<< HEAD
        */
        totalPower.text = currentPower.ToString();
        /*
=======
        totalPower.text = currentPower.ToString();
>>>>>>> parent of fe2c9607... Fixed Crash, Placing Objects now cost currency, currency UI updates with placing objects.
        solarPowerPercentage.text = solarPercentage.ToString();
        coalPowerPercentage.text = coalPercentage.ToString();
        windPowerPercentage.text = windPercentage.ToString();
        gasPowerPercentage.text = gasPercentage.ToString();
        */
<<<<<<< HEAD



        
        while (currentPower > powerNeeded)
=======
        while(currentPower > powerNeeded)
>>>>>>> parent of fe2c9607... Fixed Crash, Placing Objects now cost currency, currency UI updates with placing objects.
        {
            happiness++;
            currentPower --;
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
        //currency -= coal.coalUpkeep + gasAmount.

        currencyAmount.text = currency.ToString();
        populationAmount.text = population.ToString();
        environmentThing.text = pollutionLevels.ToString();


        //Random Events   
        SmogEvent();
    }

    public void SmogEvent()
    {
        currentEvent = allRandomEvents.smog;
        solarMultiplier = smogSolarEffect;
    }
}

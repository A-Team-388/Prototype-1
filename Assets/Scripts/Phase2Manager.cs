using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

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
    [Range(1, 100)]
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
    public TextMeshProUGUI housesPoweredText;
    public TextMeshProUGUI housesUnpoweredText;

    public enum allRandomEvents { smog, treesFall, unhealthyAir, windmillBreaks, protests, gasLeak, cloudyDay };
    public allRandomEvents currentEvent;

    public float smogSolarEffect = .9f;
    public float smogThreshold;
    public float unhealthyAirThreshold;
    public float unhealthyAirMultiplier = 0f;
    public float cloudsMultiplier = .5f;
    StartUpScript start;

    BuildFunctions build;

    public static int amountOfHousesPowered = 0;
    public static int amountOfHousesUnpowered = 0;

    [Range(0, 1)]
    public float solarDeviationMin;
    [Range(0, 1)]
    public float solarDeviationMax;
    [Range(0, 1)]
    public float windDeviationMin;
    [Range(0, 1)]
    public float windDeviationMax;


    private void Start()
    {
        amountOfHousesUnpowered = StartUpScript.houseAmount;
        currency = startingCurrency;

        build = FindObjectOfType<BuildFunctions>();
        coal = coalReference.GetComponent<CoalScript>();
        turbine = turbineReference.GetComponent<TurbineScript>();
        gas = gasReference.GetComponent<NaturalGasScript>();
        solar = solarReference.GetComponent<SolarScript>();



        TriggerEvent();
        if (excessPowerGrowth < 1)
        {
            excessPowerGrowth = 1;
        }



    }
    /*
     * Powered Houses
     * Unpowered Houses
     * Start Random Events
     */

    public void TriggerEvent()
    {
        start = FindObjectOfType<StartUpScript>();
        RunSimulation(BuildMenuFunctions.coalAmount, BuildMenuFunctions.turbineAmount, BuildMenuFunctions.gasAmount, BuildMenuFunctions.solarAmount, StartUpScript.houseAmount);

    }

    //updates the total power ui element
    public void UpdateUi(int coalAmount, int turbineAmount, int gasAmount, int solarAmount)
    {

        currentPower = 0;
        coalTotal = coalAmount * coal.power * coalMultiplier;
        windTotal = turbineAmount * turbine.power * windMultiplier;
        gasTotal = gasAmount * gas.power * gasMultiplier;
        solarTotal = solarAmount * solar.power * solarMultiplier;

        currentPower = coalTotal + windTotal + solarTotal + gasTotal;
        totalPower.text = currentPower.ToString();

        solarPower.text = solarTotal.ToString();
        coalPower.text = coalTotal.ToString();
        windPower.text = windTotal.ToString();
        gasPower.text = gasTotal.ToString();


        coalPercentage = coalTotal / currentPower;
        windPercentage = windTotal / currentPower;
        solarPercentage = solarTotal / currentPower;
        gasPercentage = gasTotal / currentPower;

        populationAmount.text = StartUpScript.houseAmount.ToString();

        if (currentPower != 0)
        {
            solarPowerPercentage.text = System.String.Format("{0:0.0%}", solarPercentage);
            coalPowerPercentage.text = System.String.Format("{0:0.0%}", coalPercentage);
            windPowerPercentage.text = System.String.Format("{0:0.0%}", windPercentage);
            gasPowerPercentage.text = System.String.Format("{0:0.0%}", gasPercentage);
        }
        else
        {
            solarPowerPercentage.text = "0.0%";
            coalPowerPercentage.text = "0.0%";
            windPowerPercentage.text = "0.0%";
            gasPowerPercentage.text = "0.0%";
        }

        housesPoweredText.text = amountOfHousesPowered.ToString();
        housesUnpoweredText.text = amountOfHousesUnpowered.ToString();
    }

    //updates the total currency amount
    public void UpdateCurrency()
    {
        currencyAmount.text = currency.ToString();
    }



    public void RunSimulation(int coalAmount, int turbineAmount, int gasAmount, int solarAmount, int houseAmount)
    {
        if (brokeWindmill)
        {
            turbineAmount -= 1;
            brokeWindmill = false;
        }
        if (gasLeak)
        {
            turnCount++;
            if (turnCount <= 2)
            {
                gasAmount--;
            }
            else
            {
                turnCount = 0;
                gasLeak = false;
            }
        }
        if(protest)
        {
            gasAmount--;
        }
        //previousPopulation = population;
        //.text = previousPopulation.ToString();
        //previousPollution = pollutionLevels;
        //previousPollutionText.text = previousPollution.ToString();
        //int unpoweredHouses = houseAmount - poweredHouses;
        powerNeeded = population * powerNeededPerPerson;
        pollutionLevels += coal.pollution * coalAmount;
        pollutionLevels += gas.pollution * gasAmount;

        currentPower = 0;
        coalTotal = coalAmount * coal.power * coalMultiplier;
        windTotal = turbineAmount * turbine.power * windMultiplier;
        gasTotal = gasAmount * gas.power * gasMultiplier;
        solarTotal = solarAmount * solar.power * solarMultiplier;
        currentPower = coalTotal + windTotal + solarTotal + gasTotal;
        coalPercentage = coalTotal / currentPower;
        windPercentage = windTotal / currentPower;
        solarPercentage = solarTotal / currentPower;
        gasPercentage = gasTotal / currentPower;


        solarPower.text = solarTotal.ToString();
        coalPower.text = coalTotal.ToString();
        windPower.text = windTotal.ToString();
        gasPower.text = gasTotal.ToString();

        totalPower.text = currentPower.ToString();
        /*
        totalPower.text = currentPower.ToString();
        totalPower.text = currentPower.ToString();
        */

        solarPowerPercentage.text = System.String.Format("{0:0.0%}", solarPercentage);
        coalPowerPercentage.text = System.String.Format("{0:0.0%}", coalPercentage);
        windPowerPercentage.text = System.String.Format("{0:0.0%}", windPercentage);
        gasPowerPercentage.text = System.String.Format("{0:0.0%}", gasPercentage);



        Debug.Log(amountOfHousesPowered + "houses powered");
        Debug.Log(amountOfHousesUnpowered + "houses unpowered");

        if (pollutionLevels > pollutionTollerance * pollutionTicks)
        {
            pollutionTicks++;
        }

        if (amountOfHousesUnpowered > 0)
        {
            while (currentPower > powerNeeded)
            {
                happiness++;
                currentPower -= excessPowerGrowth;
            }
        }

        happiness -= pollutionTicks;

        if (happiness > population)
        {
            population = happiness;
        }

        if (amountOfHousesUnpowered > 3)
        {
            int totalUnpowered = amountOfHousesUnpowered;

            while (totalUnpowered > 2)
            {
                population--;
                totalUnpowered -= 2;
            }

        }




        housesPoweredText.text = amountOfHousesPowered.ToString();
        housesUnpoweredText.text = amountOfHousesUnpowered.ToString();


        //Something involving punishments with pollutionTicks.

        currency += amountOfHousesPowered * currencyPerPerson;
        //currency -= coal.coalUpkeep + gasAmount.
        currency -= CoalScript.upkeep * coalAmount;
        currency -= NaturalGasScript.upkeep * gasAmount;
        currency -= SolarScript.upkeep * solarAmount;
        currency -= TurbineScript.upkeep * turbineAmount;

        currencyAmount.text = currency.ToString();
        populationAmount.text = StartUpScript.houseAmount.ToString();
        environmentThing.text = pollutionLevels.ToString();
        UpdateUi(coalAmount, turbineAmount, gasAmount, solarAmount);

        coalMultiplier = 1f;
        gasMultiplier = 1f;
        solarMultiplier = Random.Range(solarDeviationMin, solarDeviationMax);
        windMultiplier = Random.Range(windDeviationMin, windDeviationMax);
        RollRandom();
    }

    public void RollRandom()
    {
        int RandomValue;
        int totalRandom = 5;

        if (pollutionLevels > smogThreshold)
        {
            totalRandom++;
        }
        if (pollutionLevels > unhealthyAirThreshold)
        {
            totalRandom++;
        }

        RandomValue = Random.Range(0, totalRandom);

        switch(RandomValue)
        {
            case 0:
                TreesFall();
                break;
            case 1:
                WindmillBreak();
                break;
            case 2:
                GasLeak();
                break;
            case 3:
                Protest();
                break;
            case 4:
                break;
            case 5:
                SmogEvent();
                break;
            case 6:
                UnheathlyAir();
                break;
        }
    }

    public void SmogEvent()
    {
        currentEvent = allRandomEvents.smog;
        solarMultiplier = smogSolarEffect;
    }

    public void TreesFall()
    {
        GameObject[] allPower = GameObject.FindGameObjectsWithTag("Power");
        List<GameObject> nextToTrees = new List<GameObject>();
        for (int i = 0; i < allPower.Length; i++)
        {
            if(build.isObjectNextToTree(allPower[i]))
            {
                nextToTrees.Add(allPower[i]);
            }
        }

        if(nextToTrees == null)
        {
            return;
        }

        int rollRandom = Random.Range(0, nextToTrees.Count);
        GameObject[] toArray = nextToTrees.ToArray();
        Destroy(toArray[rollRandom]);
        currentEvent = allRandomEvents.treesFall;
    }

    public void UnheathlyAir()
    {
        coalMultiplier = unhealthyAirMultiplier;
        gasMultiplier = unhealthyAirMultiplier;
        currentEvent = allRandomEvents.unhealthyAir;
    }
    bool brokeWindmill;
    public void WindmillBreak()
    {
        brokeWindmill = true;
        currentEvent = allRandomEvents.windmillBreaks;
    }

    bool gasLeak;
    int turnCount;
    public void GasLeak()
    {
        gasLeak = true;
        currentEvent = allRandomEvents.gasLeak;
    }

    bool protest;
    public void Protest()
    {
        protest = true;
        currency -= NaturalGasScript.cost;
        currentEvent = allRandomEvents.protests;
    }

    public void Clouds()
    {
        solarMultiplier = cloudsMultiplier;
        currentEvent = allRandomEvents.cloudyDay;
    }
}

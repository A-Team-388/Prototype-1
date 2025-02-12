﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBarScript : MonoBehaviour
{
    public GameObject powerPanel;
    public GameObject populationPanel;

    public void PowerButtonFunction()
    {
        SoundManager.PlaySound("menu");
        powerPanel.SetActive(!powerPanel.activeSelf);
    }

    public void PopulationButtonFunction()
    {
        SoundManager.PlaySound("menu");
        populationPanel.SetActive(!populationPanel.activeSelf);
    }
}

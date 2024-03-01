using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager instance;

    public int availableUpgrades = 0, playerLevel = 1;

    public float playerExperience, expRequiredForNextLevel;



    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        expRequiredForNextLevel = Mathf.Pow(2, (playerLevel - 1) / 10) * 8 * playerLevel;
    }

    private void Update()
    {
        if (playerExperience >= expRequiredForNextLevel)
        {
            playerExperience -= expRequiredForNextLevel;
            playerLevel++;
            availableUpgrades++;
            expRequiredForNextLevel = Mathf.Pow(2, (playerLevel - 1) / 10) * playerLevel;

            UIManager.instance.frontXPBar.fillAmount = 0;
            UIManager.instance.UpdateLevel();


        }
    }

    public void AddExperience(float exp)
    {
        playerExperience += exp;
    }
}

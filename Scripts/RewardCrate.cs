using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCrate : MonoBehaviour
{
    int randomRoll;
    public AudioSource healSound, moneySound, xpSound;

    float respawnTimer;
    private void Start()
    {
        // 0 - Health, 1 - Coins, 2 - Experience
        randomRoll = Random.Range(0, 3);
        respawnTimer = Random.Range(90, 180);
    }

    public void GetReward()
    {
        BoxCollider cratecollider = GetComponent<BoxCollider>();

        switch (randomRoll)
        {
            case 0:
                GainHealth();
                StartCoroutine("respawnCrate");
                foreach (Transform child in transform)
                {
                    MeshRenderer renderer = child.GetComponent<MeshRenderer>();
                    renderer.enabled = false;
                }
                cratecollider.enabled = false;
                break;

            case 1:
                GainCoins();
                StartCoroutine("respawnCrate");
                foreach (Transform child in transform)
                {
                    MeshRenderer renderer = child.GetComponent<MeshRenderer>();
                    renderer.enabled = false;
                    cratecollider.enabled = false;
                }
                break;

            case 2:
                GainExperience();
                StartCoroutine("respawnCrate");
                foreach (Transform child in transform)
                {
                    MeshRenderer renderer = child.GetComponent<MeshRenderer>();
                    renderer.enabled = false;
                    cratecollider.enabled = false;
                }

                break;
        }
    }

    void GainHealth()
    {
        PlayerStats.instance.currentHealth += PlayerStats.instance.maximumHealth * 0.2f;

        if (PlayerStats.instance.currentHealth > PlayerStats.instance.maximumHealth)
        {
            PlayerStats.instance.currentHealth = PlayerStats.instance.maximumHealth;
        }
        UIManager.instance.UpdateHP();
        UIManager.instance.UpdateMaxHP();
        healSound.Play();
    }

    void GainCoins()
    {
        PlayerStats.instance.coins += ((int)(50 * (PlayerStats.instance.elapsedTime / 60)));
        UIManager.instance.UpdateMoney();
        moneySound.Play();
    }

    void GainExperience()
    {
        ExperienceManager.instance.AddExperience(ExperienceManager.instance.expRequiredForNextLevel * 0.4f);
        xpSound.Play();
    }

    IEnumerator respawnCrate()
    {
        yield return new WaitForSeconds(respawnTimer);
        foreach (Transform child in transform)
        {
            MeshRenderer renderer = child.GetComponent<MeshRenderer>();
            renderer.enabled = true;

        }
        BoxCollider cratecollider = GetComponent<BoxCollider>();
        cratecollider.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    public GameObject[] Upgrades;
    public GameObject UpgradeChooserScreen;
    public int[] upgradeCount, upgradeRolled;
    public int remainingUpgrades;
    private bool upgradeLocked = false;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        upgradeCount = new int[Upgrades.Length];
        upgradeRolled = new int[Upgrades.Length];
        remainingUpgrades = Upgrades.Length;
    }

    public void BulletAmountUpgrade()
    {
        PlayerAimControl.instance.bulletAmount++;
        upgradeCount[0]++;

        if (upgradeCount[0] == 8)
        {
            remainingUpgrades--;
        }

        DoneUpgrade();
    }

    public void BulletPenetrationUpgrade()
    {
        PlayerAimControl.instance.bulletPenetration++;
        upgradeCount[1]++;

        if (upgradeCount[1] == 8)
        {
            remainingUpgrades--;
        }

        DoneUpgrade();
    }

    public void BulletSizeUpgrade()
    {
        PlayerAimControl.instance.bulletSizeScale += 0.1f;
        PlayerAimControl.instance.bulletDamage += 0.5f;

        upgradeCount[2]++;

        if (upgradeCount[2] == 8)
        {
            remainingUpgrades--;
        }

        DoneUpgrade();
    }

    public void BulletDamageUpgrade()
    {
        PlayerAimControl.instance.bulletDamage += 2;
        upgradeCount[3]++;

        if (upgradeCount[3] == 8)
        {
            remainingUpgrades--;
        }

        DoneUpgrade();
    }

    public void BulletSpeedUpgrade()
    {
        PlayerAimControl.instance.bulletSpeed += 0.2f;
        upgradeCount[4]++;

        if (upgradeCount[4] == 8)
        {
            remainingUpgrades--;
        }

        DoneUpgrade();
    }

    public void PlayerHealthUpgrade()
    {
        PlayerStats.instance.maximumHealth += 20f;
        PlayerStats.instance.currentHealth += (PlayerStats.instance.maximumHealth * 0.3f);

        if (PlayerStats.instance.currentHealth > PlayerStats.instance.maximumHealth)
        {
            PlayerStats.instance.currentHealth = PlayerStats.instance.maximumHealth;
        }

        upgradeCount[5]++;

        if (upgradeCount[5] == 8)
        {
            remainingUpgrades--;
        }

        DoneUpgrade();
    }

    public void PlayerMovementUpgrade()
    {
        UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter.instance.m_MoveSpeedMultiplier += 0.2f;
        upgradeCount[6]++;

        if (upgradeCount[6] == 8)
        {
            remainingUpgrades--;
        }

        DoneUpgrade();
    }

    public void PlayerJumpUpgrade()
    {
        UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter.instance.m_JumpPower += 0.2f;
        upgradeCount[7]++;

        if (upgradeCount[7] == 8)
        {
            remainingUpgrades--;
        }

        DoneUpgrade();
    }

    public void MachineGunUpgrade()
    {
        PlayerAimControl.instance.shotsCooldown = .1f;
        PlayerAimControl.instance.bulletDamage /= 2f;
        upgradeCount[8]++;

        if (upgradeCount[8] == 1)
        {
            remainingUpgrades -= 2;
        }

        DoneUpgrade();
    }

    public void SniperUpgrade()
    {
        PlayerAimControl.instance.bulletDamage += 100f;
        PlayerAimControl.instance.shotsCooldown = 1.5f;
        PlayerAimControl.instance.bulletSizeScale += 0.2f;
        PlayerAimControl.instance.bulletPenetration++;

        if (upgradeCount[0] < 8)
        {
            PlayerAimControl.instance.bulletAmount = 1;
            upgradeCount[0] = 8;
            remainingUpgrades--;
        }

        upgradeCount[9]++;

        if (upgradeCount[9] == 1)
        {
            remainingUpgrades -= 2;
        }

        DoneUpgrade();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && ExperienceManager.instance.availableUpgrades > 0)
        {
            if (!UpgradeChooserScreen.activeInHierarchy)
            {
                OpenUpgrade();
                RollUpgrades();
            }
            else
            {
                CloseUpgrade();
            }
        }
    }

    public void RollUpgrades()
    {
        if (!upgradeLocked)
        {
            int upgradeChoiceCount = remainingUpgrades > 2 ? 3 : remainingUpgrades;

            if (upgradeCount[8] == 0 && upgradeCount[9] == 0)
            {
                if (upgradeCount[0] == 8 && upgradeCount[4] == 8)
                {
                    upgradeRolled[8] = 1;
                    GameObject riggedUpgrade = Upgrades[8];
                    riggedUpgrade.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 150);
                    riggedUpgrade.SetActive(true);

                    for (int i = 1; i < upgradeChoiceCount; i++)
                    {
                        int rolledIndex = Random.Range(0, Upgrades.Length - 2);

                        while (upgradeCount[rolledIndex] == 8 || upgradeRolled[rolledIndex] == 1)
                        {
                            rolledIndex = Random.Range(0, Upgrades.Length - 2);
                        }

                        upgradeRolled[rolledIndex] = 1;
                        GameObject rolledUpgrade = Upgrades[rolledIndex];
                        rolledUpgrade.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 150 - (170 * i));
                        rolledUpgrade.SetActive(true);
                    }
                }
                else if (upgradeCount[1] == 8 && upgradeCount[3] == 8 && upgradeCount[4] == 8)
                {
                    upgradeRolled[9] = 1;
                    GameObject riggedUpgrade = Upgrades[9];
                    riggedUpgrade.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 150);
                    riggedUpgrade.SetActive(true);

                    for (int i = 1; i < upgradeChoiceCount; i++)
                    {
                        int rolledIndex = Random.Range(0, Upgrades.Length - 2);

                        while (upgradeCount[rolledIndex] == 8 || upgradeRolled[rolledIndex] == 1)
                        {
                            rolledIndex = Random.Range(0, Upgrades.Length - 2);
                        }

                        upgradeRolled[rolledIndex] = 1;
                        GameObject rolledUpgrade = Upgrades[rolledIndex];
                        rolledUpgrade.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 150 - (170 * i));
                        rolledUpgrade.SetActive(true);
                    }
                }
                else
                {
                    for (int i = 0; i < upgradeChoiceCount; i++)
                    {
                        int rolledIndex = Random.Range(0, Upgrades.Length - 2);

                        while (upgradeCount[rolledIndex] == 8 || upgradeRolled[rolledIndex] == 1)
                        {
                            rolledIndex = Random.Range(0, Upgrades.Length - 2);
                        }

                        upgradeRolled[rolledIndex] = 1;
                        GameObject rolledUpgrade = Upgrades[rolledIndex];
                        rolledUpgrade.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 150 - (170 * i));
                        rolledUpgrade.SetActive(true);
                    }
                }
            }
            else
            {
                for (int i = 0; i < upgradeChoiceCount; i++)
                {
                    int rolledIndex = Random.Range(0, Upgrades.Length - 2);

                    while (upgradeCount[rolledIndex] == 8 || upgradeRolled[rolledIndex] == 1)
                    {
                        rolledIndex = Random.Range(0, Upgrades.Length - 2);
                    }

                    upgradeRolled[rolledIndex] = 1;
                    GameObject rolledUpgrade = Upgrades[rolledIndex];
                    rolledUpgrade.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 150 - (170 * i));
                    rolledUpgrade.SetActive(true);
                }
            }

            upgradeLocked = true;
        }
    }

    private void DoneUpgrade()
    {
        ExperienceManager.instance.availableUpgrades--;
        UIManager.instance.UpdateHP();
        UIManager.instance.UpdateMaxHP();
        upgradeLocked = false;

        for (int i = 0; i < Upgrades.Length; i++)
        {
            upgradeRolled[i] = 0;
            Upgrades[i].SetActive(false);
        }

        if (ExperienceManager.instance.availableUpgrades > 0)
        {
            RollUpgrades();
        }
        else
        {
            CloseUpgrade();
        }
    }

    public void OpenUpgrade()
    {
        UpgradeChooserScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseUpgrade()
    {
        UpgradeChooserScreen.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}

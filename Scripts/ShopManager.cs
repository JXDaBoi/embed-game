using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public UserStats userStats;
    public GameObject ShopMenu;
    public GameObject buyHealthButton, buyReviveButton, buyKeyItemButton;
    public GameObject winUI;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "firstlv")
        {
            if (!buyKeyItemButton.activeInHierarchy)
            {
                buyKeyItemButton.SetActive(true);
            }
        }

        if (PlayerStats.instance.currentHealth < PlayerStats.instance.maximumHealth && PlayerStats.instance.coins >= 300)
        {
            buyHealthButton.GetComponent<Button>().enabled = true;
            buyHealthButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            buyHealthButton.GetComponent<Button>().enabled = false;
            buyHealthButton.GetComponent<Image>().color = Color.grey;
        }

        if (!PlayerStats.instance.hasRevive && PlayerStats.instance.coins >= 800)
        {
            buyReviveButton.GetComponent<Button>().enabled = true;
            buyReviveButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            buyReviveButton.GetComponent<Button>().enabled = false;
            buyReviveButton.GetComponent<Image>().color = Color.grey;
        }

        if (PlayerStats.instance.coins >= 1500)
        {
            buyKeyItemButton.GetComponent<Button>().enabled = true;
            buyKeyItemButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            buyKeyItemButton.GetComponent<Button>().enabled = false;
            buyKeyItemButton.GetComponent<Image>().color = Color.grey;
        }
    }

    public void Health()
    {
        PlayerStats.instance.IncreaseHealth(50f);
        PlayerStats.instance.coins -= 300;
        UIManager.instance.UpdateMoney();
    }

    public void Revive()
    {
        PlayerStats.instance.hasRevive = true;
        PlayerStats.instance.coins -= 800;
        UIManager.instance.UpdateMoney();
    }

    public void KeyItem()
    {
        CloseShop();
        PlayerStats.instance.coins -= 1500;

        StartCoroutine("ToNextLevel");
    }

    public void TriggerShop()
    {
        if (!ShopMenu.activeInHierarchy)
        {
            OpenShop();
        }
        else
        {
            CloseShop();
        }
    }

    void OpenShop()
    {
        ShopMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void CloseShop()
    {
        ShopMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator ToNextLevel()
    {
        userStats.levelPassed();
        winUI.SetActive(true);
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("secondlv");
    }
}

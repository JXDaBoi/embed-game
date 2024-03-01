using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public float currentHealth, maximumHealth = 140f, playerSpeed, playerJumpPower, elapsedTime;
    public int coins;
    public bool hasRevive = false;
    public GameObject DeathScreen, reviveButton;

    public AudioSource gotHurt;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        coins = 0;
        currentHealth = maximumHealth;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        playerSpeed = UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter.instance.m_MoveSpeedMultiplier;
        playerJumpPower = UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter.instance.m_JumpPower;

        if (currentHealth <= 0)
        {
            Time.timeScale = 0f;

            DeathScreen.SetActive(true);

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            if (hasRevive)
            {
                reviveButton.SetActive(true);
            }
        }
    }

    public void RevivePlayer()
    {
        hasRevive = false;
        reviveButton.SetActive(false);

        currentHealth = maximumHealth / 2;

        DeathScreen.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) <= 10f)
            {
                Destroy(enemy);
            }
        }

        UIManager.instance.UpdateHP();
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void IncreaseHealth(float health)
    {
        currentHealth += health;

        if (currentHealth > maximumHealth)
        {
            currentHealth = maximumHealth;
        }

        UIManager.instance.UpdateHP();
        UIManager.instance.UpdateMaxHP();
    }

    public void Hurt(float damage)
    {
        currentHealth -= damage;

        UIManager.instance.UpdateHP();

        UIManager.instance.screenDamageUI.color = new Color(UIManager.instance.screenDamageUI.color.r, UIManager.instance.screenDamageUI.color.g, UIManager.instance.screenDamageUI.color.b, 1);

        gotHurt.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBody")
        {
            currentHealth -= 2;

            UIManager.instance.UpdateHP();
        }
    }
}

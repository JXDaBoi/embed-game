using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossAI : MonoBehaviour
{
    public static FinalBossAI instance;
    public GameObject[] teleportationPoints, explosionPoints, flamePoints;
    public GameObject explosion, flame;
    public Animator anim;

    public float enemyHealth;
    public float initialEnemyHealth;
    private float attackCooldown, attackTimer, teleportationCooldown, teleportationTimer;
    private GameObject player;
    private bool isDead = false;

    public GameObject winUI;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        attackCooldown = Random.Range(5f, 15f);
        attackTimer = attackCooldown;
        teleportationCooldown = Random.Range(10f, 25f);
        teleportationTimer = teleportationCooldown;
        player = GameObject.FindGameObjectWithTag("Player");
        teleportationPoints = GameObject.FindGameObjectsWithTag("bossTP");
        explosionPoints = GameObject.FindGameObjectsWithTag("ExplosionPoint");
        flamePoints = GameObject.FindGameObjectsWithTag("FlamePoint");
        initialEnemyHealth = enemyHealth;
    }

    private void Update()
    {
        if (!isDead)
        {
            // Attack
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f)
            {
                int attackMove = Random.Range(0, 3);

                if (attackMove == 0)
                {
                    anim.SetTrigger("spell1");

                    int effectType = Random.Range(0, 2);

                    if (effectType == 0)
                    {
                        foreach (var explosionPoint in explosionPoints)
                        {
                            Instantiate(explosion, explosionPoint.transform.position + new Vector3(0f, 20f, 0f), Quaternion.identity);
                        }
                    }
                    else
                    {
                        foreach (var flamePoint in flamePoints)
                        {
                            Instantiate(flame, flamePoint.transform.position + new Vector3(0f, -5f, 0f), Quaternion.Euler(-90f, 0f, 0f));
                        }
                    }
                }
                else
                {
                    anim.SetTrigger("spell2");

                    GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>().SpawnEnemy();
                }

                attackCooldown = Random.Range(5f, 15f);
                attackTimer = attackCooldown;
            }

            // Teleportation
            teleportationTimer -= Time.deltaTime;

            if (teleportationTimer <= 0f)
            {
                int teleportLocation = Random.Range(0, teleportationPoints.Length);

                StartCoroutine(Teleportation(teleportLocation));

                teleportationCooldown = Random.Range(10f, 25f);
                teleportationTimer = teleportationCooldown;
            }

            // Regeneration
            if (enemyHealth < initialEnemyHealth)
            {
                enemyHealth += 6 * Time.deltaTime;
            }
        }
    }

    IEnumerator Teleportation(int locationIndex)
    {
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        GetComponentInChildren<BoxCollider>().enabled = false;

        transform.position = teleportationPoints[locationIndex].transform.position;

        yield return new WaitForSeconds(3f);

        transform.LookAt(player.transform);
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        GetComponentInChildren<BoxCollider>().enabled = true;
    }

    public void enemyDamaged(float damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            isDead = true;
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;

            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            winUI.SetActive(true);
        }
    }
}

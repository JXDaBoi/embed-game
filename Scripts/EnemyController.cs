using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    public float enemyHealth;
    public NavMeshAgent agent;
    public UserStats userStats;

    public float initialSpeed;

    private GameObject player;
    private Animator anim;

    private float initialEnemyHealth;
    private bool isDead = false;

    public GameObject DeathEffect;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        enemyHealth = enemyHealth + Mathf.Pow(PlayerStats.instance.elapsedTime * 0.03f, 1.5f);
        initialEnemyHealth = enemyHealth;

        initialSpeed = agent.speed;
        agent.speed = initialSpeed + (PlayerStats.instance.elapsedTime /150);

    }

    void Update()
    {
        if (!isDead)
        {
            if (Vector3.Distance(transform.position, player.transform.position) >= 3f)
            {
                anim.SetBool("Run", true);
                agent.destination = player.transform.position;
            }
            else
            {
                anim.SetBool("Run", false);
                anim.SetTrigger("Attack");
            }
        }
    }

    public void enemyDamaged(float damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            isDead = true;
            int coinAmount = Random.Range(3, 8);
            PlayerStats.instance.coins += coinAmount + Mathf.FloorToInt(initialEnemyHealth * 0.1f);
            UIManager.instance.UpdateMoney();

            ExperienceManager.instance.AddExperience(initialEnemyHealth * .8f);

            if (gameObject.tag == "MiniBoss")
            {
                GetComponent<NavMeshAgent>().enabled = false;
                GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                GetComponentInChildren<Collider>().enabled = false;
                StartCoroutine(ToNextLevel());
            }
            else
            {
                Destroy(gameObject);
            }
            Instantiate(DeathEffect, transform.position, transform.rotation);
        }
    }

    IEnumerator ToNextLevel()
    {
        MiniBossTrigger.instance.winUi.SetActive(true);

        userStats.levelPassed();
       
        yield return new WaitForSeconds(8f);
        
        SceneManager.LoadScene("thirdlv");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBullet : MonoBehaviour
{
    public static PlayerBullet instance;

    public GameObject DamageIndicator;
    public float bulletDamage, bulletDecayTime;
    public int penetrationDurability = 0;

    public GameObject explosionEffect;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        penetrationDurability = PlayerAimControl.instance.bulletPenetration;
        StartCoroutine(BulletDecay());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBody")
        {
            penetrationDurability--;

            other.transform.parent.GetComponent<EnemyController>().enemyDamaged(bulletDamage);
            DamageIndication(other.transform);

            if (penetrationDurability == 0)
            {
                Destroy(gameObject);
            }

            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        if (other.tag == "Boss")
        {
            penetrationDurability--;

            other.GetComponent<FinalBossAI>().enemyDamaged(bulletDamage);
            DamageIndication(other.transform);

            if (penetrationDurability == 0)
            {
                Destroy(gameObject);
            }

            Instantiate(explosionEffect, transform.position, transform.rotation);
        }
    }

    IEnumerator BulletDecay()
    {
        yield return new WaitForSeconds(bulletDecayTime);
        Destroy(gameObject);
    }

    void DamageIndication(Transform enemyPosition)
    {
        GameObject dmgIndicator = Instantiate(DamageIndicator, enemyPosition.position + new Vector3(1f, 1f, 0f), Quaternion.identity);
        dmgIndicator.GetComponentInChildren<SuperTextMesh>().text = "<drawAnim=Fly><readDelay=0.01><j>" + bulletDamage.ToString();
    }
}

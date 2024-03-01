using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimControl : MonoBehaviour
{
    public static PlayerAimControl instance;
    public Transform playerTransform, shootOrigin;
    public GameObject[] playerBullet;
    public int bulletType;
    public Camera cam;
    public int bulletAmount = 1;

    GameObject hitObject = null;

    public int bulletPenetration = 1;
    public float bulletSizeScale = 1f, bulletDamage = 1f, fireRate = .1f, shotsCooldown = 2f, bulletSpeed = 1f;
    public bool shootLock = false;

    public GameObject interactUI,gunUI,sniperUI,machinegunUI;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        bulletType = 0; // default bullet

    }
    void Update()
    {
        if (shotsCooldown <= .1f)
        {
            bulletType = 1; // change to machinegun bullet
            gunUI.SetActive(false);
            machinegunUI.SetActive(true);
        }
        if (bulletDamage >= 100f)
        {
            bulletType = 2; // change to sniper bullet
            gunUI.SetActive(false);
            sniperUI.SetActive(true);
        }
        if (Input.GetButton("Fire1") && Time.timeScale != 0 && !shootLock)
        {
            shootLock = true;
            UIManager.instance.updateReload();

            StartCoroutine(Shoot());
        }

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 5.5f))
        {
            if (hit.collider.tag == "shop")
            {
                hitObject = hit.collider.gameObject;
                hitObject.GetComponent<Outline>().enabled = true;
                interactUI.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    ShopManager.instance.TriggerShop();
                    interactUI.gameObject.SetActive(false);
                }
            }

            if (hit.collider.tag == "rewardCrate")
            {
                hitObject = hit.collider.gameObject;
                hitObject.GetComponent<Outline>().enabled = true;
                interactUI.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hitObject.GetComponent<RewardCrate>().GetReward();
                    interactUI.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (hitObject != null)
            {
                hitObject.GetComponent<Outline>().enabled = false;
                hitObject = null;
                interactUI.gameObject.SetActive(false);
            }
        }
    }

    void ShootBullet()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        GameObject spawnedBullet = Instantiate(playerBullet[bulletType], shootOrigin.position, playerTransform.rotation);
        spawnedBullet.transform.localScale = new Vector3(bulletSizeScale, bulletSizeScale, bulletSizeScale);
        spawnedBullet.GetComponent<Rigidbody>().AddForce(ray.direction * 20f * bulletSpeed, ForceMode.Impulse);
        spawnedBullet.GetComponent<PlayerBullet>().bulletDamage = bulletDamage;
    }

    IEnumerator Shoot()
    {
        for (int i = 0; i < bulletAmount; i++)
        {
            ShootBullet();

            yield return new WaitForSeconds(fireRate);
        }

        yield return new WaitForSeconds(shotsCooldown);
        UIManager.instance.reloadCircle.fillAmount = 1;
        shootLock = false;
    }
}

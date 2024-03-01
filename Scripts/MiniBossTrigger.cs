using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossTrigger : MonoBehaviour
{
    public static MiniBossTrigger instance;
    bool[] onlyOnce = new bool[10];
    bool canSummon = false;
    public GameObject miniBoss;
    public Transform miniBossSpawn;

    public SuperTextMesh AnnouncementUI;
    public GameObject winUi;
    public AudioSource wolfHowl;


    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (PlayerStats.instance.elapsedTime >= 600f && !onlyOnce[0])
        {
            onlyOnce[0] = true;
            canSummon = true;
            AnnouncementUI.gameObject.SetActive(true);
        }

        if (canSummon && !onlyOnce[1] && Input.GetKeyDown(KeyCode.G))
        {
            onlyOnce[1] = true;
            Instantiate(miniBoss, miniBossSpawn.position, Quaternion.identity);
            AnnouncementUI.gameObject.SetActive(false);
            wolfHowl.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float destroyTime;
    private void Start()
    {
        StartCoroutine(selfDestroy());
    }

    private void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform);
    }

    IEnumerator selfDestroy()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}

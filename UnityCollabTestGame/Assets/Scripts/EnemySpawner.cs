using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject enemy;

    [SerializeField] private float cooldown = 2f;

    private bool isSpawnerReady = true;

    // Update is called once per frame
    void Update()
    {
        if (!isSpawnerReady)
        {
            return;
        }
        Instantiate(enemy, transform.position, transform.rotation);
        StartCoroutine(StartCoolDown());
    }

    private IEnumerator StartCoolDown()
    {
        isSpawnerReady = false;
        yield return new WaitForSeconds(cooldown);
        isSpawnerReady = true;
    }
}

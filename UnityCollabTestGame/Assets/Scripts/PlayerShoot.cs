using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float cooldown = 0.2f;
    private bool isBulletReady = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!isBulletReady)
        {
            return;
        }
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        isBulletReady = false;
        yield return new WaitForSeconds(cooldown);
        isBulletReady = true;
    }
}

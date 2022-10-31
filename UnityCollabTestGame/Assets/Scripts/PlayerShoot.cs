using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    // Default values
    [SerializeField] private float fireRate = 5f;
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private int magazineSize = 10;

    private int bulletsLeft;
    private bool isBulletReady = true;
    private bool isReloading = false;

    private void Start()
    {
        bulletsLeft = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(bulletsLeft);
        if((bulletsLeft == 0 || Input.GetKey(KeyCode.R)) && !isReloading)
        {
            Reload();
        }

        if (Input.GetKey(KeyCode.Mouse0) && !isReloading)
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
        bulletsLeft--;
        StartCoroutine(bulletCooldown());
    }

    private void Reload()
    {
        StartCoroutine(reloadCooldown());
        bulletsLeft = magazineSize;
    }

    private IEnumerator bulletCooldown()
    {
        isBulletReady = false;
        yield return new WaitForSeconds(1/fireRate);
        isBulletReady = true;
    }

    private IEnumerator reloadCooldown()
    {
        isReloading = true;
        isBulletReady = false;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        isBulletReady = true;
    }
}

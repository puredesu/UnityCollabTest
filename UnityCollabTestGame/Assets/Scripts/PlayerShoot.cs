using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI ammoText;

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
        if((bulletsLeft == 0 || Input.GetKey(KeyCode.R))) Reload();

        if (Input.GetKey(KeyCode.Mouse0)) Shoot();

        ammoText.text = "Ammo: " + bulletsLeft + "/" + magazineSize;
        if (isReloading) ammoText.text += "\nreloading...";
    }

    private void Shoot()
    {
        if (isBulletReady && !isReloading)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            StartCoroutine(bulletCooldown());
        }
    }

    private void Reload()
    {
        if (!isReloading) StartCoroutine(reloadCooldown());
    }

    private IEnumerator bulletCooldown()
    {
        isBulletReady = false;
        bulletsLeft--;
        yield return new WaitForSeconds(1/fireRate);
        isBulletReady = true;
    }

    private IEnumerator reloadCooldown()
    {
        isReloading = true;
        isBulletReady = false;
        bulletsLeft = 0;
        yield return new WaitForSeconds(reloadTime);
        bulletsLeft = magazineSize;
        isReloading = false;
        isBulletReady = true;
    }
}

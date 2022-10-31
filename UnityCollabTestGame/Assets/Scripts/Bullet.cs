using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject bulletHitEffect;
    [SerializeField] private GameObject enemyHitEffect;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float lifeTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        GameObject hitEffect = Instantiate(bulletHitEffect, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(hitEffect, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            GameObject hitEffect = Instantiate(enemyHitEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(hitEffect, 0.5f);
        }
    }
}

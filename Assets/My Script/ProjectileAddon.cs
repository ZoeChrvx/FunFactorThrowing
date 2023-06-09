using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddon : MonoBehaviour
{
    public int damages;

    private Rigidbody rb;

    private bool targetHit;

    public GameObject smoke;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(targetHit)
        {
            return;
        }
        else
        {
            targetHit = true;
        }

        if(collision.gameObject.GetComponent<BasicEnemy>() != null)
        {
            smoke.SetActive(true);

            BasicEnemy enemy = collision.gameObject.GetComponent<BasicEnemy>();

            enemy.TakeDamage(damages);

            Destroy(gameObject);
        }

        rb.isKinematic = true;

        transform.SetParent(collision.transform);
    }
}

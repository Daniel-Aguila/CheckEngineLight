﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float _health;
    [Header("KnockBack")]
    [SerializeField] private float vertKnockback = 100;
    [SerializeField] private float horKnockback = 100;
    [Header("Attacked")]
    [SerializeField] private GameObject deathParticlePrefab;
    [SerializeField] private SpriteRenderer[] bodyParts;
    [SerializeField] private Color hurtColor;
    [SerializeField] private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        if (bodyParts.Length > 0)
        {
            originalColor = bodyParts[0].GetComponent<SpriteRenderer>().color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_health <= 0)
        {
            Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
            CameraShake.Instance.ShakeCamera(8f, .1f);
            Destroy(gameObject, 0.0f);
        }
    }

    public void EnemyDamage(float _damage)
    {
        // Lower enemies health
        _health -= _damage;
        // Cheange enemie's clor when hit.
        if (bodyParts.Length > 0)
        {
            StartCoroutine(flash());
        }
    }

    public void knockBack(Transform tran)
    {
        if (tran.position.x < this.transform.position.x)
        {
            this.GetComponent<Rigidbody2D>().AddForce(transform.up * vertKnockback + transform.right * horKnockback);

        }
        else
        {
            this.GetComponent<Rigidbody2D>().AddForce(transform.up * vertKnockback + (transform.right * horKnockback) * -1);
        }
    }

    IEnumerator flash()
    {

        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].color = hurtColor;
        }
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].color = originalColor;
        }
    }
}

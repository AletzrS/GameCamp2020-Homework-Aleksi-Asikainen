using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 100f;
    public float fireRate = 15f;

    public int maxAmmo = 2;
    private int currentAmmo;

    public float reloadTime = 1f;
    float currentReloadTime;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    bool readyToReload = false;

    public Animator animator;

    private void Start()
    {
        currentAmmo = maxAmmo;
        currentReloadTime = reloadTime;
    }

    private void OnEnable()
    {
        readyToReload = false;
        animator.SetBool("Reloading", false);
    }

    void Update()
    {
        if(currentAmmo <= 0)
        {
            Reload();
            return;
        }

        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Reload()
    {
        if (currentReloadTime <= 0)
        {
            readyToReload = true;
            currentReloadTime = reloadTime;
        } else
        {
            animator.SetBool("Reloading", true);
        }

        if(readyToReload)
        {
            FindObjectOfType<AudioManager>().Play("PlayerReload");
            Debug.Log("Reloaded.");
            currentAmmo = maxAmmo;
            animator.SetBool("Reloading", false);
            readyToReload = false;
        } else
        {
            currentReloadTime -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();

        // Play shooting sound.
        FindObjectOfType<AudioManager>().Play("PlayerShoot");

        currentAmmo--;

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) 
        {
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2);
        }
    }
}

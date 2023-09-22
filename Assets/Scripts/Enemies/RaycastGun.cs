using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asteroids2;
using Player;

[RequireComponent(typeof(LineRenderer))]
public class RaycastGun : MonoBehaviour
{
    public Transform laserOrign;
    public float gunRange = 50f;
    public float fireRate = 2f;
    public float laserDuration = 0.1f;

    private LineRenderer laserLine;
    private float fireTimer;
    private Transform _target;

    private void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
        
    }

    private void Update()
    {
        StartShooting();
    }

    public void StartShooting()
    {
        _target = GameObject.FindWithTag("Player").transform;
        StartCoroutine(waiter());
        Vector3 direction = _target.position - gameObject.transform.position;
        fireTimer += Time.deltaTime;
        if (fireTimer > fireRate)
        {
            fireTimer = 0;
            laserLine.SetPosition(0, laserOrign.position);
            Vector3 rayOrigin = laserOrign.position;
            RaycastHit hit;
            
            if (Physics.Raycast(rayOrigin, direction, out hit, gunRange)) 
            {
                laserLine.SetPosition(1, hit.point);
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    PlayerModel.PlayerHealth.takeDamage(100);
                }
            }
            else
            {
                laserLine.SetPosition(1,rayOrigin + (direction * gunRange));
            }

            StartCoroutine(ShootLaser());
        }
    }

    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(2f);
    }
}

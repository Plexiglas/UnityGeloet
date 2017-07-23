using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class Gun : MonoBehaviour {

    public enum GunType { Semi, Burst, Auto};
    public GunType gunType;
    public float rateOfFire;

    // Components
    public Transform spawn;
    private LineRenderer tracer;

    // System
    private float secondsBetweenShots;
    private float nextPossibleShootTime;

    private void Start()
    {
        secondsBetweenShots = 60 / rateOfFire;
        if(GetComponent<LineRenderer>())
        {
            tracer = GetComponent<LineRenderer>();
        }
    }

    public void Shoot()
    {
        if(CanShoot())
        {
            Ray ray = new Ray(spawn.position, spawn.forward);
            RaycastHit hit;

            float shotDistance = 20;

            if (Physics.Raycast(ray, out hit, shotDistance))
            {
                shotDistance = hit.distance;
            }

            nextPossibleShootTime = Time.time + secondsBetweenShots;

            GetComponent<AudioSource>().Play();
            
            if(tracer)
            {
                StartCoroutine("RenderTracer", ray.direction * shotDistance);
            }
        }
        
    }
    
    public void ShootAuto()
    {
        if(gunType == GunType.Auto)
        {
            Shoot();
        }
    }

    private bool CanShoot()
    {
        bool canShoot = true;

        if(Time.time < nextPossibleShootTime)
        {
            canShoot = false;
        }

        return canShoot;
    }

    IEnumerator RenderTracer(Vector3 targetPosition)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, spawn.position);
        tracer.SetPosition(1, spawn.position + targetPosition);

        yield return null;
        tracer.enabled = false;
    }
}

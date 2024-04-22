using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGolem : Controllable
{
    [Header("Shooting Info")]
    [SerializeField] private bool canShoot;
    [SerializeField] private bool isShootingHorizontally;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private Transform spawnPoint;

    protected override void OnAction(){
         if(canShoot){
            GameObject bullet = PoolingManager.instance.GetPooledObject(); 
            if (bullet != null) {
                bullet.transform.position = spawnPoint.position;
                bullet.SetActive(true);
             }
            canShoot = false;
            Invoke("ResetAttack", fireRate);
        }
    }
    
    private void ResetAttack()
    {
        canShoot = true;
    }
}

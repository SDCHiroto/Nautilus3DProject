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

    [Header("Materials")]
    [SerializeField] Material off; // Materiale quando il controllo è disabilitato
    [SerializeField] Material on; // Materiale quando il controllo è abilitato

    protected override void OnAction(){
         if(canShoot){
            GameObject bullet = PoolingManager.instance.GetPooledObject(); 
            if (bullet != null) {
                bullet.transform.position = spawnPoint.position;
                bullet.transform.rotation = spawnPoint.rotation;
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

    void StartControlling(){
        skinnedMeshRenderer.material = on; // Imposta il materiale della mesh quando il controllo inizia
    }

    void EndControlling(){
        skinnedMeshRenderer.material = off; // Imposta il materiale della mesh quando il controllo termina
    }
}

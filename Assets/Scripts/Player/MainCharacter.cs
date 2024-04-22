using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Controllable
{
    [Header("Slash Informations")]
    [SerializeField] float cooldownDuration = 1.0f; // Durata del cooldown in secondi
    [SerializeField] float damage = 1;
    [SerializeField] GameObject slashParticle;
    [SerializeField] Transform spawnPositionOfSlash;
    [SerializeField] float radiusOfHitbox;
    private bool canAttack = true; // Flag per indicare se è possibile attaccare
    
    protected override void OnAction(){
        if(canAttack){
            anim.SetTrigger("Slash1");
            canAttack = false;
            Invoke("ResetAttack", cooldownDuration);
        }

    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    void ManageHit()
    {
        Collider[] hitted = Physics.OverlapSphere(spawnPositionOfSlash.position, radiusOfHitbox);
        if(hitted.Length > 0)
        {
            foreach(Collider collHitted in hitted)
            {
                if(collHitted.gameObject.TryGetComponent(out IDamageable objHitted))
                {
                    objHitted.GetDamage(damage);
                }
            }
        }
    }
    new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(spawnPositionOfSlash.position, radiusOfHitbox); // Disegna una sfera di gizmo per rappresentare il raggio di attacco
    }

    private void _InstantiateSlashParticle()
    {
        if(isFacingRight)
            Instantiate(slashParticle, spawnPositionOfSlash.position, Quaternion.Euler(0f, 180f, 0f));
        else
            Instantiate(slashParticle, spawnPositionOfSlash.position, Quaternion.Euler(0f, 0f, 0f));
    }
}

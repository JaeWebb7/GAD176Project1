using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEnemy : PistolEnemy
{
    [SerializeField]
    private GameObject shotgun;

    protected override void AttackPlayer(){
    //look at player
        transform.LookAt(player);
    //only stop moving when you're even closer to the player than your maximum attack range so that the player can't instantly leave your attack range when you stop moving
        if (distanceFromPlayer <= attackRange -4){
           transform.position = transform.position;
        }
        else{
            transform.position = Vector3.Lerp(transform.position,playertest.transform.position,chaseSpeed*Time.deltaTime);
        }   
        
        if(!alreadyAttacked){
            //attack code here
            shotgun.GetComponent<EnemyShotgunScript>().Shoot();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }



}

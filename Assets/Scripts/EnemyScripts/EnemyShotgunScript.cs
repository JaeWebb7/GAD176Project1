using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotgunScript : EnemyGun
{

    public override void Shoot(){
            if(!reloading && bulletsLeft > 0){
        Debug.Log("ENEMYSHOT");

        readyToShoot = false;

        Vector3 targetPoint;
        targetPoint = player.transform.position;
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;


        for (int i = 0; i < 10; i++){
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x,y,0);
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        }
        bulletsLeft --;
        }
    }
        



}

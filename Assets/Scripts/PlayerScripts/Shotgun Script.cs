using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : GunScript
{
    protected override void Shoot(){
        
        readyToShoot = false;  

        Debug.Log("shotgun");

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        Vector3 targetPoint;
        targetPoint = ray.GetPoint(7);
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
        Invoke("ResetShot", timeBetweenShooting);

    }


}

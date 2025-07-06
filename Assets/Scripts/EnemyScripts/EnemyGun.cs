using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField]
    protected float shootForce, timeBetweenShooting, spread, reloadTime, timeBetweenShots, bulletSpeed;
    [SerializeField]
    protected int magazineSize, bulletsPerClick;
    [SerializeField]
    protected int bulletsLeft;
    public bool shooting, readyToShoot = true, reloading;
    public GameObject bullet;
    public Transform attackPoint;
    public Transform player;
    private void Inputs(){


        if (bulletsLeft <= 0){
            Reload();
        }

    }

    private void Reload(){
        reloading = true;
        Debug.Log("ENEMYRELOADING");
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished(){
        bulletsLeft = magazineSize;
        reloading = false;
    }

    public virtual void Shoot(){

        if(!reloading && bulletsLeft > 0){
        Debug.Log("ENEMYSHOT");

        readyToShoot = false;

        Vector3 targetPoint;
        targetPoint = player.transform.position;
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x,y,0);

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        bulletsLeft --;
        }
    }


    void Update()
    {
        Inputs();
    }

    void Start()
    {
        bulletsLeft = magazineSize;
    }
}

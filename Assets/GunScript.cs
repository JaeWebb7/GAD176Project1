using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public int damage;
    public float shootForce, upwardForce, timeBetweenShooting, spread, range, reloadTime, timeBetweenShots, bulletSpeed;
    public int magazineSize, bulletsPerClick;
    public Camera fpsCam;
    [SerializeField]
    protected int bulletsLeft;
    protected bool shooting, readyToShoot, reloading;
    public GameObject bullet;
    public Transform attackPoint;
    private void Inputs(){

        shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading){
            Reload();
        }

        if(readyToShoot && shooting && !reloading && bulletsLeft <= 0){
            Reload();
        }


        if (readyToShoot && shooting && !reloading && bulletsLeft > 0){
            Shoot();
        }



    }

    private void Reload(){
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished(){
        bulletsLeft = magazineSize;
        reloading = false;
    }

    protected virtual void Shoot(){

        Debug.Log("BANG");

        readyToShoot = false;

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        Vector3 targetPoint;
        targetPoint = ray.GetPoint(7);
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x,y,0);

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        bulletsLeft --;
        Invoke("ResetShot", timeBetweenShooting);
    }

    private void ResetShot(){
        readyToShoot = true;
    }
    void Update()
    {
        Inputs();
    }

    void Start()
    {
        readyToShoot = true;
        bulletsLeft = magazineSize;
    }
}

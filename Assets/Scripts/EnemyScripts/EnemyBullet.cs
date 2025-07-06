using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag != "enemy"){
            Destroy(gameObject);
        }
        if(collision.collider.tag == "player"){
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        Invoke("KillBullet", 2);
    }

    private void KillBullet(){
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag != "player"){
            Destroy(gameObject);
        }
        if(collision.collider.tag == "enemy"){
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{
    public int playerHealth;
    public int enemyHealth;
    private GameObject enemy;

    void Update()
    {
        if (playerHealth <=0){
            GameOver();
        }

        if (enemyHealth <=0){
            KillEnemy();
        }

    }

    void GameOver(){

    }

    void KillEnemy(){
        Destroy(enemy);
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PistolEnemy : MonoBehaviour
{

//essentials
[SerializeField]
protected int health;
protected float distanceFromPlayer;
public GameObject playertest = GameObject.Find("Player");
[SerializeField]
protected float moveSpeed = 0.01f;
[SerializeField]
protected float chaseSpeed;
[SerializeField]
protected Vector3 Targetrotation;
[SerializeField]
protected Transform targetpoint;
public Transform player;
[SerializeField]
protected int damage;
public GameObject enemyGun;


//patrolling area
protected Vector3 walkPosition;
protected bool walkPositionSet;

//attacking player
[SerializeField]
protected float timeBetweenAttacks;
protected bool alreadyAttacked;

//Ranges
[SerializeField]
protected float sightRange, attackRange;
public bool playerInSightRange, playerInAttackRange;

 private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    void Start()
    {
        chaseSpeed = 0.3f;
        moveSpeed = 0.2f;
        playertest = GameObject.Find("Player");
        player = GameObject.Find("Player").transform;
    }


    void Update(){
        //check if player is in sight range or attack range
        distanceFromPlayer = Vector3.Distance(gameObject.transform.position, playertest.transform.position);
        //Debug.Log("distance = " + distance);
        playerInSightRange = distanceFromPlayer <=sightRange;
        playerInAttackRange = distanceFromPlayer <=attackRange;
        if (!playerInSightRange && !playerInAttackRange){
            Patrol();
        }
        if (playerInSightRange && !playerInAttackRange){
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange){
            AttackPlayer();
        }   
    
    }
    protected void Patrol(){

    //find walk position
        if (!walkPositionSet){
            Debug.Log("searching walk position");
            findWalkPosition();
        }

        if (walkPositionSet == true){
        //check the distance from the object to the target position
            float distanceToWalkPosition = Vector3.Distance(gameObject.transform.position, walkPosition);

        //rotate towards the position then move to the position
            transform.LookAt(targetpoint);
            transform.position = Vector3.Lerp(transform.position,walkPosition,moveSpeed*Time.deltaTime);

        //don't leave the bounds of the level
            if (transform.position.x <= -12f || transform.position.x >= 12f|| transform.position.z <= -12f || transform.position.z >= 12f){
                transform.position = Vector3.Lerp(transform.position,new Vector3(0,0,0),moveSpeed*Time.deltaTime);
                walkPositionSet = false;
            }

            if (distanceToWalkPosition < 5f){
                walkPositionSet = false;
            }

        }

    }
    protected void findWalkPosition(){
        float randomZ = Random.Range(-12.5f,12.5f);
        float randomX = Random.Range(-12.5f,12.5f);
        //Debug.Log("set walk position = " + walkPosition);
        walkPosition = new Vector3(0 + randomX, transform.position.y, 0 + randomZ);
        targetpoint.transform.position = walkPosition;
        walkPositionSet = true;
    }

    protected void ChasePlayer(){
        transform.position = Vector3.Lerp(transform.position,playertest.transform.position,chaseSpeed*Time.deltaTime);
        transform.LookAt(player);
    }

    protected virtual void AttackPlayer(){
    //look at player
        transform.LookAt(player);
    //only stop moving when you're even closer to the player than your maximum attack range so that the player can't instantly leave your attack range when you stop moving
        if (distanceFromPlayer <= attackRange -4){
           transform.position = transform.position;
        }
        else{
            transform.position = Vector3.Lerp(transform.position,playertest.transform.position,chaseSpeed*Time.deltaTime);
        }   

        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(playertest.transform.position - transform.position), rotationSpeed * Time.deltaTime); Don't know why this doesn't work...
        
        if(!alreadyAttacked){
            //attack code here
            enemyGun.GetComponent<EnemyGun>().Shoot();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    protected void ResetAttack(){
        alreadyAttacked = false;
    }
    public void takedamage(){
        
    }
}


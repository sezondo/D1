using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveFollow : MonoBehaviour
{

    Transform p;
    NavMeshAgent nav;
    public EnemyHP EnemyContoroller;
    public EnemyAttack EnemyAttack;
    public bool enemyNavEn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        p = GameObject.Find("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (p != null && nav.enabled && !EnemyContoroller.EnemyDie && !EnemyAttack.navStop)
        {
            enemyNavEn = true;
            nav.SetDestination(p.position);
        }
        else
        {
            enemyNavEn = false;
        }
        
    }
}


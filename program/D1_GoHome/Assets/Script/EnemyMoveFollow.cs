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
    private float chaseRange = 10f;
    private bool rangeTPG = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        p = GameObject.Find("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }

    

    // Update is called once per frame
    void Update()
    {
        float range = Vector3.Distance(transform.position,p.transform.position);

        if (range < chaseRange)
        {
            rangeTPG= true;
        }
        else rangeTPG = false;

        if (p != null && nav.enabled && !EnemyContoroller.EnemyDie && !EnemyAttack.navStop && rangeTPG)
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


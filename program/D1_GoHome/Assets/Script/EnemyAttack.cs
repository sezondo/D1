using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;
    public int attackDamage = 10;
    public bool AttackTRG = false;
    public GameObject player;
    private float lastAttackTime = 0;
    public bool enemyAttackAni = false;
    public bool navStop = false;
    private DelayTimer attackDelay = new DelayTimer();
    public PlayerContoroller playerContoroller;
    public EnemyHP enemyHP;
    public AudioClip attackSound;
    public AudioSource attackSoundSource;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (player == null) return;
        enemyAttackAni = false;
        
        float dist = Vector3.Distance(transform.position, player.transform.position);

        if (Time.time - lastAttackTime >= attackCooldown && playerContoroller.playerDie == false && !enemyHP.EnemyDie)
        {
            navStop = false;
            if (dist <= attackRange)
            {
                Attack();
                lastAttackTime = Time.time;
            }
            
        }

    }

    void Attack()
    {   
        Debug.Log("적이 나를 공격함");
        navStop = true;
        enemyAttackAni = true;
        attackSoundSource.PlayOneShot(attackSound); 

        StartCoroutine(DelayedDamage(0.5f));

        /*var playerController = player.GetComponent<PlayerContoroller>();
        if (playerController != null)
        {
            
            playerController.TakeDamage(attackDamage);
            
           // transform.LookAt(player.transform); // 시1234발럼 버그 개123시1발
            
        }
        */
         
    }
    private IEnumerator DelayedDamage(float delay)
    {
        yield return new WaitForSeconds(delay);

        var playerController = player.GetComponent<PlayerContoroller>();
        if (playerController != null)
        {
            playerController.TakeDamage(attackDamage);
        }
    }


}

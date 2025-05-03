using UnityEngine;

public class EnemyAniController : MonoBehaviour
{
    public Animator EnemyAnim;

    public EnemyHP EnemyContoroller;
    public EnemyAttack EnemyAttack;
    public EnemyMoveFollow EnemyMoveFollow;
    bool save = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //EnemyAnim.SetBool("enemydie",EnemyContoroller.EnemyDie);

        if (EnemyContoroller.EnemyDie && !save)
        {
            EnemyAnim.SetTrigger("enemydie");
            save = true;
        }
        //else EnemyAnim.ResetTrigger("enemydie");

        EnemyAnim.SetBool("enemyattack",EnemyAttack.enemyAttackAni);

        EnemyAnim.SetBool("enemyrun",EnemyMoveFollow.enemyNavEn);

    }
}

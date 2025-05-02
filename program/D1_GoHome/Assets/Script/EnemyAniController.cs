using UnityEngine;

public class EnemyAniController : MonoBehaviour
{
    public Animator EnemyAnim;

    public EnemyHP EnemyContoroller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyContoroller.EnemyDie)
        {
            EnemyAnim.SetTrigger("enemydie");
        }
    }
}

using UnityEngine;
using UnityEngine.Android;

public class AniController : MonoBehaviour
{

    public Animator playerAnim;

    public PlayerContoroller PlayerContoroller;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 애니메이션션
        if (PlayerContoroller.jumpTrg)
        {
            playerAnim.SetTrigger("jump");
        }
        else playerAnim.ResetTrigger("jump");
        
        
        playerAnim.SetBool("stamding",PlayerContoroller.landing);
        
        
        playerAnim.SetBool("forward",PlayerContoroller.playerRunEnable);

        
        playerAnim.SetBool("fall",PlayerContoroller.falling);
        
        if (PlayerContoroller.AttackTRGAni)
        {
            playerAnim.SetTrigger("attack");
        }
        else playerAnim.ResetTrigger("attack");

        playerAnim.SetBool("attackReset",PlayerContoroller.AttackTRGAni);
        
        
        



        

        
        

        

        
    }

    
}

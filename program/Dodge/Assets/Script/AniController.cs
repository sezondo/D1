using UnityEngine;

public class AniController : MonoBehaviour
{

    public Animator anim;
    public PlayerContoroller PlayerContoroller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (PlayerContoroller.jumpTrg)
        {
            anim.SetTrigger("jump");
        }
        else anim.ResetTrigger("jump");
        
        
        anim.SetBool("stamding",PlayerContoroller.landing);
        
        
        anim.SetBool("forward",PlayerContoroller.playerRunEnable);

        
        anim.SetBool("fall",PlayerContoroller.falling);
        
        

        

        
    }

    
}

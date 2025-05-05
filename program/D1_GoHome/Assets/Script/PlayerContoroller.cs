using TMPro;
using Unity.Mathematics;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XInput;
using UnityEngine.UI;

public class PlayerContoroller : MonoBehaviour
{

    public VariableJoystick j;
    private Rigidbody playerRigidbody;
    public float speed = 5f;
    public float jumpForce = 20f;
    public bool isGrounded = true; //on시 땅에 닿아있음음
    public bool playerRunEnable = false;
    public bool jumpTrg = false;
    public float delayTimeSet = 0.5f;
    public bool qTrigger = false;
    public float rotationSpeed = 5f;
    public bool landing = false;
    public bool falling = false;
    private DelayTimer jumpDelayTimer = new DelayTimer();
    public AudioClip dieSound;
    public AudioSource dieSoundSource;
    public GameObject dieEffectPrefab;
    public AudioClip jumpSound;
    public AudioSource jumpSoundSource;
    private ClearZone gameClaerSvae;
    public bool AttackTRG = false;
    public bool AttackTRGAni = false;
    //private DelayTimer AttackyTimer = new DelayTimer();
    private float attackRange = 2f;
    private int damage = 60;
    private float attackCooldown = 1.5f;
    private LayerMask enemyLayer;
    private float lastAttackTime = 0;
    public int maxHP = 100;
    public int currentHP;
    public Slider hpSlider;
    public bool playerDie = false;

    public AudioClip attackSound;
    public AudioSource attackSoundSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();  
        gameClaerSvae = FindFirstObjectByType<ClearZone>();
        enemyLayer = LayerMask.GetMask("enemyLayer");
        
        currentHP = maxHP;
        hpSlider.maxValue = maxHP;
        hpSlider.value = currentHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (AttackTRGAni)
        {
            AttackTRGAni = false;
        }

        if (AttackTRG && Time.time - lastAttackTime >= attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }


        //float xInput = Input.GetAxis("Horizontal");
        //float zInput = Input.GetAxis("Vertical");

        float xInput = j.Horizontal;
        float zInput = j.Vertical;

        
        
        Vector3 newVeiocity = new Vector3(xInput, 0f, zInput).normalized;
        
        // 현재 y속도 유지 + xz 속도 덮어쓰기
        Vector3 targetVelocity = newVeiocity * speed;

        if (!gameClaerSvae.gameClear){
            playerRigidbody.linearVelocity = new Vector3(
                targetVelocity.x,
                playerRigidbody.linearVelocity.y,
                targetVelocity.z
        );
        }

        // 애니메이션 작업

        if (playerRigidbody.linearVelocity.y < -0.1f) 
        {
            falling = true;
            landing = false;
        }
        else if (playerRigidbody.linearVelocity.y > 0.1f)
        {
            falling = false;
            landing = false;
        }else{
            falling = false;
            landing = true;
        }
        if (newVeiocity.sqrMagnitude > 0.01f) 
        {
            playerRunEnable = true;
        }else{
            playerRunEnable = false;
        }

        

        if (newVeiocity.sqrMagnitude > 0.2f)
        {
            Quaternion toRotation = Quaternion.LookRotation(newVeiocity, Vector3.up);// 타겟 방향 설정
            
            transform.rotation = Quaternion.Slerp( // Quaternion 함수는 유니티에서 물체를 회전시킬때 쓰는 함수
            transform.rotation, // 현재 회전 상태
            toRotation,  // 목표 회전
            rotationSpeed * Time.deltaTime);//한번에 얼마나 회전할꺼냐?
        }

        


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !qTrigger) // 자꾸 점프 씹혀서 여기다가 점프 트리거 작업업
        {
            
            jumpTrg = true;
            qTrigger = true;
            
        }
        
        if (qTrigger && !gameClaerSvae.gameClear)// 점프 딜레이
        {
             if (jumpDelayTimer.Run(delayTimeSet))
            {
                qTrigger = false;
            }
        }
        

    }

    void FixedUpdate()
    {
        if (jumpTrg)      // 점프 구현
        {
            jumpSoundSource.PlayOneShot(jumpSound); // 점프 소리나게 하기기

            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpTrg = false;
        }
    }
    public void JumpButton()
    {
        if (isGrounded && !qTrigger)
        {
            qTrigger = true;
            jumpTrg = true;
        }
        
    }

    public void AttackButton(){

        AttackTRG = true;
        
    }

    void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.tag == "Wall")
        {
            isGrounded = true;
        } 
    }

    public void TakeDamage(int damage){
        currentHP -= damage;

        if (hpSlider != null)
        {
        hpSlider.value = currentHP;
        }

        Debug.Log("대미지받음");
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }
    
    public void playerDieSound(){
        GameObject soundObj = new GameObject("DieSound");
        AudioSource audio = soundObj.AddComponent<AudioSource>();
        audio.clip = dieSound;
        audio.Play();
        Destroy(soundObj, dieSound.length);
        GameObject playerDieeffect =  Instantiate(dieEffectPrefab, transform.position, transform.rotation);
        Destroy(playerDieeffect, 0.3f);
    }

    public void Die(){
        
        playerDie = true;
        playerDieSound();

        gameObject.SetActive(false);

        GameManager gameManager = FindFirstObjectByType<GameManager>();
        gameManager.EndGame();
    }

    public void Attack(){

        AttackTRGAni = true;
        AttackTRG = false;
        Vector3 attackOrigin = transform.position + transform.forward *1.2f;

        attackSoundSource.PlayOneShot(attackSound); 

        Collider[] hitEnemies = Physics.OverlapSphere(attackOrigin, attackRange, enemyLayer); //공격범위 반원 생성 attackOrigin 부터  attackRange 까지enemyLayer놈을 가져와라라

        foreach (Collider enemy in hitEnemies){ // 가져온 배열중 있으면 enemy라는 이름을 부여해서 뚜시뚜시
            Debug.Log("Hit: " + enemy.name);
            enemy.GetComponent<EnemyHP>()?.TakeDamage(damage);
        }

    }

    
}

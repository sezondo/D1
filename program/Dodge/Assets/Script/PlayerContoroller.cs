using TMPro;
using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XInput;

public class PlayerContoroller : MonoBehaviour
{

    public VariableJoystick j;

    private Rigidbody playerRigidbody;
    public float speed = 5f;
    public float acceLeration = 15f;
    public float jumpForce = 20f;
    private bool isGrounded = true;
    
    public bool jumpTrg = false;
    public float rotationSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Vector3 currentVelocity;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();  
       
    }

    // Update is called once per frame
    void Update()
    {
        
        
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        //float xInput = j.Horizontal;
        //float zInput = j.Vertical;

        

        Vector3 newVeiocity = new Vector3(xInput, 0f, zInput).normalized;

        // 현재 y속도 유지 + xz 속도 덮어쓰기
        Vector3 targetVelocity = newVeiocity * speed;
        playerRigidbody.linearVelocity = new Vector3(
            targetVelocity.x,
            playerRigidbody.linearVelocity.y,
            targetVelocity.z
        );

        

        if (newVeiocity.sqrMagnitude > 0.01f)
        {
            Quaternion toRotation = Quaternion.LookRotation(newVeiocity, Vector3.up);// 타겟 방향 설정
            
            transform.rotation = Quaternion.Slerp( // Quaternion 함수는 유니티에서 물체를 회전시킬때 쓰는 함수
            transform.rotation, // 현재 회전 상태
            toRotation,  // 목표 회전
            rotationSpeed * Time.deltaTime);//한번에 얼마나 회전할꺼냐?
        }


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // 여기부터 작업
            jumpTrg = true;
        }
        
        

    }

    void FixedUpdate()
    {
        if (jumpTrg)      // 점프 구현
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpTrg = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.tag == "GameController")
        {
            isGrounded = true;
        } 
    }

    

    public void Die(){
        gameObject.SetActive(false);

        GameManager gameManager = FindFirstObjectByType<GameManager>();

        gameManager.EndGame();
    }

}

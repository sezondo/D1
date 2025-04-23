using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerContoroller : MonoBehaviour
{

    private Rigidbody playerRigidbody;
    public float speed = 8f;
    public float acceLeration = 15f;

    private Vector3 targetVelocity; 
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

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        Vector3 newVeiocity = new Vector3(xSpeed, 0f, zSpeed).normalized; //방향
        Vector3 targetVelocity = newVeiocity * speed; // 타겟 속도

        currentVelocity = Vector3.MoveTowards( // 현재속도 >> 타겟속도 점차 증가 (현재속도, 타겟속도, 증가량)
            currentVelocity,
            targetVelocity,
            acceLeration * Time.fixedDeltaTime// 200sec DT 속도가 8이니니 프레임당당 대충 0.16움직임

        );
    playerRigidbody.linearVelocity = currentVelocity;


/*
        Vector3 newVeiocity = new Vector3(xSpeed, 0f, zSpeed);
        Vector3 adjustedVelocity = newVeiocity.normalized * speed;

        playerRigidbody.linearVelocity = adjustedVelocity;
        playerRigidbody.linearDamping = 3f;
*/
    }

    
    public void Die(){
        gameObject.SetActive(false);

        GameManager gameManager = FindFirstObjectByType<GameManager>();

        gameManager.EndGame();
    }

}

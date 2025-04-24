using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    
    
    public Vector3 offset;
    public float followSpeed = 5f;
    public float followThreshold = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        offset = transform.position - player.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    
    void FixedUpdate()
    {
        Vector3 targetPos = player.position + offset;
        float distance = Vector3.Distance(transform.position, targetPos);
        
        if (distance > followThreshold)
        {
            transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            followSpeed * Time.deltaTime
        );
        }
        
    

        //transform.position = player.position + offset;

    }

}

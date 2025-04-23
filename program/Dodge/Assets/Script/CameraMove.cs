using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    
    public Vector3 offset;
    public float followSpeed = 5f;

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
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            player.position + offset,
            followSpeed * Time.deltaTime
        );


        //transform.position = player.position + offset;

    }

}

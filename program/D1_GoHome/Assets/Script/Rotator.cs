using UnityEngine;

public class Rotator : MonoBehaviour
{

    public float speed = 60;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(0f, speed * Time.deltaTime, 0f);
    }
}

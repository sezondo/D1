using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 8f;
    public GameObject effectPrefab;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;

        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerContoroller playerContoroller = other.GetComponent<PlayerContoroller>();
            
            if (playerContoroller != null)
            {
                playerContoroller.Die();
                Instantiate(effectPrefab, transform.position, transform.rotation);
            }
        }        
    }

}

using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 8f;
    public GameObject effectPrefab;
    
    private Rigidbody rb;
    public AudioClip effectSound;
    public AudioSource effectSoundSource;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;
       
        Destroy(gameObject, 10f);
    }

    public void bulletLastSound(){
        GameObject soundObj = new GameObject("DieSound");
        AudioSource audio = soundObj.AddComponent<AudioSource>();
        audio.clip = effectSound;
        audio.Play();
        Destroy(soundObj, effectSound.length);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerContoroller playerContoroller = other.GetComponent<PlayerContoroller>();
            
            if (playerContoroller != null)
            {
                playerContoroller.Die();
                Destroy(gameObject,0.01f);
            }
        }

        if (other.tag == "Wall")
        { 
            bulletLastSound();
            effectSoundSource.PlayOneShot(effectSound);
            GameObject effect = Instantiate(effectPrefab, transform.position, transform.rotation);
            Destroy(effect, 0.3f);
            Destroy(gameObject,0.01f);
        }
    }      
    

}

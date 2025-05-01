using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public Transform currentTransform;
    public GameObject prefab;
    public float min = 0.5f;
    public float max = 3f;
    public float rotationalSpeed = 2f;
    public float fireRange = 25;
    private Transform target;
    private float spwnRate;
    private float timeAfterSpawn;

    public GameObject firePrefab;
    public AudioClip fireSound;
    public AudioSource fireSoundSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeAfterSpawn = 0f;
        spwnRate = Random.Range(min, max);
        target = FindFirstObjectByType<PlayerContoroller>().transform;
    }

    


    // Update is called once per frame
    void Update()
    {
        timeAfterSpawn += Time.deltaTime;

        Vector3 dir = target.position - currentTransform.position;
        float distance = dir.magnitude;
        dir.y = 0;

        if(distance <= fireRange && dir != Vector3.zero){
            Quaternion targetRot = Quaternion.LookRotation(dir);
            currentTransform.rotation = Quaternion.Lerp(currentTransform.rotation, targetRot, Time.deltaTime * rotationalSpeed);
        }

        if (distance <= fireRange && timeAfterSpawn >= spwnRate)
        {
            GameObject effect = Instantiate(firePrefab, transform.position, transform.rotation);
            Destroy(effect, 0.3f);
            fireSoundSource.PlayOneShot(fireSound); 


            timeAfterSpawn = 0f;
            GameObject bullet = Instantiate(prefab, currentTransform.position, currentTransform.rotation);
            //bullet.transform.LookAt(target);

            spwnRate = Random.Range(min,max);
        }
    }
}

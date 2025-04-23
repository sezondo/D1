using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float min = 0.5f;
    public float max = 3f;


    private Transform target;
    private float spwnRate;
    private float timeAfterSpawn;


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

        if (timeAfterSpawn >= spwnRate)
        {
            timeAfterSpawn = 0f;
            GameObject bullet = Instantiate(prefab, transform.position, transform.rotation);
            bullet.transform.LookAt(target);

            spwnRate = Random.Range(min,max);
        }
    }
}

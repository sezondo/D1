using UnityEngine;
public class ClearZone : MonoBehaviour
{
    public bool gameClear = false;

    public GameObject congratulationsPrefab;
   void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject effect = Instantiate(congratulationsPrefab, transform.position, transform.rotation);
            Destroy(effect, 10f);
            
            gameClear = true;
                
            
        }
    }
}

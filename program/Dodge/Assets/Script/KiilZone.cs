using UnityEngine;

public class KiilZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerContoroller playerContoroller = other.GetComponent<PlayerContoroller>();
            
            if (playerContoroller != null)
            {
                playerContoroller.Die();
            }
        }
    }
}

using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;
    public AudioClip dieSound;
    public AudioSource dieSoundSource;
    public bool EnemyDie = false;

    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"{gameObject.name} 피격! 남은 HP: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void EnemyDieSound(){
        GameObject soundObj = new GameObject("DieSound");
        AudioSource audio = soundObj.AddComponent<AudioSource>();
        audio.clip = dieSound;
        audio.Play();
        Destroy(soundObj, dieSound.length);
    }

    void Die()
    {
        EnemyDie = true;
        EnemyDieSound();
        Debug.Log($"{gameObject.name} 사망!");
        Destroy(gameObject, 5f);
    }


}

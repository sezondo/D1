using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class GameManager : MonoBehaviour
{

    public GameObject gameoverText;
    public GameObject gameClearText;
    public GameObject reStartButton;

    public Text timeText;
    public Text textRecord;

    private ClearZone gameClaerSvae;

    float surviveTime;
    bool isGameover;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        surviveTime =0f;
        isGameover = false;
        gameClaerSvae = FindFirstObjectByType<ClearZone>();
    }

    public void Retry(){
        SceneManager.LoadScene("MainScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameover)
        {
            surviveTime += Time.deltaTime;
            timeText.text = "Time: "+(int) surviveTime;
        }else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }

        if (gameClaerSvae.gameClear)
        {
            GameClaer();
        }


    }
    public void EndGame(){
        gameoverText.SetActive(true);
        reStartButton.SetActive(true);
        
    }
    public void GameClaer(){

        isGameover = true;
        gameClearText.SetActive(true);
        reStartButton.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("BestTime",0);

        if (bestTime == 0f || surviveTime < bestTime)
        {
            bestTime = surviveTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }
        textRecord.text = "Best Time: " + (int)bestTime;

    }
    void Awake()
    {
	    Application.targetFrameRate = 60;
    }
    
}

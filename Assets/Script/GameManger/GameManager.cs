using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pointsCube;
    [SerializeField] TMP_Text pointsCubeText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] public GameObject gameOverUI;


    float remainingTime = 120f;//2mins
    const string text = "Remaining Cubes :";
    int count;
    public bool isGameActive = true;

    private void Start()
    {
        gameOverUI.SetActive(false);
       
    }

    private void Update()
    {
        AdjustPointsCube();
        AdjustTimer();
    }

    void AdjustPointsCube()
    {
         count = pointsCube.transform.childCount;
        pointsCubeText.text = text + count.ToString();
    }

    void AdjustTimer()
    {
        if (count == 0 && remainingTime > 0) // Win condition
        {
            isGameActive = false;
            Debug.LogWarning("You win!");
            return; // Stop updating the timer
        }

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0)
        {
            isGameActive = false;
            remainingTime = 0;
           
            gameOverUI.SetActive(true);
            return; // Stop updating further
        }

        // Update UI Timer
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = $"Time: {minutes:00}:{seconds:00}";


    }


    public void OnRestart()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);


    }

    public void OnQuit()
    {
        Application.Quit();
    }
}

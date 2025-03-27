using UnityEngine;

public class DeathCube : CubeBase
{
    GameManager gameManager;
    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    public override void OnPlayerTrigger(GameObject other)
    {
        Destroy(other.gameObject);
        gameManager.gameOverUI.SetActive(true);
        gameManager.isGameActive = false;
    }
}

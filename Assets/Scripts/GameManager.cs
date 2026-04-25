using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TMP_Text coinText;
    [SerializeField] private PlayerController playerController;

    private int coinCount = 0;
    private int gemCount = 0;
    private Vector3 playerPosition;

    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;

    private bool isGameOver = false;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        UpdateGUI();
        playerPosition = playerController.transform.position;

        // ✅ IMPORTANT: Hide panels at start
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);
    }

    // ✅ COINS
    public void IncrementCoinCount()
    {
        coinCount++;
        UpdateGUI();
    }

    // ✅ GEMS
    public void IncrementGemCount()
    {
        gemCount++;
        Debug.Log("Gem collected: " + gemCount);
    }

    private void UpdateGUI()
    {
        if (coinText != null)
            coinText.text = coinCount.ToString();
    }

    // ✅ RESPAWN
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        playerController.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        playerController.transform.position = playerPosition;
        playerController.gameObject.SetActive(true);
    }

    // ✅ GAME OVER (MAIN PART)
    public void GameOver()
    {
        Debug.Log("GAME OVER CALLED");

        isGameOver = true;

        playerController.gameObject.SetActive(false);

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Debug.Log("GameOver Panel Activated");
        }
        else
        {
            Debug.LogError("GameOver Panel NOT assigned!");
        }
    }

    // ✅ LEVEL COMPLETE
    public void LevelComplete()
    {
        playerController.gameObject.SetActive(false);

        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(true);

        Debug.Log("LEVEL COMPLETE");
    }

    // ✅ RESTART BUTTON
    public void RestartGame()
    {
        HealthManager.instance.ResetLives();
        SceneManager.LoadScene(0);
    }
}
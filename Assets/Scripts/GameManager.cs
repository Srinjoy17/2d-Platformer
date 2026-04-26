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

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        UpdateGUI();
        playerPosition = playerController.transform.position;
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

    // ✅ RESPAWN (for lives system before final death)
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

    // ✅ GAME OVER → LOAD GAME OVER SCENE
    public void GameOver()
    {
        Debug.Log("GAME OVER");

        StartCoroutine(LoadGameOverScene());
    }

    private IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(1f);

        // reset lives before going to next scene
        if (HealthManager.instance != null)
            HealthManager.instance.ResetLives();

        SceneManager.LoadScene("GameOverScene");
    }

    // ✅ LEVEL COMPLETE → LOAD WIN SCENE
    public void LevelComplete()
    {
        Debug.Log("LEVEL COMPLETE");

        StartCoroutine(LoadWinScene());
    }

    private IEnumerator LoadWinScene()
    {
        yield return new WaitForSeconds(1f);

        // reset lives
        if (HealthManager.instance != null)
            HealthManager.instance.ResetLives();

        SceneManager.LoadScene("WinScene");
    }
}
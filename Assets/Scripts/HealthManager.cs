using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    [Header("Lives System")]
    public int maxLives = 3;
    public int currentLives;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite FullHeartSprite;
    [SerializeField] private Sprite EmptyHeartSprite;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentLives = maxLives;
        UpdateHearts();
    }

    public void PlayerDied()
    {
        currentLives--;

        UpdateHearts();
        if (currentLives <= 0)
        {
            GameManager.instance.GameOver();
        }
        else
        {
            GameManager.instance.RespawnPlayer();
        }
    }

    public void ResetLives()
    {
        currentLives = maxLives;
        UpdateHearts();
    }

    // 🔁 Compatibility (for old scripts)
    public void HurtPlayer(int damage = 1)
    {
        PlayerDied();
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLives)
                hearts[i].sprite = FullHeartSprite;
            else
                hearts[i].sprite = EmptyHeartSprite;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public static UIController instance;

    private void Awake() {
        instance = this;
        GameManager.OnCollectiblePickedUp += UpdateCollectibleScore;
        GameManager.OnPlayerDamaged += UpdateHealthBar;
    }

    private void OnDestroy()
    {
        GameManager.OnPlayerDamaged += UpdateHealthBar;
        GameManager.OnCollectiblePickedUp -= UpdateCollectibleScore;
    }

    public Image fadeScreen;
    private bool isFadingToBlack, isFadingFromBlack;
    public float fadeSpeed = 1f;

    public Slider healthSlider;
    public TMP_Text healthText, timeText;
    public TMP_Text dieText;
    public TMP_Text coinText;

    public GameObject pauseScreen;
    public GameObject settingsScreen;

    public string mainMenu, levelSelect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (isFadingToBlack) {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 5f, fadeSpeed * Time.deltaTime));
            dieText.color = new Color(255, 255, 255,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        }
        if (isFadingFromBlack) {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            dieText.color = new Color(255, 255, 255,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseUnpause();
        }
    }

    public void FadeToBlack() {
        isFadingToBlack = true;
        isFadingFromBlack = false;
    }
    public void FadeFromBlack() {
        isFadingToBlack = false;
        isFadingFromBlack = true;
    }

    /*public void UpdateHealthDisplay(int health)
    {
        healthText.text = "Health: " + health + "/" + PlayerHealthController.instance.maxHealth;

        healthSlider.maxValue = PlayerHealthController.instance.maxHealth;
        healthSlider.value = health;
    }*/

    private void UpdateCollectibleScore()
    {
        coinText.text = GameManager.Instance.GetCollectibleCount().ToString();
    }
    private void UpdateHealthBar(int currentHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    public void PauseUnpause() {
        pauseScreen.SetActive(!pauseScreen.activeSelf);
        if (pauseScreen.activeSelf) {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }


    public void GoToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenu);
    }
    public void GoToSettingsMenu() {
        Time.timeScale = 0;
        settingsScreen.SetActive(!settingsScreen.activeSelf);
        pauseScreen.SetActive(!pauseScreen.activeSelf);


    }
    public void GoToLevelSelect() {
        Time.timeScale = 1;

        SceneManager.LoadScene(levelSelect);
    }

    public void QuitGame() {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
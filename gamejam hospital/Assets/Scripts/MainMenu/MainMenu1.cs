using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu1 : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;
    public GameObject controlsPanel; // Controls panel

    [Header("Settings UI")]
    public Slider soundSlider;          // single slider controlling all audio
    public AudioSource musicSource;     // background music
    public AudioSource buttonClickSFX; // example SFX

    [HideInInspector]
    public float soundVolume = 1f;

    void Start()
    {
        ShowMainMenu();

        // Load saved sound volume
        if (soundSlider != null)
        {
            soundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);
            soundSlider.value = soundVolume;
            UpdateSoundVolume();
        }

        // Hook slider to real-time update
        if (soundSlider != null)
            soundSlider.onValueChanged.AddListener(delegate { UpdateSoundVolume(); });
    }

    #region Button Handlers

    public void StartGame()
    {
        PlayClick();
        SceneManager.LoadScene("WakeUp");
    }

    public void OpenSettings()
    {
        PlayClick();

        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        creditsPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        PlayClick();

        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void OpenControls()
    {
        PlayClick();

        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        PlayClick();

        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        PlayClick();
        Debug.Log("Quit Game");
        Application.Quit();
    }

    #endregion

    #region Audio

    public void UpdateSoundVolume()
    {
        if (soundSlider != null)
            soundVolume = soundSlider.value;

        if (musicSource != null)
            musicSource.volume = soundVolume;

        if (buttonClickSFX != null)
            buttonClickSFX.volume = soundVolume;

        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
        PlayerPrefs.Save();
    }

    void PlayClick()
    {
        if (buttonClickSFX != null)
        {
            buttonClickSFX.volume = soundVolume;
            buttonClickSFX.Play();
        }
    }

    #endregion

    void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }
}
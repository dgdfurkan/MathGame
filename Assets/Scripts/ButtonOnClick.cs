using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonOnClick : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [Header("Panels")] [SerializeField] private GameObject stabilPanel;
    [SerializeField] private GameObject backgroundMainPanel, levelPanel, processPanel, inGamePanel, gamePanel, leaderboardPanel, schoolPanel, settingsPanel, backgroundChangePanel, musicButton;

    [Space(25)] [SerializeField] private Sprite musicOn;
    [SerializeField] private Sprite musicOff;
    [SerializeField] private AudioSource buttonSound, backgroundSound;
    [SerializeField] private List<AudioSource> answersSounds;
    [SerializeField] private Slider musicVolume;

    #endregion

    #endregion

    private void Awake()
    {
        Panels();
        backgroundMainPanel.SetActive(true);
        MusicVolume();
    }

    #region Panel Voids

    public void Panels()
    {
        stabilPanel.SetActive(true);
        backgroundMainPanel.SetActive(false);
        levelPanel.SetActive(false);
        processPanel.SetActive(false);
        inGamePanel.SetActive(false);
        gamePanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        schoolPanel.SetActive(false);
        settingsPanel.SetActive(false);
        backgroundChangePanel.SetActive(false);
    }

    public void PanelBackgroundMain()
    {
        Panels();
        backgroundMainPanel.SetActive(true);
    }

    public void PanelLevel()
    {
        Panels();
        levelPanel.SetActive(true);
    }

    public void PanelProcess()
    {
        Panels();
        processPanel.SetActive(true);
    }

    public void PanelInGame()
    {
        Panels();
        inGamePanel.SetActive(true);
        gamePanel.SetActive(true);
        Processes.Instance.NewQuestion();
        //PlayerScores.Instance.GetGet();
    }

    public void PanelLeaderboard()
    {
        Panels();
        leaderboardPanel.SetActive(true);
    }

    public void PanelSchool()
    {
        Panels();
        schoolPanel.SetActive(true);
    }

    public void PanelSettingsOpen()
    {
        settingsPanel.SetActive(true);
    }

    public void PanelSettingsClose()
    {
        settingsPanel.SetActive(false);
    }

    public void PanelBackgroundChangeOpen()
    {
        settingsPanel.SetActive(false);
        backgroundChangePanel.SetActive(true);
    }

    public void PanelBackgroundChangeClose()
    {
        PlayerPrefs.SetInt("selectedIndex", ChangeBackground.Instance.selectedIndex);
        ChangeBackground.Instance.ChangeBck();
        backgroundChangePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    
    #endregion

    public void ButtonSound()
    {
        buttonSound.Play();
    }

    public void MusicOnOff()
    {
        if (musicButton.GetComponent<Image>().sprite == musicOff)
        {
            musicButton.GetComponent<Image>().sprite = musicOn;
        }else if (musicButton.GetComponent<Image>().sprite == musicOn)
        {
            musicButton.GetComponent<Image>().sprite = musicOff;
        }
    }

    public void MusicVolume()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolume.value = PlayerPrefs.GetFloat("musicVolume");
            buttonSound.volume = musicVolume.value;
            backgroundSound.volume = musicVolume.value;
            for (int i = 0; i < answersSounds.Count; i++)
            {
                answersSounds[i].volume = musicVolume.value;
            }
        }
        musicVolume.onValueChanged.AddListener((v) =>
        {
            PlayerPrefs.SetFloat("musicVolume", v);
            buttonSound.volume = musicVolume.value;
            backgroundSound.volume = musicVolume.value;
            for (int i = 0; i < answersSounds.Count; i++)
            {
                answersSounds[i].volume = musicVolume.value;
            }
        });
    }
}

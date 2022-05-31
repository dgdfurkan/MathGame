using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    public static ChangeBackground Instance;

    #region Self Variables

    #region Serialized Variables

    [SerializeField] private List<Sprite> Backgrounds;
    [SerializeField] private GameObject BackgroundImage, panelBackground;
    [SerializeField] public int selectedIndex;

    #endregion

    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }else if(Instance != null){
            Destroy(this.gameObject);
        }

        if (PlayerPrefs.HasKey("selectedIndex"))
        {
            BackgroundImage.GetComponent<Image>().sprite = Backgrounds[PlayerPrefs.GetInt("selectedIndex")];
            panelBackground.GetComponent<Image>().sprite = Backgrounds[PlayerPrefs.GetInt("selectedIndex")];
        }
    }
    public void ChangeBackPlus()
    {
        int lenght = Backgrounds.Count;
        selectedIndex++;
        if (selectedIndex == lenght)
        {
            selectedIndex = 0;
        }
        UpdateBackground();
    }

    public void ChangeBackMines()
    {
        int lenght = Backgrounds.Count;
        selectedIndex--;
        if (selectedIndex < 0)
        {
            selectedIndex = lenght - 1;
        }
        UpdateBackground();
    }

    public void UpdateBackground()
    {
        panelBackground.GetComponent<Image>().sprite = Backgrounds[selectedIndex];
    }

    public void ChangeBck()
    {
        BackgroundImage.GetComponent<Image>().sprite = Backgrounds[selectedIndex];
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuEngine : MonoBehaviour
{
    public Image about;
    public List<Sprite> info_cards = new List<Sprite>();
    private int info_index = 0;

    public RectTransform controller;
    public RectTransform keyboard;

    public TextMeshProUGUI soundVolumeText;
    public TextMeshProUGUI musicVolumeText;

    SettingsScript settings;

    GameObject aboutTab;
    GameObject controllsTab;

    private void Start()
    {
        settings = GameObject.Find("Settings").GetComponent<SettingsScript>();
        aboutTab = GameObject.Find("AboutTab");
        controllsTab = GameObject.Find("ControllsTab");
        if(aboutTab != null && controllsTab != null)
        {
            Back();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void About()
    {
        aboutTab.SetActive(true);
    }

    public void Controlls()
    {
        controllsTab.SetActive(true);
    }

    public void Back()
    {
        aboutTab.SetActive(false);
        controllsTab.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeInfo(int index)
    {
        info_index = Mathf.Abs(Modulo(index, info_cards.Count));
        about.sprite = info_cards[info_index];
    }

    public void PreviousInfo()
    {
        ChangeInfo(info_index - 1);
    }

    public void NextInfo()
    {
        ChangeInfo(info_index + 1);
    }

    public void SwitchControlls()
    {
        var defNumber = settings.defender;
        settings.defender = settings.attacker;
        settings.attacker = defNumber;

        var keyPosition = keyboard.localPosition.x * (-1);
        var conPosition = controller.localPosition.x * (-1);

        var keyAnchorMin = keyboard.anchorMin;
        keyboard.anchorMin = controller.anchorMin;
        controller.anchorMin = keyAnchorMin;

        var keyAnchorMax = keyboard.anchorMax;
        keyboard.anchorMax = controller.anchorMax;
        controller.anchorMax = keyAnchorMax;

        var keyPivot = keyboard.pivot;
        keyboard.pivot = controller.pivot;
        controller.pivot = keyPivot;

        keyboard.localPosition = new Vector3(keyPosition, keyboard.localPosition.y, keyboard.localPosition.z);
        controller.localPosition = new Vector3(conPosition, controller.localPosition.y, controller.localPosition.z);
    }

    public void SoundVolume(System.Single volume)
    {
        volume = (int)volume;
        settings.soundVolume = volume / 100;
        soundVolumeText.text = volume.ToString() + " %";
    }

    public void MusicVolume(System.Single volume)
    {
        volume = (int)volume;
        settings.musicVolume = volume / 100;
        musicVolumeText.text = volume.ToString() + " %";
    }

    private int Modulo(int a, int b)
    {
        return (a % b + b) % b;
    }
}

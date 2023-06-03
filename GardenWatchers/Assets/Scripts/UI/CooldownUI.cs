using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    [Dropdown("characters")]
    public string character;
    Action_Script action;
    Image cooldown;

    private string[] characters = new string[] { "Player_Offensive", "Player_Defensive" };

    // Start is called before the first frame update
    void Start()
    {
        cooldown = GetComponent<Image>();
        action = GameObject.Find(character).GetComponent<Action_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (action.currentAbilityCooldown == -1)
        {
            return;
        }

        cooldown.fillAmount = action.cooldown / action.currentAbilityCooldown;
    }
}

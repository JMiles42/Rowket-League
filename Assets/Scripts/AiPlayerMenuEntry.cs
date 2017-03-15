using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AiPlayerMenuEntry : JMilesBehaviour
{
    public bool Enabled = false;
    public DropDownEvent AiMode;
    public DropDownEvent AiReactionSpeed;
    public ButtonClickEvent ButtonClick;
    public InputFieldEvent Inputfield;
    private bool IsPlayer;
    [SerializeField] private AiAgressiveMode aiAgressiveMode = AiAgressiveMode.BallOnly;
    [SerializeField] private AiReactionTime aiReactionTime = AiReactionTime.Normal;

    private const string ENABLED_STR = "Active", DISABLED_STR = "In-Active";
    private void OnEnable()
    {
        ButtonClick.onMouseClick += OnButtonClicked;

        AiMode.onValueChanged+= GetAiAgressiveModeFromDropDown;
        AiReactionSpeed.onValueChanged+= GetAiReactionTimeFromDropDown;

        AiMode.myDropdown.interactable = Enabled;
        AiReactionSpeed.myDropdown.interactable = Enabled;
        Inputfield.inputField.interactable = Enabled;
        ButtonClick.myButton.SetText(Enabled ? ENABLED_STR : DISABLED_STR);
    }

    private void OnDisable()
    {
        ButtonClick.onMouseClick -= OnButtonClicked;
        AiMode.onValueChanged-= GetAiAgressiveModeFromDropDown;
        AiReactionSpeed.onValueChanged-= GetAiReactionTimeFromDropDown;
    }

    private void OnButtonClicked()
    {
        Enabled = !Enabled;

        AiMode.myDropdown.interactable = Enabled;
        AiReactionSpeed.myDropdown.interactable = Enabled;
        Inputfield.inputField.interactable = Enabled;
        ButtonClick.myButton.SetText(Enabled ? ENABLED_STR : DISABLED_STR);
    }

    void GetAiAgressiveModeFromDropDown(int value)
    {
        if (value == 5)
        {
            aiAgressiveMode = AiAgressiveMode.PlayerOne;
            AiReactionSpeed.myDropdown.interactable = false;
            IsPlayer = true;
        }
        else if (value == 6)
        {
            aiAgressiveMode = AiAgressiveMode.PlayerTwo;
            AiReactionSpeed.myDropdown.interactable = false;
            IsPlayer = true;
        }
        else
        {
            IsPlayer = false;
            AiReactionSpeed.myDropdown.interactable = Enabled;
            aiAgressiveMode = (AiAgressiveMode) AiMode.myDropdown.value;
        }
    }

    void GetAiReactionTimeFromDropDown(int value)
    {
        aiReactionTime = (AiReactionTime) value;
    }

    public PlayerDetails GetPlayerDetails()
    {
        var pD = new PlayerDetails(aiReactionTime,aiAgressiveMode,Enabled,IsPlayer,Inputfield.Text);
        return pD;
    }
}
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
    [SerializeField] private AiAgressiveMode aiAgressiveMode = AiAgressiveMode.BallOnly;
    [SerializeField] private AiReactionTime aiReactionTime = AiReactionTime.Normal;

    private void OnEnable()
    {
        ButtonClick.onMouseClick += OnButtonClicked;

        AiMode.onValueChanged+= GetAiAgressiveModeFromDropDown;
        AiReactionSpeed.onValueChanged+= GetAiReactionTimeFromDropDown;

        AiMode.myDropdown.interactable = Enabled;
        AiReactionSpeed.myDropdown.interactable = Enabled;
        Inputfield.inputField.interactable = Enabled;
        ButtonClick.myButton.SetText(Enabled ? "Enabled" : "Disabled");
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
        ButtonClick.myButton.SetText(Enabled ? "Enabled" : "Disabled");
    }

    void GetAiAgressiveModeFromDropDown(int value)
    {
        if (value == 5)
        {
            AiReactionSpeed.myDropdown.gameObject.SetActive(false);
            Inputfield.inputField.gameObject.SetActive(true);
            aiAgressiveMode = AiAgressiveMode.Player;
        }
        else
        {
            AiReactionSpeed.myDropdown.gameObject.SetActive(true);
            aiAgressiveMode = (AiAgressiveMode) AiMode.myDropdown.value;
            Inputfield.inputField.gameObject.SetActive(false);
        }
    }

    void GetAiReactionTimeFromDropDown(int value)
    {
        aiReactionTime = (AiReactionTime) value;
    }
}
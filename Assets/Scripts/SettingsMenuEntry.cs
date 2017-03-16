using UnityEngine;

public class AiPlayerMenuEntry : JMilesBehaviour
{
    public bool Enabled = false;
    public DropDownEvent AiMode;
    public DropDownEvent AiReactionSpeed;
    public ButtonClickEvent ButtonClick;
    public InputFieldEvent Inputfield;
    bool IsPlayer;
    [SerializeField] AiAggressiveMode aiAggressiveMode = AiAggressiveMode.BallOnly;
    [SerializeField] AiReactionTime aiReactionTime = AiReactionTime.Normal;

    const string ENABLED_STR = "Active", DISABLED_STR = "In-Active";
    void OnEnable()
    {
        ButtonClick.onMouseClick += OnButtonClicked;
        AiMode.onValueChanged+= GetAiAggressiveModeFromDropDown;
        AiReactionSpeed.onValueChanged+= GetAiReactionTimeFromDropDown;

        AiMode.myDropdown.interactable = Enabled;
        AiReactionSpeed.myDropdown.interactable = Enabled;
        Inputfield.inputField.interactable = Enabled;
        ButtonClick.myButton.SetText(Enabled ? ENABLED_STR : DISABLED_STR);
    }

    void OnDisable()
    {
        ButtonClick.onMouseClick -= OnButtonClicked;
        AiMode.onValueChanged-= GetAiAggressiveModeFromDropDown;
        AiReactionSpeed.onValueChanged-= GetAiReactionTimeFromDropDown;
    }

    void OnButtonClicked()
    {
        Enabled = !Enabled;

        AiMode.myDropdown.interactable = Enabled;
        AiReactionSpeed.myDropdown.interactable = Enabled;
        Inputfield.inputField.interactable = Enabled;
        ButtonClick.myButton.SetText(Enabled ? ENABLED_STR : DISABLED_STR);
    }

    void GetAiAggressiveModeFromDropDown(int value)
    {
        //These values are where these modes show up in the dropdown box UI
        switch (value)
        {
            case 5:
                aiAggressiveMode = AiAggressiveMode.PlayerOne;
                AiReactionSpeed.myDropdown.interactable = false;
                IsPlayer = true;
                break;
            case 6:
                aiAggressiveMode = AiAggressiveMode.PlayerTwo;
                AiReactionSpeed.myDropdown.interactable = false;
                IsPlayer = true;
                break;
            case 7:
                aiAggressiveMode = AiAggressiveMode.PlayerThree;
                AiReactionSpeed.myDropdown.interactable = false;
                IsPlayer = true;
                break;
            case 8:
                aiAggressiveMode = AiAggressiveMode.PlayerFour;
                AiReactionSpeed.myDropdown.interactable = false;
                IsPlayer = true;
                break;
            default:
                IsPlayer = false;
                AiReactionSpeed.myDropdown.interactable = Enabled;
                aiAggressiveMode = (AiAggressiveMode) AiMode.myDropdown.value;
                break;
        }
    }

    void GetAiReactionTimeFromDropDown(int value)
    {
        aiReactionTime = (AiReactionTime) value;
    }

    public PlayerDetails GetPlayerDetails()
    {
        var pD = new PlayerDetails(aiReactionTime,aiAggressiveMode,Enabled,IsPlayer,Inputfield.Text);
        return pD;
    }
}
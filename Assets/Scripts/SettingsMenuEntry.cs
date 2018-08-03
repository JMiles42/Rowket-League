using UnityEngine;

public class SettingsMenuEntry: JMilesBehaviour
{
	private const            string           ENABLED_STR = "Active", DISABLED_STR = "In-Active";
	public                   bool             Enabled;
	public                   DropDownEvent    AiMode;
	public                   DropDownEvent    AiReactionSpeed;
	public                   ButtonClickEvent ButtonClick;
	public                   InputFieldEvent  Inputfield;
	[SerializeField] private InputMoterMode   aiAggressiveMode = InputMoterMode.BallOnly;
	[SerializeField] private AiReactionTime   aiReactionTime   = AiReactionTime.Normal;

	private void OnEnable()
	{
		ButtonClick.onMouseClick                += OnButtonClicked;
		AiMode.onValueChanged                   += GetInputMotorModeFromDropDown;
		AiReactionSpeed.onValueChanged          += GetAiReactionTimeFromDropDown;
		AiMode.myDropdown.interactable          =  Enabled;
		AiReactionSpeed.myDropdown.interactable =  Enabled;
		Inputfield.inputField.interactable      =  Enabled;
		ButtonClick.myButton.SetText(Enabled? ENABLED_STR : DISABLED_STR);
		AiMode.myDropdown.value = (int) aiAggressiveMode;
		GetInputMotorModeFromDropDown(AiMode.myDropdown.value);
		AiReactionSpeed.myDropdown.value = (int) aiReactionTime;
	}

	private void OnDisable()
	{
		ButtonClick.onMouseClick       -= OnButtonClicked;
		AiMode.onValueChanged          -= GetInputMotorModeFromDropDown;
		AiReactionSpeed.onValueChanged -= GetAiReactionTimeFromDropDown;
	}

	private void OnButtonClicked()
	{
		Enabled                                 = !Enabled;
		AiMode.myDropdown.interactable          = Enabled;
		AiReactionSpeed.myDropdown.interactable = Enabled;
		Inputfield.inputField.interactable      = Enabled;
		ButtonClick.myButton.SetText(Enabled? ENABLED_STR : DISABLED_STR);
	}

	private void GetInputMotorModeFromDropDown(int value)
	{
		//These values are where these modes show up in the dropdown box UI
		switch(value)
		{
			case 5:
				aiAggressiveMode                        = InputMoterMode.PlayerOne;
				AiReactionSpeed.myDropdown.interactable = false;

				break;
			case 6:
				aiAggressiveMode                        = InputMoterMode.PlayerTwo;
				AiReactionSpeed.myDropdown.interactable = false;

				break;
			case 7:
				aiAggressiveMode                        = InputMoterMode.PlayerThree;
				AiReactionSpeed.myDropdown.interactable = false;

				break;
			case 8:
				aiAggressiveMode                        = InputMoterMode.PlayerFour;
				AiReactionSpeed.myDropdown.interactable = false;

				break;
			default:
				AiReactionSpeed.myDropdown.interactable = Enabled;
				aiAggressiveMode                        = (InputMoterMode)AiMode.myDropdown.value;

				break;
		}
	}

	private void GetAiReactionTimeFromDropDown(int value)
	{
		aiReactionTime = (AiReactionTime)value;
	}

	public PlayerDetails GetPlayerDetails()
	{
		var pD = new PlayerDetails(aiReactionTime, aiAggressiveMode, Enabled, Inputfield.Text);

		return pD;
	}
}
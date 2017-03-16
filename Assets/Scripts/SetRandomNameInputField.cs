/// <summary>
/// Sets the input feilds value to an entry in the StringList
/// </summary>
public class SetRandomNameInputField : JMilesBehaviour
{
    public InputFieldEvent inputField;
    public StringListScriptableObject stringList;

    private void Start()
    {
        if (inputField.Text == "")
            inputField.Text = stringList.GetRandomEntry();
    }
}
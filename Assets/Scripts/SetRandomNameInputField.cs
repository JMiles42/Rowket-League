public class SetRandomNameInputField : JMilesBehaviour
{
    public InputFieldEvent inputField;
    public StringListScriptableObject NameList;

    void Start()
    {
        if (inputField.Text == "")
            inputField.Text = NameList.GetRandomEntry();
    }
}

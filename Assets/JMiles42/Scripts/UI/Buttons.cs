using UnityEngine.UI;
namespace JMiles42.UI
{
	/// <summary>
	/// This struct contains the refferance for the button and its text.
	/// </summary>
	[System.Serializable]
	public struct ButtonText
	{
		public Button Btn;	//Button
		public Text Txt;    //Buttons text
		/// <summary>
		/// Get text in children.
		/// </summary>
		public void GetTextObj()
		{
			if ( Txt )
				return;
			Txt = Btn.GetComponentInChildren<Text> ();
		}
		/// <summary>
		/// Set Text in Txt.
		/// </summary>
		/// <param name="t">Value to set text</param>
		public void SetText(string t = "")
		{
			Txt.text = t;
        }
		/// <summary>
		/// Returns Text from Txt.
		/// </summary>
		/// <returns>The current text in Txt</returns>
		public string GetText()
		{
			return ( Txt.text.ToString () );
		}
	}
	[System.Serializable]
	public struct ImageText
	{
		public Image Img;  //Button
		public Text Txt;    //Buttons text
							/// <summary>
							/// Get text in children.
							/// </summary>
		public void GetTextObj()
		{
			if (Txt)
				return;
			Txt = Img.GetComponentInChildren<Text>();
		}
		/// <summary>
		/// Set Text in Txt.
		/// </summary>
		/// <param name="t">Value to set text</param>
		public void SetText(string t = "")
		{
			Txt.text = t;
		}
		/// <summary>
		/// Returns Text from Txt.
		/// </summary>
		/// <returns>The current text in Txt</returns>
		public string GetText()
		{
			return (Txt.text.ToString());
		}
	}
}

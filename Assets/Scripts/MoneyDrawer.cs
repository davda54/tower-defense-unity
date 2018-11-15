using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDrawer : MonoBehaviour
{
    public GameObject TextFieldObject;
    private Text text;

    public void Draw(int money)
    {
        text.text = money.ToString();
    }

	// Use this for initialization
	void OnEnable ()
    {
        text = TextFieldObject.GetComponent<Text>();
	}
}

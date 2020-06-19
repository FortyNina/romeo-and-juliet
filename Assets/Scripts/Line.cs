using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Line : MonoBehaviour
{
	private TextMeshProUGUI gui;

	private void Start()
	{
		gui = GetComponent<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update()
    {
		gui.color = new Color(gui.color.r, gui.color.g, gui.color.b, GetAlphaFromHeight(transform.position.y));
		Debug.Log(transform.position.y);
    }

	private float GetAlphaFromHeight(float y)
	{
		float temp = 500 - y;
		return temp / 500;
	}

}

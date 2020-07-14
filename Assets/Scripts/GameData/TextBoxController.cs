using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxController : MonoBehaviour
{
    public string[] textLines;

    public GameObject textBox;
    public Text textObject;

    public int currLine;
    public int endLine;

    public bool active;

    void Start()
    {
        textBox = GameObject.FindGameObjectWithTag("TextBox");

        textBox.SetActive(false);
        active = false;

    }

    void Update()
    {
        if (active)
        {
            textObject.text = textLines[currLine];

            if (Input.GetKeyDown(KeyCode.Return))
            {
                currLine += 1;
            }

            if (currLine > endLine)
            {
                textBox.SetActive(false);
                active = false;
                currLine = 0;
            }
        }
    }

    public void NewTextBox(TextAsset newText, int newEndLine)
    {
        if (newText != null)
        {
            textLines = (newText.text.Split('\n'));
        }

        if (newEndLine == 0)
        {
            endLine = textLines.Length - 1;
        }
        else
        {
            endLine = newEndLine;
        }

        textBox.SetActive(true);
        active = true;
    }
}


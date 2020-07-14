using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TextInteractive : MonoBehaviour {

    public TextBoxController textBox;
    public TextAsset textFile;
    public int endLine;

    public bool interactiveObject;
    public bool inRange;

    public GameObject FloatingTextPrefab;

    void Start()
    {
        //textBox = GameObject.FindGameObjectWithTag("TextBox").GetComponent<TextBoxController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange)
        {
            textBox.NewTextBox(textFile, 0);
        }
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            if (!interactiveObject)
            {

            }
            else
            {
                inRange = true;
                FloatingTextPrefab.SetActive(true);
            }
        }
    }

    //When the player leaves this object's collider, remove text and set inRange to false.
    private void OnTriggerExit(Collider Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            if (interactiveObject)
            {
                inRange = false;
                FloatingTextPrefab.SetActive(false);
            }
        }
    }
}

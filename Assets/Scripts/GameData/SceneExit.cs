using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneExit : MonoBehaviour
{
    public string exitName;
    public string newScene;
    public SceneTransition sceneTransition;
    public bool interactiveExit;
    public bool inRange;

    public GameObject FloatingTextPrefab;

    void Start()
    {
        inRange = false;
    }

    void Update()
    {
        //Check for interact button when player is able to use exit.
        if (Input.GetKeyDown(KeyCode.E) && inRange && interactiveExit) {
            GameMaster.lastExitUsed = exitName;
            Debug.Log("Name Recorded");
            sceneTransition.changeScene(newScene);
        }
    }

    //When the player enters this object's collider, show text and set inRange to true if 
    //  it is an interactive exit. Change scene and record exitName if not.

    void OnTriggerEnter(Collider Col)
    {
         if (Col.gameObject.tag == "Player")
        {
            if (!interactiveExit)
            {
                GameMaster.lastExitUsed = exitName;
                Debug.Log("Name Recorded");
                sceneTransition.changeScene(newScene);
            }
            else {
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
            if (interactiveExit)
            {
                inRange = false;
                FloatingTextPrefab.SetActive(false);
            }
        }
    }

    //If Used then records exitName and changes scene.
    void OnInteraction()
    {
        GameMaster.lastExitUsed = exitName;
        Debug.Log("Name Recorded");
        sceneTransition.changeScene(newScene);
    }
}

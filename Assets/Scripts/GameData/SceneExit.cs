using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneExit : MonoBehaviour
{
    public string exitName;
    public string newScene;
    public SceneTransition sceneTransition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider Col)
    {
         if (Col.gameObject.tag == "Player")
        {
            GameMaster.lastExitUsed = exitName;
            Debug.Log("Name Recorded");
            sceneTransition.changeScene(newScene);
        }
    }
}

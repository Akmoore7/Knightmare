using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator sceneAnimator;
    public string sceneName;
    public PlayerController player;

    public SceneEntrance[] entrances;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        entrances = GameObject.FindGameObjectWithTag("Entrance").GetComponents<SceneEntrance>();
        findEntrance();
    }

    //Changes the scene to "newScene".
    public void changeScene(string newScene)
    {
        sceneName = newScene;
        StartCoroutine(LoadScene());
    }

    //Finds the Entrance with the "ConnectedExit" value that is the same as the 
    //  "lastExitUsed" stored in the GameMaster.
    public void findEntrance()
    {
        for(int i = 0; i < entrances.Length; i++)
        {
            Debug.Log(GameMaster.lastExitUsed);
            if (entrances[i].connectedExit.Equals(GameMaster.lastExitUsed))
            {
                player.setTransformPos(entrances[i].GetComponent<Transform>());
                Debug.Log("exitfound");
            }
        }

    }

    //Animated Scene Fadeout, change WaitForSeconds to change fadeout time.
    IEnumerator LoadScene()
    {
        sceneAnimator.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}

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
    //public BoxCollider[] portals;
    // Start is called before the first frame update
    void Start()
    {
        findEntrance();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(LoadScene());

        }
    }

    public void changeScene(string newScene)
    {
        sceneName = newScene;
        StartCoroutine(LoadScene());
    }

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

    IEnumerator LoadScene()
    {
        sceneAnimator.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}

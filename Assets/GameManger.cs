using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    [SerializeField] private float sceneLoadingDelay = 2.0f;

    private Coroutine routine = null;
    
    public void ResetGame()
    {
        // .. Reset Game
        if(routine == null) {
            routine = StartCoroutine(ResettingGame());
        }
    }

    private IEnumerator ResettingGame()
    {
        yield return new WaitForSeconds(sceneLoadingDelay);

        SceneManager.UnloadSceneAsync(gameObject.scene.buildIndex);
        SceneManager.LoadScene(gameObject.scene.buildIndex);

        routine = null;
    }
}

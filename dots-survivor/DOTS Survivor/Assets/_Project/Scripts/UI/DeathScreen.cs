using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Scenes;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Load scores
        CheckHighscore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckHighscore()
    {
        int score = 999;
        int highscore = PlayerPrefs.GetInt("highscore", 0);
        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
            //play highscore animation
        }
    }

    private void CleanUpEntities()
    {
        // 1
        //Unity.Entities.World.DisposeAllWorlds();
        //var entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;
        //entityManager.DestroyEntity(entityManager.UniversalQuery);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        World.DisposeAllWorlds();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();

        // 2 
        // Destroy all entities in the current world
        //var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        //entityManager.DestroyEntity(entityManager.UniversalQuery);
        //
        //// Load the desired scene
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

        //3
        // Dispose the current ECS world
        //World.DisposeAllWorlds();
        //
        //// Initialize a new ECS world with the desired scene
        //DefaultWorldInitialization.Initialize("SampleScene", false);
        //ScriptBehaviourUpdateOrder.AppendWorldToCurrentPlayerLoop(World.DefaultGameObjectInjectionWorld);
    }

    public void ReloadLevel()
    {
        CleanUpEntities();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}

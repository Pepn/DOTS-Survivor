using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
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
        var em = World.DefaultGameObjectInjectionWorld.EntityManager;
        var entities = em.GetAllEntities();
        for (var i = 0; i < entities.Length; ++i)
        {
            em.DestroyEntity(entities[i]);
        }
    }

    public void ReloadLevel()
    {
        CleanUpEntities();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceLoadTrigger : MonoBehaviour
{
    [SerializeField] private SceneField[] _sceneToLoad;
    [SerializeField] private SceneField[] _sceneToUnLoad;

    private bool _isScenceLoad;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player")) 
        {
          // load and unload scence ;
          LoadScence();
          UnLoadScence();
        }
    }
    private void LoadScence() 
    {
        for(int i = 0; i < _sceneToLoad.Length; i++) 
        {
            for(int j = 0; j < SceneManager.sceneCount; j++) 
            { 
               Scene loadScene = SceneManager.GetSceneAt(j);
                if (loadScene.name == _sceneToLoad[i].SceneName) 
                {
                    _isScenceLoad = true;
                    break;
                }
            }
            if (!_isScenceLoad) 
            {
                SceneManager.LoadSceneAsync(_sceneToLoad[i],LoadSceneMode.Additive);
            }
        }
    }
    private void UnLoadScence() 
    {
        for (int i = 0; i < _sceneToLoad.Length; i++)
        {
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadScene = SceneManager.GetSceneAt(j);
                if (loadScene.name == _sceneToUnLoad[i].SceneName)
                {
                    SceneManager.UnloadSceneAsync(_sceneToUnLoad[i]);
                }
            }
          
        }
    }
}

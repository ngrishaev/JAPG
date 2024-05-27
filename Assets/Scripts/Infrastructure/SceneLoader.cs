using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        
        public void Load(string name, Action? onLoaded = null) => 
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

        private IEnumerator LoadScene(string nextScene, Action? onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name.Equals(nextScene))
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation? waitNextScene = SceneManager.LoadSceneAsync(nextScene);
            
            Assert.IsNotNull(waitNextScene, $"Scene {nextScene} not found");
            while (!waitNextScene.isDone)
                yield return null;
            
            onLoaded?.Invoke();
        }
    }
}
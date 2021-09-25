using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
  void Update()
  {
    if (SceneManager.GetActiveScene().name != "Game" && Input.anyKeyDown)
      SceneController.nextScene();
  }
  public static void nextScene()
  {
    if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
      SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    else
      SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
  }

  public static void reloadScene()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
  }
}

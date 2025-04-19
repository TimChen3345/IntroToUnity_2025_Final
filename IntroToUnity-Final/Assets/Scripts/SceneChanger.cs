using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

  public void ChangeScene(string newScene)
{
    Debug.Log ("Changing to" + newScene);

    SceneManager.LoadScene(newScene);
}

}

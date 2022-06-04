using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void ChangeScenes(int numberScene)
    {
        SceneManager.LoadScene(numberScene);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void SetCharacter(int index)
    {
        PlayerPrefs.SetInt("Character", index);
    }
    public void SetPlanet(int index)
    {
        PlayerPrefs.SetInt("Planet", index);
    }
}

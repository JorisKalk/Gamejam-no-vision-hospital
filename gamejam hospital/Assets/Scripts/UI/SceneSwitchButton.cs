using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchButton : MonoBehaviour
{
    [SerializeField]
    private int targetScene;

    public void OnButtonPressed()
    {
        SceneManager.LoadScene(targetScene);
    }
}

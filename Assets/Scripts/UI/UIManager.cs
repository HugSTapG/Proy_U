using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField]private GameObject gameOverScreen;
    private void Awake()
    {
        gameOverScreen.SetActive(false);
    }
    //Activa la pantalla de GameOver
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}

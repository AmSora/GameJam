using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnPlay()
    {
        SceneManager.LoadScene("JUEGO");
    }

    public void BtnQuit()
    {
        Application.Quit();
    }
}

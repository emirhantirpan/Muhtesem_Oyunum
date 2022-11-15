using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject mainPanel, settingsPanel;
    
    void Start()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);

    }

    void Update()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Level1");
    }
    public void SettingsButton()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void BackButton()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    public void PlayAgainButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitButton()
    {
        Application.Quit();
        
    }
    
}

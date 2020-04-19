using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public int gameSceneBuildIndex = 1;
    public GameObject creditsPanel;
    public GameObject mainPanel;
    private GameObject activePanel;
    private void Start()
    {
        creditsPanel.SetActive(false);
        activePanel = mainPanel;
        activePanel.SetActive(true);
    }
    public void NewGame()
    {
        SceneManager.LoadScene(gameSceneBuildIndex);
    }
    private void SwitchToPanel(GameObject panel)
    {
        activePanel.SetActive(false);
        activePanel = panel;
        activePanel.SetActive(true);
    }
    public void Credits()
    {
        SwitchToPanel(creditsPanel);
    }
    public void BackToMenu()
    {
        SwitchToPanel(mainPanel);
    }
}

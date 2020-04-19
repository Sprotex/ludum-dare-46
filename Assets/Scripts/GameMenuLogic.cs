using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuLogic : MonoBehaviour
{
    public int mainMenuBuildIndex = 0;
    public GameObject menuPanel;
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;
    public PlayerFeed playerFeed;
    private void Start()
    {
        menuPanel.SetActive(true);
        DisplayMenu();
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuBuildIndex);
    }
    public void DisplayMenu()
    {
        var activeState = !menuPanel.activeSelf;
        menuPanel.SetActive(activeState);
        playerAttack.enabled = !activeState;
        playerMovement.enabled = !activeState;
        playerFeed.enabled = !activeState;
        Time.timeScale = activeState ? 0f : 1f;
        Cursor.lockState = activeState ? CursorLockMode.None : CursorLockMode.Locked;
    }
    private void Update()
    {
        if (Input.GetButtonDown(CConstants.Input.Cancel))
        {
            DisplayMenu();
        }
    }
}

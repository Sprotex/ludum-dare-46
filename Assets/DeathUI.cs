using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUI : MonoBehaviour
{
    public GameObject reasonHungerPanel;
    public GameObject reasonHealthPanel;
    public GameMenuLogic gameMenuLogic;

    private void Common()
    {
        var sound = SoundManager.instance.Play(Vector3.zero, SoundManager.instance.gameOver);
        sound.spatialBlend = 0f;
        Cursor.lockState = CursorLockMode.None;
        gameMenuLogic.enabled = false;
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DeathByHunger()
    {
        Common();
        reasonHungerPanel.SetActive(true);
    }
    public void DeathByHealth()
    {
        Common();
        reasonHealthPanel.SetActive(true);
    }
}

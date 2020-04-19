using System.Collections.Generic;
using UnityEngine;

public class TutorialLogic : MonoBehaviour
{
    public GameObject movePanel;
    public GameObject flyPanel;
    public GameObject attackPanel;
    public GameObject pickupPanel;
    public GameObject feedPanel;
    private List<GameObject> tutorialSteps = new List<GameObject>();
    private int tutorialStepIndex = 0;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Tutorial") == 1)
        {
            PlayerPrefs.SetInt("Tutorial", 0);
        }
    }
}

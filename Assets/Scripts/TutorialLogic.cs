﻿using System.Collections.Generic;
using UnityEngine;

public class TutorialLogic : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject horizontalMovePanel;
    public GameObject verticalMovePanel;
    public GameObject flyPanel;
    public GameObject attackPanel;
    public GameObject pickupPanel;
    public GameObject feedSelfPanel;
    public GameObject feedChildrenPanel;
    public GameObject lastStep;
    private List<GameObject> tutorialSteps = new List<GameObject>();
    private List<string> tutorialButtonSteps = new List<string>();
    private int tutorialStepIndex = 0;

    private void Start()
    {
        tutorialSteps.Add(verticalMovePanel);
        tutorialButtonSteps.Add(CConstants.Input.VerticalAxis);
        tutorialSteps.Add(horizontalMovePanel);
        tutorialButtonSteps.Add(CConstants.Input.HorizontalAxis);
        tutorialSteps.Add(flyPanel);
        tutorialButtonSteps.Add(CConstants.Input.Fly);
        tutorialSteps.Add(attackPanel);
        tutorialButtonSteps.Add(CConstants.Input.Attack);
        tutorialSteps.Add(pickupPanel);
        tutorialButtonSteps.Add(CConstants.Input.Pickup);
        tutorialSteps.Add(feedChildrenPanel);
        tutorialButtonSteps.Add(CConstants.Input.FeedChildren);
        tutorialSteps.Add(feedSelfPanel);
        tutorialButtonSteps.Add(CConstants.Input.FeedSelf);
        lastStep.SetActive(false);
        foreach (var step in tutorialSteps)
        {
            step.SetActive(false);
        }
        tutorialStepIndex = tutorialSteps.Count + 1;
        if (PlayerPrefs.GetInt(CConstants.PPrefs.Strings.Tutorial, CConstants.PPrefs.DefaultValues.Tutorial) == 1)
        {
            tutorialPanel.SetActive(true);
            tutorialStepIndex = 0;
            tutorialSteps[tutorialStepIndex].SetActive(true);
        } else
        {
            Destroy(tutorialPanel);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (tutorialStepIndex != tutorialSteps.Count)
        {
            if (Input.GetButton(tutorialButtonSteps[tutorialStepIndex]))
            {
                tutorialSteps[tutorialStepIndex].SetActive(false);
                if (tutorialStepIndex < tutorialSteps.Count)
                {
                    ++tutorialStepIndex;
                    if (tutorialStepIndex < tutorialSteps.Count)
                    {
                        tutorialSteps[tutorialStepIndex].SetActive(true);
                    }
                }
                else
                {
                    Destroy(tutorialPanel);
                    Destroy(gameObject);
                }
            }
        } else
        {
            lastStep.SetActive(true); 
            PlayerPrefs.SetInt(CConstants.PPrefs.Strings.Tutorial, 0);
            CoroutineManager.instance.DestroyAfter(tutorialPanel, 2f);
            CoroutineManager.instance.DestroyAfter(gameObject, 2f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public PanelScript currentPanel = null;

    private List<PanelScript> panelHistory = new List<PanelScript>();

    private void Start()
    {
        SetupPanels();
    }

    private void SetupPanels()
    {
        var panels = GetComponentsInChildren<PanelScript>();

        foreach (var panel in panels)
            panel.Setup(this);

        currentPanel.Show();
    }

    private void Update()
    {
        // Button for clicking
        if (OVRInput.GetDown(OVRInput.Button.One))
            GoToPrevious();
    }

    public void GoToPrevious()
    {
        if (panelHistory.Count == 0)
        {
            OVRManager.PlatformUIConfirmQuit();
            return;
        }

        var lastIndex = panelHistory.Count - 1;
        SetCurrent(panelHistory[lastIndex]);
        panelHistory.RemoveAt(lastIndex);
    }

    public void SetCurrentWithHistory(PanelScript newPanel)
    {
        panelHistory.Add(currentPanel);
        SetCurrent(newPanel);
    }

    public void SetCurrent(PanelScript newPanel)
    {
        currentPanel.Hide();

        currentPanel = newPanel;
        currentPanel.Show();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGamne()
    {
        Debug.Log("Game has been quit");
        Application.Quit();
    }
}
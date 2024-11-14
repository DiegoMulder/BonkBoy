using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour
{
    public GameObject pauseMenu, endPanel, deathPanel;
    private bool pauseMenuToggle;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenuToggle = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        PauseMenuLogic();
        PanelBehaviour();
    }

    private void PanelBehaviour()
	{
		if (PlayerHealth.isDead == true)
		{
            deathPanel.SetActive(true);
            Time.timeScale = 0;
		}
        else if (EndRoom.theEnd == true)
		{
            endPanel.SetActive(true);
            Time.timeScale = 0;
		}
	}

    private void PauseMenuLogic()
	{
        if (Input.GetKeyDown(KeyCode.Escape)) pauseMenuToggle = !pauseMenuToggle;

		if (pauseMenuToggle)
		{
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
		{
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
	}

    public void ResumeButton() => pauseMenuToggle = false;
}

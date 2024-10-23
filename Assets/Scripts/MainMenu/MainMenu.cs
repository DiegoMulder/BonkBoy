using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneSwitchBehaviour(int ID) => SceneManager.LoadScene(ID);

    public void SettingsActivate() => settingsPanel.SetActive(true);

    public void SettingsDisable() => settingsPanel.SetActive(false);

    public void QuitBehaviour() => Application.Quit();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    public bool isGame;

    private void Awake()
    {
        if(!isGame)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SceneLoad(string SceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }
    public void Exit()
    {
        Application.Quit();
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isGame) SceneLoad("Menu");
    }
}

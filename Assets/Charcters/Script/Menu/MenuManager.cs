using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
   public void Play()
   {
        SceneManager.LoadScene(2);
   }
   public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }
    
}

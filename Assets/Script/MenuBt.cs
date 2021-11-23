using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBt : MonoBehaviour
{
    // Start is called before the first frame update
    public void BtQuit()
    {
       Application.Quit();
    }
 

 public void OpenAudio()
    {
        Application.OpenURL("https://youtube.com/playlist?list=PLsDUIWlwo18rUD1-3R3QoSDn3dzQtfGrT");
    }

    public void OpenVideo()
    {
        Application.OpenURL("https://youtube.com/playlist?list=PLsDUIWlwo18rcBp4t99VibngiWnQHtyEo");
    }

      public void OpenQuiz()
    {
      SceneManager.LoadScene("Level1");
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

public class SpriteOnVisible : MonoBehaviour
{

    public GameObject targetObject;
    public int countAnswer;
    
    private static int k=0;
   

   void Start () { 
         
       if  (targetObject!=null)
       {
           targetObject.SetActive(false);
       }
    

    }



     public void VisibleTask()
    {
        k++;
        Debug.Log(k);
        targetObject.SetActive(true);
        Debug.Log(targetObject.name);
        
        StartCoroutine(GameObject.Find("audioClick").GetComponent<SoundBt>().winSound());
        
        var lr = GameObject.Find("star"+k.ToString());
        lr.GetComponent<SpriteRenderer>().color = new Color(178f/255f,209f/255f,121f/255f);
        ScoreKeeper.GetScoreKeeper().Score += 1;

        var bt = EventSystem.current.currentSelectedGameObject;
        bt.GetComponent<Button>().enabled = false;
    }

    public void SaveAnswearAndLoadScene(string sceneName)
    {
        
        var OK = EventSystem.current.currentSelectedGameObject;
        if(OK != null) { OK.GetComponent<Button>().enabled = false;}
         StartCoroutine(LoadSceneAsync(sceneName));
        // ScoreKeeper.GetScoreKeeper().Score -= 1;
         
    }

    public IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName);
    }

}


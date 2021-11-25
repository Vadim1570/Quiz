using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOnVisible : MonoBehaviour
{

    public GameObject targetObject;
   

   void Start () {    
       if  (targetObject!=null)
       {
           targetObject.SetActive(false);
       }
    

    }



     public void VisibleTask()
    {
        targetObject.SetActive(true);
    }

}


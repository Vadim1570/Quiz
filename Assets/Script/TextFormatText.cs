using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TextFormatText : MonoBehaviour
{
  [SerializeField] private Text myText;
  private void Start() 
  {
      myText.text = "Пока Сургут";
     
  }
}

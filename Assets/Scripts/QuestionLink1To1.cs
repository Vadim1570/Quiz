using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestionLink1To1 : MonoBehaviour
{
    public string QuestionUniqueId;

    //Заранее заполненные правильные ответы
    public PointPair [] rightLinks;

    //Сюда запоминаем точки, которые пытаються связать между собой
    public GameObject point1 = null;

    public Vector3 MousePos;
    public Vector3 PrevMousePos;
    public GameObject point2 = null;

    //public int MouseButtonCount1;
    //public int MouseButtonCount2;
    //public int MouseButtonCount3;
    
    //Сюда запоминаем уже связанные между собой точки
    public List<PointPair> alreadylinked = new List<PointPair>();

    #region Private methods
    private bool IsAlreadyLinked(GameObject point)
    {
        foreach(var pair in alreadylinked)
        {
            if(point == pair.Point1 || point == pair.Point2)
            return true;
        }
        return false;
    }

    private void RemoveFromAlreadyLinked(GameObject point)
    {
        var pairToRemove = alreadylinked.FirstOrDefault(pair => point == pair.Point1 || point == pair.Point2);
        if (pairToRemove != null)
            alreadylinked.Remove(pairToRemove);
    }
    #endregion

    private void Awake()
    {
    }

    void Start()
    {
        PrevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos = PrevMousePos;
    }

    void Update()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) {
            //MouseButtonCount1++;
            //MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(MousePos.x, MousePos.y);
            
            //Если нажали на точку-с-линией
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.tag == "Linkpoint") {
                //При нажатии на первую точку, включим рисовалку линии
                if(point1 == null)
                {
                    var point = hit.collider.gameObject;
                    if(point.GetComponent<LineRenderer>() != null && point.GetComponent<LineRenderer>().enabled == true)
                    {
                        point1 = point;
                        GameObject.Find("audioClick").GetComponent<SoundBt>().ClickSound();
                        RemoveFromAlreadyLinked(point1);
                    }
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            //Анимация перетаскивания линии за курсором мыши
            if(point1 != null && point2 == null && PrevMousePos.x != MousePos.x && PrevMousePos.y != MousePos.y)
            {
                //MouseButtonCount2++;
                //MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var point1_tr = point1.GetComponent<Transform>();
                var point1_lr = point1.GetComponent<LineRenderer>();
                
                point1_lr.positionCount = 2;
                point1_lr.SetPosition(0, point1_tr.position);
                point1_lr.SetPosition(1, new Vector3(MousePos.x, MousePos.y, 0f));

                //Если находимся рядом со свободной точкой, то свяжем 2 точки 
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if(hit.transform != null && hit.transform.CompareTag("Linkpoint"))
                {
                    var point = hit.transform.gameObject;
                    if(!IsAlreadyLinked(point) && point.GetComponent<LineRenderer>() != null && point.GetComponent<LineRenderer>().enabled == false)
                    {
                        point2 = point;
                        point1_lr.SetPosition(0, point1.GetComponent<Transform>().position);
                        point1_lr.SetPosition(1, point2.GetComponent<Transform>().position);
                        GameObject.Find("audioClick").GetComponent<SoundBt>().ChoicePlace();
                        alreadylinked.Add(new PointPair() { Point1 = point1, Point2 = point2});
                        point1 = null;
                        point2 = null;

                    }
                }
                PrevMousePos = MousePos;
            }
        }
        else
        {
            //MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Если протягивали линию, но отжали кнопку, то отменим линию
            if(point1 != null && point2 == null && PrevMousePos.x != MousePos.x && PrevMousePos.y != MousePos.y)
            {
                //MouseButtonCount3++;
                PrevMousePos = MousePos;
                point1.GetComponent<LineRenderer>().SetPosition(1, point1.GetComponent<Transform>().position);
                point1 = null;
                
            }  
        }
    }

    public void SaveAnswearAndLoadScene(string sceneName)
    {
        int correctPairCount = 0;
        foreach(var pair in alreadylinked)
        {
            bool isPairCorrect = false;
            foreach(var rpair in rightLinks)
            {
               //Каждую связанную пару точек, проверим в массиве правильных ответов
               if(rpair.Point1 == pair.Point1 && rpair.Point2 == pair.Point2)
               {
                  isPairCorrect = true;
               }
            }

            if(isPairCorrect)
            {
                //Запоминаем сколько правильных пар
                correctPairCount ++;
                //Подсветим правильную линию зеленым
                var lr = pair.Point1.GetComponent<LineRenderer>();
                lr.startColor = new Color(178f/255f,209f/255f,121f/255f);//green
                lr.endColor = new Color(178f/255f,209f/255f,121f/255f);    
                ScoreKeeper.GetScoreKeeper().Score += 1;
            }
            else
            {
                //Подсветим ошибочную линию красным
                var lr = pair.Point1.GetComponent<LineRenderer>();
                lr.startColor = new Color(183f/255f,80f/255f,84f/255f);//red
                lr.endColor = new Color(183f/255f,80f/255f,84f/255f);
            }

            //Блокируем кнопку OK
            pair.Point1.GetComponent<Collider2D>().enabled=false;
            var OK = EventSystem.current.currentSelectedGameObject;
            if(OK != null) { OK.GetComponent<Button>().enabled = false;}
        }

        //Если все пары правильные, то звук победы
        if(correctPairCount == rightLinks.Count())
        {
            StartCoroutine(GameObject.Find("audioClick").GetComponent<SoundBt>().winSound());
        }
        else
        {       
            StartCoroutine(GameObject.Find("audioClick").GetComponent<SoundBt>().errorSound());
        }

        //Прибавим счёт
        ScoreKeeper.GetScoreKeeper().Score += correctPairCount;

        StartCoroutine(waitOkSound(sceneName));
    }


    public IEnumerator waitOkSound(string sceneName)
    {
   
     yield return new WaitForSeconds(2f); 
     SceneManager.LoadScene(sceneName);
     
    }
}




[System.Serializable]
public class PointPair {
    public GameObject Point1;
    public GameObject Point2;
}

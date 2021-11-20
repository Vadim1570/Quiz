using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionLink1To1 : MonoBehaviour
{
    public string QuestionUniqueId;

    //Заранее заполненные правильные ответы
    public PointPair [] rightLinks;

    //Сюда запоминаем точки, которые пытаються связать между собой
    public GameObject point1 = null;
    public GameObject point2 = null;
    
    //Сюда запоминаем уже связанные между собой точки
    public List<PointPair> alreadylinkedPoints = new List<PointPair>();

    #region Private methods
    private bool IsAlreadyLinked(GameObject point)
    {
        foreach(var pair in alreadylinkedPoints)
        {
            if(point == pair.Point1 || point == pair.Point2)
            return true;
        }
        return false;
    }

    private void RemoveFromAlreadyLinked(GameObject point)
    {
        var pairToRemove = alreadylinkedPoints.FirstOrDefault(pair => point == pair.Point1 || point == pair.Point2);
        if (pairToRemove != null)
            alreadylinkedPoints.Remove(pairToRemove);
    }
    #endregion

    private void Awake()
    {
    }

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            //Если нажали на точку-с-линией
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.tag == "Linkpoint") {
                OnLinkpointClick(hit.collider.gameObject);
            }
            else
            {
                //Если протягивали линию, но нажали в пустое место, то отменим линию
                if(point1 != null && point2 == null)
                {
                    point1.GetComponent<LineRenderer>().SetPosition(1, point1.GetComponent<Transform>().position);
                    point1 = null;
                }  
            }
        }
        
        //Анимация перетаскивания линии за курсором мыши
        if(point1 != null && point2 == null)
        {
            var point1_tr = point1.GetComponent<Transform>();
            var point1_lr = point1.GetComponent<LineRenderer>();
            
            point1_lr.positionCount = 2;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point1_lr.SetPosition(0, point1_tr.position);
            point1_lr.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
        }
    }

    public void OnLinkpointClick(GameObject point)
    {
        //При нажатии на первую точку, включим рисовалку линии
        if(point1 == null)
        {
            if(point.GetComponent<LineRenderer>() != null && point.GetComponent<LineRenderer>().enabled == true)
            {
                point1 = point;
                RemoveFromAlreadyLinked(point1);
            }
        }
        else //У второй точки, отключим рисовалку линии
        {
            if(!IsAlreadyLinked(point))
            {
                if(point.GetComponent<LineRenderer>() == null || point.GetComponent<LineRenderer>().enabled == false)
                {
                    point2 = point;
                }
            }
        }

        //Если привязка двух точек произошла, то начертим линию между ними
        if(point1 != null && point2 != null)
        {
            var point1_lr = point1.GetComponent<LineRenderer>();
            point1_lr.SetPosition(0, point1.GetComponent<Transform>().position);
            point1_lr.SetPosition(1, point2.GetComponent<Transform>().position);

            alreadylinkedPoints.Add(new PointPair() { Point1 = point1, Point2 = point2});
            point1 = null;
            point2 = null;
        }
    }

    public void SaveAnswearAndLoadScene(string sceneName)
    {
        bool isAnswearCorrect = true;

        foreach(var pair in alreadylinkedPoints)
        {
            //Каждую связанную пару точек, проверим в массиве правильных ответов
            if(alreadylinkedPoints.Count(rpair => rpair.Point1 == pair.Point1 && rpair.Point2 == pair.Point2) == 0)
                isAnswearCorrect = false;
        }

        if(isAnswearCorrect)
        {
            ScoreKeeper.GetScoreKeeper().Score += 1;
        }

        SceneManager.LoadScene(sceneName);
    }
}

[System.Serializable]
public class PointPair {
    public GameObject Point1;
    public GameObject Point2;
}

using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

//https://www.youtube.com/watch?v=KHZFpRL3Xzc
public class QuestionDragAnswear : MonoBehaviour
{
    public List<StampWithProperty> stampsWithProperty = new List<StampWithProperty>();
    public GameObject selectedStamp;

    void Start()
    {
        foreach(var piece in  GameObject.FindGameObjectsWithTag("Stamp"))
        {
            //Запоминаем как расставлены кусочки пазла
            stampsWithProperty.Add(new StampWithProperty() { stampGameObject = piece, rightPosition = piece.transform.position });
            //Разбросать элементы пазла
            piece.transform.position = new Vector3(Random.Range(-2.18f, 1.32f), Random.Range(-2.72f, -3.72f));
        }
    }

    void Update()
    {
        //Запоминаем выбранный элемент пазла
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.transform != null && hit.transform.CompareTag("Stamp"))
            {
                selectedStamp = hit.transform.gameObject;
                //selectedStamp.GetComponent<SortingGroup>().sortingOrder = 1;
            }
        }

        //Забываем о выбранном элементе пазла
        if(Input.GetMouseButtonUp(0))
        {
            selectedStamp = null;
        }

        if(selectedStamp != null)
        {
            var selectedStampProp = stampsWithProperty.FirstOrDefault(pp => pp.stampGameObject == selectedStamp);

            //Двиагем выбранный элемент пазла
            var mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedStamp.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0f);

            //Магнитим выбранный элемент пазла
            foreach(var stampProp in stampsWithProperty)
            {
                if(Vector3.Distance(selectedStamp.transform.position, stampProp.rightPosition) < 0.5f)
                {
                    selectedStamp.transform.position = stampProp.rightPosition;
                    //selectedStamp.GetComponent<SortingGroup>().sortingOrder = 0;
                }
            }
        }
    }

    public void SaveAnswearAndLoadScene(string sceneName)
    {
        //Если все кусочки лежат в правильных местах, то прибавляем очко
        if(stampsWithProperty.Count() == stampsWithProperty.Count(p => p.stampGameObject.transform.position == p.rightPosition))
            ScoreKeeper.GetScoreKeeper().Score += 1;

        SceneManager.LoadScene(sceneName);
    }
}

[System.Serializable]
public class StampWithProperty {
    public GameObject stampGameObject;
    public Vector3 rightPosition;
}
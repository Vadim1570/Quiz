using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//https://www.youtube.com/watch?v=KHZFpRL3Xzc
public class QuestionPuzzle : MonoBehaviour
{
    public UnityEngine.UI.InputField AnwearInput;
    public string RightAnswear;
    public Material AnswearMaterial;
    public List<PieceWithProperty> rightLinks = new List<PieceWithProperty>();
    public GameObject selectedPiece;
    int OIL =1;
    public bool Selected;
    public Animator anim;
    bool collect = false; //тригер собраности пазлов, чтобы анимация Правильный ответ Марка не выводилась, если его не собирали


    public List<GameObject> alreadylinked = new List<GameObject>();

    #region Private methods
    private bool IsAlreadyLinked(GameObject piece)
    {
        foreach(var linkedPiece in alreadylinked)
        {
            if(linkedPiece == piece)
            return true;
        }
        return false;
    }

    private void RemoveFromAlreadyLinked(GameObject piece)
    {
        var pieceToRemove = alreadylinked.FirstOrDefault(go => go == piece);
        if (pieceToRemove != null)
            alreadylinked.Remove(pieceToRemove);
    }
    #endregion

    void Start()
    {
        //Прячем тестовое поле
        AnwearInput.gameObject.SetActive(false);

        foreach(var piece in  GameObject.FindGameObjectsWithTag("Puzzle"))
        {
            //Запоминаем как расставлены кусочки пазла
            rightLinks.Add(new PieceWithProperty() { gameObject = piece, position = piece.transform.position });
            //Разбросать элементы пазла
            piece.transform.position = new Vector3(Random.Range(-2.18f, 3f), Random.Range(-2.42f, -1f));
        }
    }

    void Update()
    {
        //Запоминаем выбранный элемент пазла
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.transform != null && hit.transform.CompareTag("Puzzle"))
            {
                selectedPiece = hit.transform.gameObject;
                RemoveFromAlreadyLinked(selectedPiece);
                Selected = true;
                GameObject.Find("audioClick").GetComponent<SoundBt>().ClickSound();
                selectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
                OIL++;
                 Debug.Log(selectedPiece.transform.position);

            }
        }

        //Забываем о выбранном элементе пазла
        if(Input.GetMouseButtonUp(0))
        {
            Selected = false;
            selectedPiece = null;
        }

        if(selectedPiece != null)
        {
            var selectedPieceProp = rightLinks.FirstOrDefault(pp => pp.gameObject == selectedPiece);

            //Двиагем выбранный элемент пазла
            var mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedPiece.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0f);

            //Магнитим выбранный элемент пазла
            if(Vector3.Distance(selectedPiece.transform.position, selectedPieceProp.position) < 0.2f)
            {
                selectedPiece.transform.position = selectedPieceProp.position;
                selectedPiece.GetComponent<SortingGroup>().sortingOrder = 0;

                if(!IsAlreadyLinked(selectedPiece))
                 { GameObject.Find("audioClick").GetComponent<SoundBt>().ChoicePlace(); }
                alreadylinked.Add(selectedPiece);

                //Отображаем тестовое поле, если все кусочки лежат в правильных местах, то выводим текстовое поле для ввода ответа
                if(rightLinks.Count() == rightLinks.Count(p => p.gameObject.transform.position == p.position))
                {
                    //AnwearInput.ActivateInputField();
                    collect = true;
                    AnwearInput.gameObject.SetActive(true);
                }
            }
        }
    }

    public void SaveAnswearAndLoadScene(string sceneName)
    {

        var OK = EventSystem.current.currentSelectedGameObject;
        if(OK != null) { OK.GetComponent<Button>().enabled = false;}


        if (collect == true)
        {
        var image = AnwearInput.GetComponent<Image>();
        image.material = AnswearMaterial;
            if (AnwearInput.text.ToLower() == RightAnswear.ToLower()) 
            {
                image.color = new Color(178f/255f,209f/255f,121f/255f);
                ScoreKeeper.GetScoreKeeper().Score += 1;
            }
            else 
            {
                image.color = new Color(183f/255f,80f/255f,84f/255f);
                anim.SetTrigger("play"); 
            }
        }
        StartCoroutine(LoadSceneAsync(sceneName));
    
    }

    public IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(sceneName);
    }
}

[System.Serializable]
public class PieceWithProperty {
    public GameObject gameObject;
    public Vector3 position;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Stage2 : MonoBehaviour
{
    [SerializeField]
    private bool peti;
    [SerializeField]
    private bool buah;

    [SerializeField]
    private List<string> DialogDesa;
    [SerializeField]
    private List<string> DialogAnakBuah;
    [SerializeField]
    private List<string> DialogSaudagar;

    private bool isA;
    private bool isB;
    private bool isC;

    [SerializeField]
    private bool isDone;

    public TextMeshProUGUI textComp;
    public float textSpeed;

    private int index;

    [SerializeField]
    private GameObject dialogBox;

    private string toTell;

    public List<Question> QnA;
    public int correctAns;

    public GameObject AnswerBox;
    public Text A, B, C;

    public bool isQuest;
    // Start is called before the first frame update
    void Start()
    {
        isA = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isQuest)
        {
            if (textComp.text == toTell)
            {
                dialogBox.SetActive(false);
                Time.timeScale = 1;


                if (isA && index == 1)
                {
                    index += 1;
                    toTell = DialogAnakBuah[index];
                    StartDialog();
                }

                if (isA && index == 3)
                {
                    index = 2;
                    toTell = DialogAnakBuah[index];
                    StartDialog();
                }

                if (isB && index == 0)
                {
                    index += 1;
                    toTell = DialogSaudagar[index];
                    StartDialog();
                }
                if (isB && index == 2)
                {
                    index = 1;
                    toTell = DialogSaudagar[index];
                    StartDialog();
                }

            }
            else
            {
                StopAllCoroutines();
                textComp.text = toTell;
            }


        }

        if (isA && index == 2 && textComp.text == toTell)
        {
            isQuest = true;
            A.text = QnA[0].Answers[0];
            B.text = QnA[0].Answers[1];
            C.text = QnA[0].Answers[2];
            correctAns = QnA[0].CorrectAnswer;
            AnswerBox.SetActive(true);
        }

        if (isB && index == 1 && textComp.text == toTell)
        {
            isQuest = true;
            A.text = QnA[1].Answers[0];
            B.text = QnA[1].Answers[1];
            C.text = QnA[1].Answers[2];
            correctAns = QnA[1].CorrectAnswer;
            AnswerBox.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AnakBuah")
        {
            GetComponent<PlayerController>().SetIdle();
            Dialog("anakbuah");
        }

        if (collision.gameObject.tag == "Saudagar")
        {
            GetComponent<PlayerController>().SetIdle();
            Dialog("saudagar");
        }

        if (collision.gameObject.tag == "loot")
        {
            collision.gameObject.GetComponent<lootButton>().SetButton(true);
            Debug.Log("muncul");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "loot")
        {
            collision.gameObject.GetComponent<lootButton>().SetButton(false);
            Debug.Log("sembunyi");
        }
    }

    private void Dialog(string type)
    {
        if (!peti && type == "anakbuah" && isA)
        {
            Debug.Log("Tidak Punya Peti");
            toTell = DialogAnakBuah[0];
            index = 0;
            StartDialog();
        }

        if (peti && type == "anakbuah" && isA)
        {
            Debug.Log("Punya Peti");
            toTell = DialogAnakBuah[1];
            index = 1;
            StartDialog();
        }

        if (buah && peti && type == "saudagar" && isB)
        {
            Debug.Log("Punya Buah");
            toTell = DialogSaudagar[0];
            index = 0;
            StartDialog();
        }
    }

    private void StartDialog()
    {

        Time.timeScale = 0;
        dialogBox.SetActive(true);
        textComp.text = string.Empty;
        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine()
    {
        foreach (char c in toTell.ToCharArray())
        {
            textComp.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    public void getLoot(string lootname)
    {
        if (lootname == "peti")
        {
            peti = true;
        }
        if (lootname == "buah")
        {
            buah = true;
        }
    }

    public void SetAnswer(int num)
    {

        dialogBox.SetActive(false);
        Time.timeScale = 1;

        AnswerBox.SetActive(false);
        isQuest = false;

        if (correctAns == num)
        {
            Debug.Log("Benar");
            if (isA)
            {
                index = 4;
                toTell = DialogAnakBuah[index];
                StartDialog();
                buah = true;
                isA = false;
                isB = true;
                return;
            }
            if (isB)
            {
                index = 3;
                toTell = DialogSaudagar[index];
                StartDialog();
                isDone = true;
                return;
            }
        }

        if (correctAns != num)
        {
            Debug.Log("Salah");
            if (isA)
            {
                index = 3;
                toTell = DialogAnakBuah[index];
                StartDialog();
                return;
            }
            if (isB)
            {
                index = 2;
                toTell = DialogSaudagar[index];
                StartDialog();
                return;
            }
        }


    }
}

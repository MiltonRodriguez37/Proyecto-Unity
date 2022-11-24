using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    #region Componentes del panel
    [Header("Panel de diálogos")]
    #pragma warning disable 0649
    [SerializeField]
    private GameObject _dialoguePnl;
    #pragma warning restore 0649
    private TextMeshProUGUI _dialogueTMP, _nameTMP, _nextTMP;
    private Button _nextBttn;
    #endregion

    #region Componentes NPC
    private string _name;
    private List<string> _dialogueList;
    private int _dialogueIdx;
    #endregion

    void Start()
    {
        #region Revisar existencia del panel
        if(_dialoguePnl==null)
        {
            Debug.LogWarning("Faltó asignar el dialoguePNL al inspector del DialogueController");
        }
        #endregion
        #region Obtener dialogueTMP 
        //primer hijo del dialoguePNL
        _dialogueTMP = _dialoguePnl.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (_dialogueTMP!=null)
        {
            _dialogueTMP.text = "DialogueTMP found here";
        }
        else
        {
            Debug.LogWarning("No se encuentra un componente TMP como hijo del panel");
        }
        #endregion
        #region Obtener NameTMP
        //primer hijo del segundo hijo
        _nameTMP = _dialoguePnl.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();

        if(_nameTMP!=null)
        {
            _nameTMP.text = "NameTMP found here";
        }
        else
        {
            Debug.LogWarning("No se encuentra un componente TMP como hijo del segundo hijo del panel");
        }
        #endregion
        #region Obtener next button
        //primer hijo del segundo hijo
        _nextBttn = _dialoguePnl.transform.GetChild(2).GetComponent<Button>();

        if (_nextBttn != null)
        {
            //Obtener hijo del botón
            _nextBttn.onClick.AddListener(delegate { ContinueDialogue(); });
            _nextTMP = _nextBttn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            if(_nextTMP != null)
            {
                _nextTMP.text = "NextTMP encontrado";
            }
            else
            {
                Debug.LogWarning("El botón no tiene texto");
            }
        }
        else
        {
            Debug.LogWarning("No se encuentró el botón como tercer hijo del panel");
        }
        #endregion


        _dialoguePnl.SetActive(false);
    }


    public void SetDialogue(string name, string[] dialogue)
    {
        Debug.Log("Se obtuvieron los datos de " + name);
        _name = name;
        _dialogueList = new List<string>(dialogue.Length);
        _dialogueList.AddRange(dialogue);
        _nameTMP.text = _name;
        _nextTMP.text = "Siguiente";
        _dialogueIdx = 0;
        ShowDialogue();
        _dialoguePnl.SetActive(true);
    }

    private void ShowDialogue()
    {
        Debug.Log("Diálogo: #" +_dialogueIdx);
        _dialogueTMP.text = _dialogueList[_dialogueIdx];
    }

    private void ContinueDialogue()
    {
        if(_dialogueIdx == _dialogueList.Count-1)//Se terminan los dialogos
        {
            Debug.Log("Se termina el diálogo con " + _name);
            _dialoguePnl.SetActive(false);
        }
        else if(_dialogueIdx == _dialogueList.Count - 2) //penúltimo diálogo
        {
            _nextTMP.text = "Salir";
            _dialogueIdx++;
            ShowDialogue();
        }
        else
        {
            _dialogueIdx++;
            ShowDialogue();
        }


    }
}

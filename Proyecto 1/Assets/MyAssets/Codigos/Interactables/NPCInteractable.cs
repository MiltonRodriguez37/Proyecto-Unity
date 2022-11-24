using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : Interactable
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private string[] _dialogue;
    private DialogueController _dialogueController;

    private void Start()
    {
        _dialogueController = FindObjectOfType<DialogueController>();//Singleton
        if (_dialogueController == null)
        {
            Debug.LogError("Falta un controlador de diálogo en la escena");
        } 
            
    }

    public override void Interact()
    {
        //base.Interact();
        Debug.Log("4.- Interactuando con un NPC");
        //Debug.Log("5.-" + _name + ": " + _dialogue[0]);
        if (_dialogueController!=null)
        {
            _dialogueController.SetDialogue(_name, _dialogue);
        }
    }


}

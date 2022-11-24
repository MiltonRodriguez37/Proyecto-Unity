using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator _playerAnimator;
        


    void Start()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
        if(_playerAnimator==null)
        {
            Debug.LogError("El jugador no tiene animator");
        }
    }


    public void SetSpeed(float velocity)
    {
        _playerAnimator.SetFloat("Speed", velocity);
    }

}

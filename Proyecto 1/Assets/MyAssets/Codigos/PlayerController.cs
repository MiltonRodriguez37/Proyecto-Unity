using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables movimiento
    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float _maxMovSpeed = 5f;
    [SerializeField]
    private bool _isRunning;
    [SerializeField]
    private float _horizontalInput, _forwardInput;

    #endregion

    #region Variables brinco
    [SerializeField]
    private bool _jumpRequest = false;
    [SerializeField]
    private float _jumpForce = 5;
    [SerializeField]
    private int _maxJumps = 3;
    [SerializeField]
    private int _availableJumps=0;
    #endregion

    private Rigidbody _playerRB;

    private PlayerAnimation _playerAnimation;


    // Start is called before the first frame update
    void Start()
    {

        #region Obtener RigidBody
        _playerRB = GetComponent<Rigidbody>();
        if (_playerRB == null)
        {
            Debug.LogWarning("El jugador no tiene componente de cuerpo rígido");
        }
        #endregion

        #region Obtener Player Animation
        _playerAnimation = GetComponent<PlayerAnimation>();
        if (_playerAnimation == null)
        {
            Debug.LogWarning("El jugador no tiene un script para animación");

        }
        else
        {
            _playerAnimation.SetSpeed(0);
        }
        #endregion

        _movementSpeed = _maxMovSpeed;
        _isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {

        #region Movimiento
        _horizontalInput = Input.GetAxis("Horizontal");
        _forwardInput = Input.GetAxis("Vertical");

        float velocity = Mathf.Max(Mathf.Abs(_horizontalInput), Mathf.Abs(_forwardInput));
        velocity *= _movementSpeed / _maxMovSpeed;
        _playerAnimation.SetSpeed(velocity);

        Vector3 movement = new Vector3(_horizontalInput, 0, _forwardInput);
        transform.Translate(movement*Time.deltaTime*_movementSpeed);
        #endregion

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {

            _isRunning = !_isRunning;
            if (_isRunning)
            {
                _movementSpeed = _maxMovSpeed;

            }
            else
            {
                _movementSpeed = _maxMovSpeed * 0.5f;
            }
        }

        #region Brinco
        if (Input.GetKeyDown(KeyCode.Space) && _availableJumps>0)
        {
            _jumpRequest = true;
        }
        #endregion
    }
    private void FixedUpdate()
    {
        if(_jumpRequest)
        {
            _playerRB.velocity = Vector3.up*_jumpForce;
            //Debug.Log("El personaje brincó");
            _availableJumps--;
            _jumpRequest = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _availableJumps = _maxJumps;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable")) 
        {
            Debug.Log("1.- Se encontró trigger con tag interactable");
            Interactable interacted = other.GetComponent<Interactable>();
            if (interacted != null)
            {
                Debug.Log("2.- Tiene un componente para interactuar");
                interacted.Interact();
            }
            else
            {
                Debug.LogWarning("El objeto a interactuar no tiene script");
            }
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables cámara
    [SerializeField]
    private Transform _player, _playerCamera, _focusPoint;
    [SerializeField]
    private float _cameraHeight;
    #endregion

    #region Variables Zoom
    [SerializeField]
    private float _zoom = -10, _zoomSpeed=2.5f;
    [SerializeField]
    private float _zoomMin = -15, _zoomMax = 0;
    #endregion

    #region Rotación cámara
    [SerializeField]
    private float _camRotX, _camRotY;
    [SerializeField]
    private float _cameraSensitivity = 2;
    #endregion

    void Start()
    {
        #region Comprobar existencia de componentes
        if(_player == null)
        {
            Debug.LogWarning("El jugador no fue asignado al controlador de la cámara");
        }
        if (_playerCamera == null)
        {
            Debug.LogWarning("La cámara principal no fue asignado al controlador de la cámara");
        }
        if (_focusPoint == null)
        {
            Debug.LogWarning("El punto focal no fue asignado al controlador de la cámara");
        }
        #endregion

        #region Asignar parentesco
        _playerCamera.SetParent(_focusPoint);
        _focusPoint.SetParent(_player);
        #endregion

        #region Asignar posición y rotación
        _focusPoint.localPosition = new Vector3(0,_cameraHeight,0);
        _focusPoint.localRotation = Quaternion.Euler(0,0,0);
        _playerCamera.localPosition = new Vector3(0, 0, _zoom);
        _playerCamera.localRotation = Quaternion.Euler(0, 0, 0);
        _playerCamera.LookAt(_player);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region Zoom
        _zoom += Input.GetAxis("Mouse ScrollWheel")*_zoomSpeed;
        _zoom = Mathf.Clamp(_zoom,_zoomMin, _zoomMax);
        _playerCamera.localPosition = new Vector3(0, 0, _zoom);
        _playerCamera.LookAt(_player);
        #endregion

        #region Rotar cámara
        if (Input.GetMouseButton(0)) //Click izquierdo
        {
            _camRotX += Input.GetAxis("Mouse X") * _cameraSensitivity; //Yaw
            _camRotY += Input.GetAxis("Mouse Y") * _cameraSensitivity; //Pitch
            _focusPoint.localRotation = Quaternion.Euler(_camRotY, 0, 0);
            _player.localRotation = Quaternion.Euler(0, _camRotX, 0);
        }
        #endregion
    }
}

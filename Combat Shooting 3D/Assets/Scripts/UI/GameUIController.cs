using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameUIController : MonoBehaviour
{
    #region Serialized Fields
    [Header("Buttons")]
    [SerializeField] private Button aimButton;
    [SerializeField] private Button shootButton;
    [SerializeField] private Button pauseButton;

    [Header("Controllers")]
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private PauseController pauseController;
    #endregion
    
    #region Events
    public UnityEvent OnShoot;
    public UnityEvent OnAim;
    public UnityEvent OnPause;
    #endregion

    private void OnEnable()
    {
        shootButton.onClick.AddListener(OnShootButtonClick);
        aimButton.onClick.AddListener(OnAimButtonClick);
        pauseButton.onClick.AddListener(OnPauseButtonClick);

        OnShoot.AddListener(weaponController.HandleShootEvent);
        OnAim.AddListener(weaponController.HandleAimEvent);
        OnPause.AddListener(pauseController.HandlePauseEvent);
    }

    private void OnShootButtonClick()
    {
        Debug.Log("SHOOT BUTTON CLICK");
        OnShoot?.Invoke();
    }

    private void OnAimButtonClick()
    {
        Debug.Log("AIM BUTTON CLICK");
        OnAim?.Invoke();
    }

    private void OnPauseButtonClick()
    {
        Debug.Log("PAUSE BUTTON CLICK");
        OnPause?.Invoke();
    }

    private void OnDisable()
    {
        shootButton.onClick.RemoveListener(OnShootButtonClick);
        aimButton.onClick.RemoveListener(OnAimButtonClick);
        pauseButton.onClick.RemoveListener(OnPauseButtonClick);
        
        OnShoot.RemoveListener(weaponController.HandleShootEvent);
        OnAim.RemoveListener(weaponController.HandleAimEvent);
        OnPause.RemoveListener(pauseController.HandlePauseEvent);
    }
}

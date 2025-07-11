using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Inject] public Player Player { get; set; }

    [SerializeField] private float _speed = 5f;

    private CharacterController _characterController;
    private Vector3 _moveDirection;

    [Inject]
    private EventBus _eventBus;
    private bool _isGameEnded;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _eventBus.OnGameEnded += EndGame;
    }

    private void Update()
    {
        if (_isGameEnded)
            return;

        HandleMovement();
    }

    private void HandleMovement()
    {
        if (!Player.CanMove)
        {
            Player.Animator.SetBool("IsMoving", false);
            return;
        }

        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _speed;
        Player.Animator.SetBool("IsMoving", _moveDirection.magnitude > 0);
        _moveDirection = transform.TransformDirection(_moveDirection);

        _moveDirection.y = Physics.gravity.y; // Apply gravity

        ClampDirecion();

        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    private void ClampDirecion()
    {
        if (transform.position.x > 6.5f || transform.position.x < -6.5f)
            _moveDirection.x = 0;
    }

    private void EndGame()
    {
        _isGameEnded = true;
    }

    private void OnDisable()
    {
        _eventBus.OnGameEnded -= EndGame;
    }
}

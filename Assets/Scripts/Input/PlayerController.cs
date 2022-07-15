using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 1;
    [SerializeField] private float rotationSpeed = 100;
    private GameControls inputActions;
    private InputAction turn;
    private Rigidbody rigidBody;

    private void Awake()
    {
        inputActions = new GameControls();
    }

    public void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        turn = inputActions.Player.Turn;
        turn.Enable();

        //inputActions.Player.Turn.performed += TurnCharacter;
        inputActions.Player.Turn.Enable();
    }

    private void TurnCharacter(InputAction.CallbackContext obj)
    {
        Debug.Log("Performed" + obj.ReadValue<Vector3>());


    }

    private void OnDisable()
    {
        turn.Disable();
        inputActions.Player.Turn.Disable();
    }

    public void Update()
    {
        Vector3 rotation = new Vector3(0, turn.ReadValue<Vector3>().x, 0);
        this.transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
        float ySpeed = rigidBody.velocity.y;
        Vector3 movement = transform.forward.normalized * speed;
        movement.y = ySpeed;
        rigidBody.velocity = movement;
    }
}

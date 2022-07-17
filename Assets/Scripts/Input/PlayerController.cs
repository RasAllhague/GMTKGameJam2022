using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 1;
    [SerializeField] private float rotationSpeed = 100;
    [SerializeField] private float dieY = -10;
    private float speedMultiplier = 1;
    private GameControls inputActions;
    private InputAction turn;
    public event Action FallOutOfBounce;
    private AudioSource audioSource;
    public AudioClip collisionWallSound;
    private Vector3 lastPosition;

    private void Awake()
    {
        inputActions = new GameControls();
    }

    private void OnEnable()
    {
        turn = inputActions.Player.Turn;
        turn.Enable();

        //Optional, if single action is needed (e.g. for jumps)
        //inputActions.Player.Turn.performed += TurnCharacter;
        inputActions.Player.Turn.Enable();
    }

    //private void TurnCharacter(InputAction.CallbackContext obj)
    //{
    //    Debug.Log("Performed" + obj.ReadValue<Vector3>());
    //}

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            Debug.LogWarning("Player has no AudioSource");
        }
        if(collisionWallSound == null)
        {
            Debug.LogWarning("Player has no CollisionWall SFX");
        }
    }

    private void OnDisable()
    {
        turn.Disable();
        inputActions.Player.Turn.Disable();
    }

    public void Update()
    {
        lastPosition = transform.position;

        //Player rotation
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y + turn.ReadValue<Vector3>().x * rotationSpeed * Time.deltaTime, 0);
        //Player movement
        Vector3 movement = transform.forward.normalized * speed;
        transform.position += movement * speedMultiplier * Time.deltaTime;
        if (transform.position.y < dieY)
            OnFallOutOfBounce();

        ToggleStepSound(IsMoving());
    }

    private bool IsMoving()
    {
        return lastPosition != transform.position;
    }

    private void ToggleStepSound(bool enable)
    {
        if(!audioSource.isPlaying && enable)
        {
            audioSource.Play();
        }
        else if(!enable)
        {
            audioSource.Stop();
        }
    }

    protected void OnFallOutOfBounce()
    {
        FallOutOfBounce?.Invoke();
    }

    public void IncreaseSpeed()
    {
        speedMultiplier += 2f;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("GameWall"))
        {
            //Rotates the player by 180°
            Vector3 eulerRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerRotation.x, 180 + eulerRotation.y + turn.ReadValue<Vector3>().x * rotationSpeed * Time.deltaTime * speedMultiplier, 0);

            audioSource.PlayOneShot(collisionWallSound);
        }
    }

        
}

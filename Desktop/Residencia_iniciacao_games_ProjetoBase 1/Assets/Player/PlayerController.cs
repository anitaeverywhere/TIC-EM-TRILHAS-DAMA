using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Animator animator;

    private PlayerInputActions inputActions;

    // Start is called before the first frame update
    void Start()
    {
        inputActions = new PlayerInputActions();
        inputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleButtonTriggered();
    }

    private void HandleButtonTriggered()
    {
        float cheerButtonValue = inputActions.Player.Cheer.ReadValue<float>();
        if (cheerButtonValue > 0)
        {
            animator.SetTrigger("t_cheer");
        }
    }

    private void HandleMovement()
    {
        Vector2 direction = inputActions.Player.Movement.ReadValue<Vector2>().normalized;
        if (direction.magnitude != 0)
        {
            // Ele está andando
            Vector3 moveDirection = new(direction.x, 0, direction.y);
            float moveDistance = speed * Time.deltaTime;

            // Rotaciona o personagem para a direção que está andando
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

            // Move o personagem para a nova posição
            transform.position += moveDistance * moveDirection;

            // Muda para animação de Walk
            animator.SetBool("b_walk", true);
        }
        else
        {
            // Ele está em Idle

            // Muda para animação de Walk
            animator.SetBool("b_walk", false);
        }
    }
}

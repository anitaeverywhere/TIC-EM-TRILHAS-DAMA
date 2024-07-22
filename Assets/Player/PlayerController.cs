using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Shoot Variables
    [SerializeField] GameObject arrow;
    private bool isAttacking;
    private Vector3 screenPosition;
    private Vector3 worldPosition;
    private Plane plane = new Plane(Vector3.down, 1);
    #endregion

    #region Original Variables
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Animator animator;
    private PlayerInputActions inputActions;

    #endregion

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
        HandleAttack();
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
            animator.SetBool("b_walk", false);
        }
    }

    #region Attacks Methods
    void HandleAttack()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            screenPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            if (plane.Raycast(ray, out float distance))
            {
                worldPosition = ray.GetPoint(distance);
            }
            Vector3 lookPosition = new Vector3(worldPosition.x, 0, worldPosition.z);
            transform.LookAt(lookPosition);
            rotationSpeed = 0.1f;
            Quaternion arrowRotation = Quaternion.Euler(-90, 0, transform.rotation.eulerAngles.y);
            GameObject arrowSpawner = transform.Find("ArrowSpawner").gameObject;
            Arrow arrowScript = Instantiate(arrow, arrowSpawner.transform.position, arrowRotation).GetComponent<Arrow>();
            arrowScript.toGo = worldPosition;
            isAttacking = true;
            animator.SetTrigger("t_attacking");
            Invoke("HandleResetAttack", 0.5f);
        }
    }

    void HandleResetAttack()
    {
        isAttacking = false;
        rotationSpeed = 25;
    }
    #endregion
}

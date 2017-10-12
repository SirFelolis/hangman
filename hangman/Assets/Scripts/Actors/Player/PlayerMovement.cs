using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Don't use this it's deprecated
 */

public enum PSTATE
{
    SIMULATED,
    MOVING,

    STATE_COUNT
};

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public PSTATE state;

    [SerializeField]
    private int stepSize = 1;

    [SerializeField]
    private LayerMask whatIsGround;

    private Rigidbody2D rb2d;

    [SerializeField]
    private float moveForce;

    private Vector3 feignTransform = new Vector3();

    private int counterX;
    private int counterY;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        state = PSTATE.MOVING;
    }

    private void Start()
    {
        feignTransform = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && state == PSTATE.MOVING)
        {
            state = PSTATE.SIMULATED;
        }
        if (Input.GetKeyUp(KeyCode.Space) && state == PSTATE.SIMULATED)
        {
            feignTransform = transform.position;
            state = PSTATE.MOVING;
        }


        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 3, Vector2.left, 0, 1 << LayerMask.NameToLayer("Interactable"));
        if (hit)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (Input.GetButtonDown("Use"))
            {
                if (interactable != null)
                {
                    interactable.CheckInteraction(transform);
                }
            }
        }

        if (state == PSTATE.MOVING)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.bodyType = RigidbodyType2D.Kinematic;

            feignTransform = new Vector2(Mathf.Round(feignTransform.x), Mathf.Round(feignTransform.y));

            Vector2 moveDirection = new Vector2();

            float time = 8;

            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (Mathf.Abs(input.x) > 0.01f)
                counterX++;
            else
                counterX = (int)time - 1;

            if (Mathf.Abs(input.y) > 0.01f)
                counterY++;
            else
                counterY = (int)time - 1;

            if (counterX % time == 0)
            {
                moveDirection.x += input.x;
            }

            if (counterY % time == 0)
            {
                moveDirection.y += input.y;
            }

            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // HACK HACK HACK, VERY TERRIBLE HACK.
            // THIS SHOULD BE DONE IN A LOOP OR SOMETHING, BUT I'LL FIX IT LATER HACK HACK HACK.
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            hit = Physics2D.Raycast(feignTransform, Vector2.down, 3, whatIsGround); // Prevent player from moving into ground
            if (hit)
            {
                if (hit.distance <= stepSize && moveDirection.y < 0) moveDirection.y = 0;
            }
            else if (!hit && !GameManager.instance.inOverworld)
            {
                if (moveDirection.y > 0) moveDirection.y = 0;
            }
            hit = Physics2D.Raycast(feignTransform, -Vector2.down, stepSize, whatIsGround); // Prevent player from moving into ceiling
            if (hit)
            {
                if (moveDirection.y > 0) moveDirection.y = 0;
            }
            hit = Physics2D.Raycast(feignTransform, Vector2.right, stepSize, whatIsGround); // Prevent player from moving into right walls
            if (hit)
            {
                if (moveDirection.x > 0) moveDirection.x = 0;
            }
            hit = Physics2D.Raycast(feignTransform, -Vector2.right, stepSize, whatIsGround); // Prevent player from moving into left walls
            if (hit)
            {
                if (moveDirection.x < 0) moveDirection.x = 0;
            }


            feignTransform += (Vector3)moveDirection;
            transform.position = Vector3.Lerp(transform.position, feignTransform, 0.3f);
        }
        else if (state == PSTATE.SIMULATED)
        {
            rb2d.bodyType = RigidbodyType2D.Dynamic;

            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            Vector2 desiredVelocity = input * moveForce;
            rb2d.AddForce(desiredVelocity * Time.deltaTime);

            hit = Physics2D.Raycast(transform.position, Vector2.down, 1.02f, whatIsGround); // Check if we are on the ground
            if (hit)
                rb2d.drag = 99;
            else
                rb2d.drag = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(feignTransform, Vector2.down * 1);
        Gizmos.DrawRay(feignTransform, -Vector2.down * 1);
        Gizmos.DrawRay(feignTransform, Vector2.right * 1);
        Gizmos.DrawRay(feignTransform, -Vector2.right * 1);
    }

}

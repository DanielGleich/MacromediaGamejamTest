using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float maxMoveSpeed;
    [SerializeField] FieldOfView fovHandler;
    Rigidbody2D rb;
    PlayerInputs controls;
    Vector2 vel;


   // Start is called before the first frame update

    private void OnEnable()
    {
        controls.Overworld.Enable();
    }

    private void OnDisable()
    {
        controls.Overworld.Disable();
    }
    private void Awake()
    {
        controls = new PlayerInputs(); 
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controls.Overworld.Move.IsPressed())
        {
            Move((controls.Overworld.Move.ReadValue<Vector2>()));
        }
        else {
            rb.velocity = Vector2.SmoothDamp(rb.velocity, Vector2.zero, ref vel, .2f);
        }
        Vector3 mousePos = rb.transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90;
        transform.Rotate(0, 0, angle);

        if (rb.velocity.magnitude > maxMoveSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxMoveSpeed;
        }

        fovHandler.SetOrigin(rb.position);
        fovHandler.SetAimDirection(Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(rb.position.x, rb.position.y, 0));
    }

    private void Move(Vector2 mov)
    {
        //rb.transform.position += (new Vector3(mov.x, mov.y, 0) * moveSpeed * Time.deltaTime);
        rb.AddForce(new Vector2(mov.x, mov.y) * moveSpeed);
    }
}

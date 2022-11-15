using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
    Rigidbody rb;
    public Animator anim;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        // Get the rigidbody on this.
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (speedOverrides.Count > 0)
            speed = speedOverrides[speedOverrides.Count - 1]();

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        anim.SetBool("isWalking",Input.GetAxisRaw("Vertical") != 0);
        // Apply movement.
        rb.velocity = transform.rotation * new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.y);
            
    }
}
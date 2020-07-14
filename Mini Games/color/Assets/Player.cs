using UnityEngine;

public class Player : MonoBehaviour {

    public float jumpForce = 10;

    public Rigidbody2D rb;

    private void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

}

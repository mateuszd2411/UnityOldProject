using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 4f;
    public Rigidbody2D rb;
    private float movement = 0f;

	
	// Update is called once per frame
	void Update () {
        movement = Input.GetAxisRaw("Horizontal") * speed;
	}

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector2(movement * Time.fixedDeltaTime, 0f));
    }
}

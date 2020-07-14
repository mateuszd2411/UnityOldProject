using UnityEngine;

public class Car : MonoBehaviour {

    public Rigidbody2D rb;

    
    public float minSpeed = 8;
    public float maxSpeed = 12;

    float speed = 1f; 

    private void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void FixedUpdate () {
        Vector2 forward = new Vector2(transform.right.x, transform.right.y);
        rb.MovePosition(rb.position + forward * Time.fixedDeltaTime * speed);
	}
}

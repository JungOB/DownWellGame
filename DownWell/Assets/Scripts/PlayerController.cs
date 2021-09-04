using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody;

    public LayerMask groundLayerMask;

    public float speed = 5f;
    public float jumpSpeed = 5f;
    public float gravity = 1f;
    public float maxFallSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.gravityScale = gravity;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.gravityScale = gravity;
        //Debug.Log(rigidbody.velocity.y);

        if (rigidbody.velocity.y <= -maxFallSpeed) rigidbody.velocity = new Vector2(rigidbody.velocity.x, -maxFallSpeed);

        HorizontalMove();

        if (Input.GetButton("Jump") && CheckTileUnderPlayer(groundLayerMask))
            Jump();

        if (Input.GetButtonDown("Fire1"))
            gravity = 3f;

        if (Input.GetKeyDown(KeyCode.B))
            Attack();
    }

    void HorizontalMove()
    {
        float h = Input.GetAxis("Horizontal");

        rigidbody.velocity = new Vector2(h * speed, rigidbody.velocity.y);
    }

    void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
    }

    bool CheckTileUnderPlayer(LayerMask checkLayer)
    {
        float rayDistance = .1f;

        Vector2 origin = new Vector2(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().size.x / 2);
        RaycastHit2D[] results = Physics2D.RaycastAll(origin, Vector2.down, rayDistance, checkLayer);

        Debug.DrawRay(origin, Vector3.down * rayDistance, Color.green);

        if (results.Length > 0) return true;

        return false;
    }

    void Attack()
    {
        float rayDistance = .1f;

        Vector2 origin = new Vector2(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().size.x / 2);
        RaycastHit2D[] results = Physics2D.RaycastAll(origin, Vector2.down, rayDistance);

        foreach(var result in results)
        {
            if (result.transform.tag == "Block")
                Destroy(result.transform.gameObject);
        }
    }
}

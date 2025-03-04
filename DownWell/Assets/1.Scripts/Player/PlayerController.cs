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

    [Space()]
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    float horizontalRaySpacing;
    float verticalRaySpacing;

    RaycastOrigins raycastOrigins;
    struct RaycastOrigins
    {
        public Vector2 bottomLeft, topLeft;
        public Vector2 bottomRight, topRight;
    }

    bool grounded = true;
    bool jumping = false;

    bool shootable = true;
    bool shooting = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.gravityScale = gravity;

        CalculateRaySpacing();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.gravityScale = gravity;

        // �ִ� �ӵ�
        if (rigidbody.velocity.y <= -maxFallSpeed) rigidbody.velocity = new Vector2(rigidbody.velocity.x, -maxFallSpeed);

        HorizontalMove();

        grounded = GroundCollision();

        Jump();

        Shoot();
    }

    private void FixedUpdate()
    {
        //HorizontalMove();
    }

    void Shoot()
    {
        if (Input.GetButtonUp("Jump"))
        {
            shootable = true;
        }

        if (shootable && Input.GetButtonDown("Jump"))
        {
            shooting = true;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        }
        if (shooting && Input.GetButton("Jump"))
        {
            GetComponent<PlayerCombat>().Shoot();
        }

        if (grounded)
        {
            jumping = false;
            shootable = false;
            shooting = false;
        }
        else
        {
            shootable = true;
        }
    }

    void HorizontalMove()
    {
        UpdateRaycastOrigins();

        float h = Input.GetAxis("Horizontal");

        //Debug.Log(HorizontalCollisions());
        //rigidbody.velocity = new Vector2(h * speed, rigidbody.velocity.y);
        if(!HorizontalCollisions()) transform.position += Vector3.right * speed * h * Time.deltaTime;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
            jumping = true;
        }

        if (rigidbody.velocity.y > 0 && Input.GetButtonUp("Jump"))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y / 2);
            jumping = false;
        }
    }

    public void LeapOff(float stepUpSpeed)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, stepUpSpeed);
        jumping = true;
    }

    public void KnuckBack(float knuckBackSpeed, int direction)
    {
        //rigidbody.velocity = new Vector2(knuckBackSpeed * direction, rigidbody.velocity.y + knuckBackSpeed);

        //StartCoroutine(KnuckBacking(knuckBackSpeed, direction));

        rigidbody.AddForce(new Vector2(knuckBackSpeed * direction, knuckBackSpeed), ForceMode2D.Impulse);
    }

    bool GroundCollision()
    {
        for(int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.bottomLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, .1f, groundLayerMask);

            Debug.DrawRay(rayOrigin, Vector2.down * .1f, Color.red);

            if (hit) return true;
        }

        return false;
    }

    bool OneSidePlatformCollision()
    {
        for(int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, .1f);

            Debug.DrawRay(rayOrigin, Vector2.up * .1f, Color.red);

            if (hit) return true;
        }

        return false;
    }

    bool HorizontalCollisions()
    {
        float directionX = Mathf.Sign(Input.GetAxis("Horizontal"));

        for(int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, .1f, groundLayerMask);
            //Debug.Log(hit.transform.name);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * .1f, Color.red);

            if (hit) return true;
        }

        return false;
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = GetComponent<BoxCollider2D>().bounds;

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = GetComponent<BoxCollider2D>().bounds;

        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);

        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
    }
}

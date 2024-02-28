using UnityEngine;

public abstract class BaseMovement : MonoBehaviour
{
    [Header("Base Attributes")]
    [SerializeField] protected float MovementSpeed;

    [Header("Walkable Area")]
    [SerializeField] protected float minX;
    [SerializeField] protected float maxX;
    [SerializeField] protected float minY;
    [SerializeField] protected float maxY;

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected bool isWalking = false;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    protected void HandleMovement(Vector2 movementInput)
    {
        rb.velocity = movementInput.normalized * MovementSpeed;
        WalkableArea();       
    }

    protected void WalkableArea()
    {
        Vector2 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }

    public bool IsWalking()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        return isWalking;
    }

    protected void SpriteFlip(Vector2 movementInput)
    {
        if (movementInput.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (movementInput.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    protected abstract Vector2 CalculateMovementInput();
}

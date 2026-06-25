using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Visual & Animation Setup")]
    public SpriteRenderer targetSpriteRenderer; 

    [Header("Weapon & Hand Renderers")]
    public SpriteRenderer pistolRenderer;
    public SpriteRenderer rightHandRenderer;
    public SpriteRenderer leftHandRenderer;

    [Header("Weapon Reference")]
    public Transform weaponPivot;

    [Header("Character Sprites")]
    public Sprite frontLeftSprite;
    public Sprite backLeftSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (targetSpriteRenderer == null)
        {
            targetSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        UpdateSpriteAndLayering();
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }

    void UpdateSpriteAndLayering()
    {
        if (weaponPivot == null || targetSpriteRenderer == null) return;

        float angle = weaponPivot.eulerAngles.z;
        if (angle > 180) angle -= 360;

        // --- 2D TWO-HAND DEPTH LAYERING ---
        // Base Body Layer = 5
        
        if (angle > 0 && angle < 180)
        {
            // Case A: Gun is pointing UP (Player faces BACK)
            targetSpriteRenderer.sprite = backLeftSprite;

            // Put the pistol and hands neatly behind the body
            if (pistolRenderer != null) pistolRenderer.sortingOrder = 3;
            if (rightHandRenderer != null) rightHandRenderer.sortingOrder = 4;
            if (leftHandRenderer != null) leftHandRenderer.sortingOrder = 4;
        }
        else
        {
            // Case B: Gun is pointing DOWN (Player faces FRONT)
            targetSpriteRenderer.sprite = frontLeftSprite;

            // Put the pistol and hands out in front of the body
            if (pistolRenderer != null) pistolRenderer.sortingOrder = 7;
            if (rightHandRenderer != null) rightHandRenderer.sortingOrder = 8; // Right hand on top grip
            if (leftHandRenderer != null) leftHandRenderer.sortingOrder = 6;  // Left hand supporting underneath
        }

        // Handle horizontal flipping
        if (angle > -90f && angle < 90f)
        {
            targetSpriteRenderer.flipX = false;
        }
        else
        {
            targetSpriteRenderer.flipX = true;
        }
    }
}
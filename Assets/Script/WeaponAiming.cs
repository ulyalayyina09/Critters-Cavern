using UnityEngine;

public class WeaponMovementAim : MonoBehaviour
{
    [Header("Blender/MMD Style Easing")]
    [Tooltip("Click this to open the graph. Add as many keyframe dots as you want!")]
    public AnimationCurve rotationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    
    [Tooltip("Total time (in seconds) for the full rotation transition.")]
    public float transitionDuration = 0.3f;

    [Header("Weapon Animation & Shooting")]
    [Tooltip("Drag your Player object here (the one with the Animator component).")]
    public Animator weaponAnimator;

    [Tooltip("How fast the gun can fire (seconds between shots).")]
    public float fireRate = 0.25f;
    private float nextFireTime = 0f;

    // --- NEW KEYBIND SETTINGS ---
    [Header("Controls")]
    [Tooltip("Choose the key or mouse button used to shoot.")]
    public KeyCode shootKeybind = KeyCode.J; // Default is Left Mouse Button

    [Header("Projectile Settings")]
    public GameObject bulletPrefab;
    public Transform muzzlePoint;

    private Quaternion startingRotation;
    private Quaternion targetRotation;
    private float timeElapsed;
    private Vector2 aimDirection = Vector2.right;
    private Vector2 lastInputDirection = Vector2.right;

    void Start()
    {
        startingRotation = transform.rotation;
        targetRotation = transform.rotation;
        timeElapsed = transitionDuration; 
    }

    void Update()
    {
        // 1. Get movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 inputDirection = new Vector2(moveX, moveY);

        // 2. CRUCIAL FIX: Only update the angle IF the player is actually pressing a button!
        // inputDirection.sqrMagnitude > 0.01f checks if the joystick/keys are being touched.
        if (inputDirection.sqrMagnitude > 0.01f)
        {
            // If the direction changed from our last stored movement direction...
            if (inputDirection.normalized != lastInputDirection)
            {
                lastInputDirection = inputDirection.normalized;
                aimDirection = inputDirection.normalized;

                startingRotation = transform.rotation;
                
                float targetAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
                
                timeElapsed = 0f; // Reset timeline clock
            }
        }
        // If the player let go of all keys (inputDirection is 0, 0), the script skips 
        // this entire block, keeping targetRotation locked at the last diagonal vector!

        // 3. Step through the unified timeline
        if (timeElapsed < transitionDuration)
        {
            timeElapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(timeElapsed / transitionDuration);
            float curveEvaluatedProgress = rotationCurve.Evaluate(progress);
            transform.rotation = Quaternion.Slerp(startingRotation, targetRotation, curveEvaluatedProgress);
        }

        // --- VISUAL FLIP LOGIC ---
        Vector3 localScale = Vector3.one;
        if (transform.right.x < 0)
        {
            localScale.y = -1f; 
        }
        else
        {
            localScale.y = 1f;  
        }
        transform.localScale = localScale;

        HandleShooting();
    }

    void HandleShooting()
    {
        // --- UPDATED: Uses the dynamic keybind field instead of hardcoded Input.GetMouseButtonDown(0) ---
        if (Input.GetKeyDown(shootKeybind) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            if (weaponAnimator != null)
            {
                weaponAnimator.ResetTrigger("Shoot");
                weaponAnimator.Play("Shoot_PSTL", -1, 0f);
            }

            if (bulletPrefab != null && muzzlePoint != null)
            {
                Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
            }
        }
    }
}
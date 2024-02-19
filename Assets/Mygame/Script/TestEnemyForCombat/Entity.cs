using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float facingDr { get; private set; } = 1;
    protected bool rightFlip = true;
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFx fx { get; private set; }


    [Header("colision infor")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatISGround;
    [SerializeField] protected LayerMask whatIsWall;
    public Transform attackCheck;
    public float attackCheckRadius;
    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected Vector2 knockbackPower = new Vector2(7, 12);
    [SerializeField] protected Vector2 knockbackOffset = new Vector2(.5f, 2);
    [SerializeField] protected float knockbackDuration = .07f;
    protected bool isKnocked;

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
        anim = GetComponentInChildren<Animator>();
        fx=GetComponent<EntityFx>();
    }
    protected virtual void Update()
    {

    }
    public virtual bool isGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatISGround);
    public virtual bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDr, wallCheckDistance, whatISGround);


    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    public void Flip()
    {
        facingDr = facingDr * -1;
        rightFlip = !rightFlip;
        transform.Rotate(0, 180, 0);
    }
    public void FlipController(float _x)
    {
        if (_x > 0 && !rightFlip)
        {
            Flip();
        }
        else if (_x < 0 && rightFlip)
        {
            Flip();
        }
    }
    public void SetZeroVelocity()
    {   if (isKnocked)
        {
            return;
        }
        rb.velocity = new Vector2(0, 0);
    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {   if(isKnocked)
        {
            return;
        }
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    public virtual void Damage()
    {
        Debug.Log("outch");
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockBack");
    }
    protected virtual IEnumerator HitKnockBack()
    {
        isKnocked = true;
        rb.velocity= new Vector2 (knockbackDirection.x*-facingDr, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked=false;
    }
}

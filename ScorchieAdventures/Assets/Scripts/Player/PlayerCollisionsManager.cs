using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles the player's colliders and collisions events, such as the Damage System
*/
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerCollisionsManager : MonoBehaviour
{
    private BoxCollider2D col;
    private Rigidbody2D rig;
    private PlayerStatsManager statsManager;
    private PhysicsMaterial2D physicsMaterial;
    public InteractiveObject interactiveObject;

    [Header("Slope")]
    private Vector2 colliderSize;
    [SerializeField] private float slopeCheckDistance;
    [SerializeField] private LayerMask groundLayer;
    [HideInInspector] public float slopeDownAngle;
    private float slopeDownAngleOld;
    private Vector2 slopeNormalPerp;
    private bool isOnSlope;

    [Header("Dash Collision")]
    public Transform dashCollisionPoint;
    [SerializeField] private float dashCollisionRadius;

    private bool canCollectCoins;

    [Header("Top Collision")]
    [SerializeField] private Vector2 topColliderSize;
    [SerializeField] private Transform topCollisionPoint;
    private bool isCollidingOnTop;
    private List<Collider2D> topCollisions;
    private Collider2D[] topCollisionsArray;


    [Header("Botton Collision")]
    [SerializeField] private Vector2 bottomColliderSize;
    [SerializeField] private Transform bottomCollisionPoint;
    private bool isCollidingOnBottom;
    [SerializeField] private List<Collider2D> bottomCollisions;
    private Collider2D[] bottomCollisionsArray;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        physicsMaterial = new PhysicsMaterial2D(col.sharedMaterial.name + " (Instance)");
        rig.sharedMaterial = physicsMaterial;
        col.sharedMaterial = physicsMaterial;
        physicsMaterial.friction = 0f;
    }

    private void Start()
    {
        statsManager = PlayerStatsManager.instance;
        colliderSize = col.size;
        topCollisions = new List<Collider2D>();
        bottomCollisions = new List<Collider2D>();
    }

    private void OnEnable()
    {
        canCollectCoins = true;

        PlayerActions.SetPlayerCollision += EnableCollision;
        PlayerActions.IgnoreEnemyCollision += IgnoreEnemyCollision;
        PlayerActions.OnCheckSlope += GetIsOnSlope;
        PlayerActions.OnTriggerDeath += DisableCollectCoins;
    }

    private void OnDisable()
    {

        PlayerActions.SetPlayerCollision -= EnableCollision;
        PlayerActions.IgnoreEnemyCollision -= IgnoreEnemyCollision;
        PlayerActions.OnCheckSlope -= GetIsOnSlope;
        PlayerActions.OnTriggerDeath -= DisableCollectCoins;
    }

    private void Update()
    {
        SlopeCheck();

        DashCollisionCheck();

        CheckTopCollision();

        CheckBottomCollision();
    }

    #region ENEMY COLLISION
    private void IgnoreEnemyCollision(bool result)
    {
        //Enable the collision with enemies so the player can take damage from enemies
        //Disable the collision with enemies so the player can pass through enemies
        Physics2D.IgnoreLayerCollision(9, 7, result);
    }

    private void EnableCollision(bool result)
    {
        col.enabled = result;
    }

    #endregion

    #region SLOPE
    private void SlopeCheck()
    {
        Vector2 checkPosition = transform.position - new Vector3(0f, colliderSize.y / 2);

        SlopeCheckVertical(checkPosition);
    }

    private void SlopeCheckVertical(Vector2 checkPosition)
    {
        if (!PlayerActions.OnCheckJump() && !PlayerActions.OnCheckDash())
        {
            RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.down, slopeCheckDistance, groundLayer);

            if (hit && !PlayerActions.OnCheckJump() &&
                !PlayerActions.OnCheckDash() && PlayerActions.OnCheckGround())
            {
                slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

                slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

                if (slopeDownAngle != slopeDownAngleOld)
                {
                    isOnSlope = true;
                    PlayerActions.IsPlayerOnSlope(true);
                }

                slopeDownAngleOld = slopeDownAngle;

                Debug.DrawRay(hit.point, slopeNormalPerp, Color.magenta);
                Debug.DrawRay(hit.point, hit.normal, Color.blue);

                col.gameObject.transform.rotation = Quaternion.Euler(hit.normal);

                physicsMaterial.friction = 0.1f;
            }
            else
            {
                if (slopeDownAngleOld != 0)
                {
                    slopeDownAngleOld = 0;
                    isOnSlope = false;
                    PlayerActions.IsPlayerOnSlope(false);
                    col.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
                    physicsMaterial.friction = 0f;
                }
            }
        }
        else
        {
            slopeDownAngleOld = 0;
            isOnSlope = false;
            PlayerActions.IsPlayerOnSlope(false);
            col.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
            physicsMaterial.friction = 0f;
        }

    }

    private bool GetIsOnSlope()
    {
        return isOnSlope;
    }

    #endregion

    private void DashCollisionCheck() 
    {
        if (PlayerActions.OnCheckDash()) 
        {
            Collider2D[] hitDash = Physics2D.OverlapCircleAll(dashCollisionPoint.position, dashCollisionRadius);
            foreach(Collider2D col in hitDash) 
            {
                if ((col.CompareTag("Enemy") || col.CompareTag("Obstacle") || col.CompareTag("Crystal")) && col.GetComponent<BurningFloor>() == null)
                {
                    if (col.CompareTag("Obstacle"))
                        col.GetComponent<WallObstacle>().TriggerEventByCollision();
                    else if (col.CompareTag("Enemy"))
                    {
                        IgnoreEnemyCollision(true);
                        col.GetComponent<Enemy>().TakeDamage();
                    }
                    else if (col.CompareTag("Crystal"))
                        col.GetComponent<CollectableCrystal>().CollectItem();

                    PlayerActions.OnCancelDash();
                    PlayerActions.OnCallFeedbackJump(28);
                    StartCoroutine(CameraController.CameraShake(2f, 2f, 0.25f));

                    PlayerActions.CallDashHitParticles(col.transform.position);

                    PlayerActions.CallDashHitScreenEffect();
                    break;
                }
            }

        }
    }

    private void CheckTopCollision() 
    {
        Collider2D[] topCollisionsCheck = Physics2D.OverlapCircleAll(dashCollisionPoint.position, dashCollisionRadius);

        if (topCollisionsArray != topCollisionsCheck) 
        {
            topCollisionsArray = topCollisionsCheck;
            topCollisions.Clear();

            foreach (Collider2D topCol in topCollisionsArray)
                topCollisions.Add(topCol);
        }
    }

    private void CheckBottomCollision()
    {
        Collider2D[] bottomCollisionsCheck = Physics2D.OverlapBoxAll(bottomCollisionPoint.position, bottomColliderSize, 0f);

        if (bottomCollisionsArray != bottomCollisionsCheck)
        {
            bottomCollisionsArray = bottomCollisionsCheck;
            bottomCollisions.Clear();

            foreach (Collider2D bottomCol in bottomCollisionsArray)
                bottomCollisions.Add(bottomCol);
        }

        CheckBottomColliders(bottomCollisions);
    }

    void CheckBottomColliders(List<Collider2D> collisions)
    {
        foreach (Collider2D collider in collisions)
        {
            //Collision with obstacles
            if (collider.gameObject.CompareTag("Obstacle"))
            {
                //Player burns the burning floor
                if (collider.gameObject.TryGetComponent<BurningFloor>(out BurningFloor burningFloor))
                {
                    burningFloor.Event();
                }
            }
        }
    }

    private void DisableCollectCoins() 
    {
        canCollectCoins = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Collision with ENEMY
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(!PlayerActions.OnCheckDash())
            {
                //Take damage 
                statsManager.SpendHP(1);
            }
        }

        //Collision with lodo
        if (collision.gameObject.CompareTag("Lodo"))
        {
            statsManager.SpendHP(1);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collision with INTERACTIVE OBJECTS 
        if (collision.gameObject.CompareTag("InteractiveObject"))
        {
            //Add the object to interact
            interactiveObject = collision.gameObject.GetComponent<InteractiveObject>();
            interactiveObject.TriggerEvent();

        }

        //Collision with COLLECTABLE ITEMS
        if (collision.gameObject.layer == 12 && canCollectCoins) //Item layer
        {
            CollectableCoin item = collision.GetComponent<CollectableCoin>();
            item.CollectItem();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Collision with INTERACTIVE OBJECTS 
        if (collision.gameObject.layer == 8)
        {
            //Remove the object to interact
            interactiveObject = null;
        }
    }

    private void OnDrawGizmos()
    {
        //Slope
        Vector3 rayDirection = transform.TransformDirection(Vector3.down) * slopeCheckDistance;
        Gizmos.DrawRay(transform.position, rayDirection);

        //Dash
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(dashCollisionPoint.position, dashCollisionRadius);

        //Top collision
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(topCollisionPoint.position, topColliderSize);

        //Bottom colision
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(bottomCollisionPoint.position, bottomColliderSize);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Main enemy class, contains the main variables for the childrem classes
*/

public abstract class Enemy : MonoBehaviour
{
    public Vector3 initialPosition;
    public Transform groundCheck;

    [SerializeField] protected SpriteRenderer spr;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody2D rig;
    [SerializeField] protected Collider2D coll;
    [SerializeField] protected float dir;
    [SerializeField] protected float speed;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected bool isLookingRight;
    protected bool isPaused;


    protected EnemiesRespawner enemyRespawner;

    protected void Awake()
    {
        
        initialPosition = transform.localPosition;
        enemyRespawner = FindObjectOfType<EnemiesRespawner>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void OnEnable()
    {
        transform.localPosition = initialPosition;
        StageBlocksHandler.OnStageBlockTransitionStarted += OnStageBlockTransitionStarted;
        StageBlocksHandler.OnStageBlockTransitionEnded += OnStageBlockTransitionEnded;
        StartCoroutine(EnableDelegatesWithDelay());
    }


    protected IEnumerator EnableDelegatesWithDelay()
    {
        yield return new WaitForSeconds(0.1f);

        GameStateManager.Instance.OnGameStateChanged += ChangeEnemyState;
    }

    protected void OnDisable()
    {
        StageBlocksHandler.OnStageBlockTransitionStarted -= OnStageBlockTransitionStarted;
        StageBlocksHandler.OnStageBlockTransitionEnded -= OnStageBlockTransitionEnded;
        GameStateManager.Instance.OnGameStateChanged -= ChangeEnemyState;

        if (enemyRespawner == null)
            enemyRespawner = FindObjectOfType<EnemiesRespawner>();

        if (enemyRespawner == null)
            return;

        if (enemyRespawner.enemiesQueue == null)
            enemyRespawner.enemiesQueue = new Queue<Enemy>();

        if (enemyRespawner.enemiesQueue == null)
            return;

        if (enemyRespawner.enemiesQueue.Count <= 0)
            enemyRespawner.RestartCounter();

        enemyRespawner.enemiesQueue.Enqueue(this);
    }

    protected virtual void Flip()
    {
        transform.Rotate(0, 180f, 0);
        isLookingRight = !isLookingRight;
    }


    #region ENEMY STATES
    public void ChangeEnemyState(GameState newState)
    {
        switch (newState)
        {
            case (GameState.Paused):
                Pause();
                break;
            case (GameState.Gameplay):
                Play();
                break;
        }
    }

    protected virtual void Pause()
    {
        speed = 0f;
        coll.enabled = false;
        rig.gravityScale = 0f;
        isPaused = true;
    }

    protected virtual void Play()
    {
        coll.enabled = true;
        speed = maxSpeed;
        rig.gravityScale = 1f;
        isPaused = false;
    }
    #endregion

    public virtual void TakeDamage()
    {
        Pause();
        SoundsManager.instance.PlayAudio(AudiosReference.dashHitEnemy, AudioType.PLAYER, null);
        anim.SetTrigger("Dead");
    }

    public virtual void Respawn()
    {
        anim.SetTrigger("Respawn");
        Debug.Log("reSDF");
    }

    protected virtual void Dead()
    {
        this.gameObject.SetActive(false);
    }

    protected virtual void OnStageBlockTransitionStarted()
    {
        anim.speed = 0f;
        Pause();
    }

    protected virtual void OnStageBlockTransitionEnded()
    {
        anim.speed = 1f;
        Play();
    }
}

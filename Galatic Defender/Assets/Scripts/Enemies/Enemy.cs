using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType
{
    Projectile,
    Spaceship
}

public class Enemy : MonoBehaviour
{
    private EnemiesManager enemiesManager;
    public EnemyType enemyType;

    [SerializeField] protected Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();   
        enemiesManager = FindObjectOfType(typeof(EnemiesManager)) as EnemiesManager;
    }


    #region Essential_functions


    public void DestroySelf()
    {
        Destroy(this.gameObject);    
    }
    #endregion


    private void OnDestroy()
    {
        enemiesManager.DecreaseEnemyCount();
    }
}

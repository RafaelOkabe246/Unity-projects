using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soldier_field_of_view : MonoBehaviour
{
    [SerializeField] internal soldier _soldier;
    [SerializeField] internal float viewRadius;
    [SerializeField] internal float viewAngle;
    [SerializeField] internal LayerMask obstacleMask, enemyMask;
    Collider2D[] EnemiesInRadius;
    public List<Transform> visibleEnemies = new List<Transform>();

    [SerializeField] internal GameObject Most_near_enemy;

    private void Start()
    {
        _soldier = this.gameObject.GetComponent<soldier>();
    }

    private void FixedUpdate()
    {
        FindVisibleEnemies();
    }

    void FindVisibleEnemies()
    {
        EnemiesInRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius);

        visibleEnemies.Clear();

        for (int i = 0; i < EnemiesInRadius.Length; i++)
        {
            Transform enemies = EnemiesInRadius[i].transform;
            Vector2 dirTarget = new Vector2(enemies.position.x - transform.position.x, enemies.position.y - transform.position.y);

            if (Vector2.Angle(dirTarget, transform.right) < viewAngle/2)
            {
                float distanceTarget = Vector2.Distance(transform.position, enemies.position);

                if (!Physics2D.Raycast(transform.position, dirTarget, distanceTarget, obstacleMask))
                {

                    visibleEnemies.Add(enemies);
                    
                }
            }
        }
    }

    public Vector2 DirFromAngle(float angleDeg, bool isGlobal)
    {
        if (!isGlobal)
        {
            angleDeg += transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Cos(angleDeg * Mathf.Deg2Rad), Mathf.Sin(angleDeg * Mathf.Deg2Rad));
    } 
}

using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public abstract void Act(Transform player, Rigidbody2D rb, float moveSpeed);

    protected Vector2 SnapTo8Directions(Vector2 input)
    {
        if (input == Vector2.zero) return Vector2.zero;

        Vector2[] directions = new Vector2[]
        {
            Vector2.up,
            new Vector2(1, 1).normalized,
            Vector2.right,
            new Vector2(1, -1).normalized,
            Vector2.down,
            new Vector2(-1, -1).normalized,
            Vector2.left,
            new Vector2(-1, 1).normalized
        };

        float maxDot = -Mathf.Infinity;
        Vector2 bestDir = Vector2.zero;

        foreach (var dir in directions)
        {
            float dot = Vector2.Dot(input.normalized, dir);
            if (dot > maxDot)
            {
                maxDot = dot;
                bestDir = dir;
            }
        }

        return bestDir;
    }
}




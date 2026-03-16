using UnityEngine;

[CreateAssetMenu(fileName = "Enemy")]
public class EnemyScriptableObject: ScriptableObject
{
    public int health;
    public int damage;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Содержит данные игрока, использует ScriptableObject
/// </summary>
[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public float score = 0f;
    public float bullets = 3f;
    public float speed = 1000f;
    public float rotationSpeed = 300f;
    public float speedBullet = 1000f;
    public float timeLifeBullet = 5f;

    /// <summary>
    /// Возвращает начальные значения количества патрон и очков
    /// </summary>
    public void Reset()
    {
        bullets = 3f;
        score = 0f;
    }
}

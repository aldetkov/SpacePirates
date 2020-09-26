using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Представление системы врагов
/// </summary>
public class EnemyView : BasicView
{
    /// <summary>
    /// Событие создания врага
    /// </summary>
    /// <param name="transform">Трансформ родителя</param>
    public delegate void OnSpawnEnemy(Transform transform);
    public event OnSpawnEnemy OnSpawn;

    /// <summary>
    /// Событие столкновения
    /// </summary>
    /// <param name="gameObject">Объект врага</param>
    /// <param name="collision">Столкновение</param>
    /// <param name="isCollisionExplosion">Должен ли быть взорван тот, с кем столкнулся враг?</param>
    public delegate void OnCollisionDestroy(GameObject gameObject, Collision collision, bool isCollisionExplosion);
    public event OnCollisionDestroy OnDestroySelf;

    /// <summary>
    /// Событие столкновения с игроком - конец игры
    /// </summary>
    public delegate void OnCollisionPlayer();
    public event OnCollisionPlayer OnGameOver;

    /// <summary>
    /// Событие изменения времени спавна врагов
    /// </summary>
    /// <returns></returns>
    public delegate float ChangeSpawnTime();
    public event ChangeSpawnTime OnGetSpawnTime;


    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    public override void UpdateView() { }

    /// <summary>
    /// Отправка события столкновения и самоуничтожения
    /// </summary>
    /// <param name="gameObject">Объект врага</param>
    /// <param name="collision">Столкновение</param>
    /// <param name="isCollisionExplosion">Должен ли быть взорван тот, с кем столкнулся враг?</param>
    public void SendEventOnDestroySelf(GameObject gameObject, Collision collision, bool isCollisionExplosion)
    {
        OnDestroySelf(gameObject, collision, isCollisionExplosion);
    }

    /// <summary>
    /// Спавн врагов
    /// </summary>
    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            OnSpawn(transform);
            yield return new WaitForSeconds(OnGetSpawnTime()); // ожидение по полученному времени из модели
        }
    }

    /// <summary>
    /// Запуск корутины окончания игры
    /// </summary>
    /// <param name="gameObj">Объект врага</param>
    /// <param name="collision">Столкновение</param>
    public void StartCoroutineGameOver(GameObject gameObj, Collision collision)
    {
        StartCoroutine(GameOver(gameObj, collision));
    }
    
    /// <summary>
    /// Конец игры - отправка события взрыва и с задержкой собятия конца игры.
    /// </summary>
    /// <param name="gameObj">Объект врага</param>
    /// <param name="collision">столкновение</param>
    /// <returns></returns>
    private IEnumerator GameOver(GameObject gameObj, Collision collision)
    {
        
        OnDestroySelf(gameObj, collision, true);
        yield return new WaitForSeconds(2f);
        OnGameOver();
    }
}

using UnityEngine;
using DG.Tweening;

public class EnemyModel
{

    private GameObject[] _enemies;
    private PlayerData _playerData;
    private float _spawnEnemyTime = 2f;

    /// <summary>
    /// Конктруктор выполняет загрузку префабов врагов и данных игрока
    /// </summary>
    public EnemyModel()
    {
        _playerData = Resources.Load("PlayerDataStats") as PlayerData; // загрузка данных игрока
        _enemies = new GameObject[3];

        // загрузка врагов
        for (int i = 0; i < _enemies.Length; i++)
        {
            GameObject obj = Resources.Load($"Enemy_{i + 1}") as GameObject; 
                _enemies[i] = obj;
        }
    }

    /// <summary>
    /// Событие изменения очков
    /// </summary>
    public delegate void ChangeScore(float score);
    public event ChangeScore OnChangeScore;


    /// <summary>
    /// Создание врага
    /// </summary>
    /// <param name="transform">Трансформ родителеского объекта</param>
    public void SpawnEnemy(Transform transform)
    {
        float sideSpawn = RandomSideSpawn();
        Quaternion spawnDir = Quaternion.Euler(0f, 90 * sideSpawn, 0f); // Поворот в направлении игрового поля
        Vector3 spawnPosition = new Vector3(ViewPortSizeInfo.leftBot.x * sideSpawn * 1.1f, 1f, Random.Range(ViewPortSizeInfo.leftBot.z, ViewPortSizeInfo.rightTop.z)); // Начальная позиция
        GameObject enemy = Object.Instantiate(_enemies[Random.Range(0,_enemies.Length)], spawnPosition, spawnDir, transform) as GameObject; // Создание врага
        enemy.GetComponent<Rigidbody>().AddForce(enemy.transform.forward * 10f, ForceMode.Impulse); // Придание импульса движения rigidbody врага
    }

    /// <summary>
    /// Уничтожение врага
    /// </summary>
    /// <param name="gameObject">Игровой объект врага</param>
    /// <param name="collision">Столкновение</param>
    /// <param name="isCollisionExplosion">Должен ли столкнувшийся объект взорваться?</param>
    public void DestroyEnemy (GameObject gameObject, Collision collision, bool isCollisionExplosion)
    {
        // взрыв столкнувшегося объекта
        if (isCollisionExplosion) 
        {
            Explosion(collision.gameObject.transform.position);
            Object.Destroy(collision.gameObject);
        }

        // взрыв врага
        Explosion(gameObject.transform.position);
        Object.Destroy(gameObject);

        // если было попадание, то прибавляем очко
        if (collision.gameObject.CompareTag("Bullet")) _playerData.score++; 
        OnChangeScore(_playerData.score);
        Camera.main.transform.rotation = Quaternion.Euler(Vector3.right * 90f); // возвращение базового поворота камеры
        Camera.main.DOShakeRotation(0.6f, 1.5f, 50); // встряска камеры
    }

    /// <summary>
    /// Остановка времени при проигрыше
    /// </summary>
    public void GameOver ()
    {
        Time.timeScale = 0f;
    }

    /// <summary>
    ///  Возвращает время появления врагов, уменьшающее по мере накопления очков. Наименьшее - 0.5f
    /// </summary>
    public float GetEnemySpawnTime()
    {
        float koef = _playerData.score / 30;
        if (koef < 1) return _spawnEnemyTime - koef * 1.5f;
        else return 0.5f;
    }

    /// <summary>
    /// Возвращает рандомно 1 или -1
    /// </summary>
    private float RandomSideSpawn()
    {
        int i = Random.Range(0, 2);
        switch (i)
        {
            case 0:
                return -1f;
            case 1:
                return 1f;
            default:
                return 1f;
        }
    }

    /// <summary>
    /// Создание системы частиц взрыва
    /// </summary>
    /// <param name="position">Позиция взрывы</param>
    private void Explosion (Vector3 position)
    {
        GameObject explosionParticle = Resources.Load("ExplosionParticle") as GameObject;
        Object.Instantiate(explosionParticle, position, Quaternion.identity);

    }
}

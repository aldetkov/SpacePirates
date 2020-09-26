using DG.Tweening;
using UnityEngine;

/// <summary>
/// Модель игрока
/// </summary>
public class PlayerModel
{
    private PlayerData _playerData;
    private GameObject _bullet;
    public PlayerModel()
    {
        _playerData = Resources.Load("PlayerDataStats") as PlayerData;
        _bullet = Resources.Load("Bullet") as GameObject;
        _playerData.Reset();
    }

    /// <summary>
    /// Событие изменения количества патрон для UI
    /// </summary>
    /// <param name="bullets">Количество патрон</param>
    public delegate void ChangeBullets(float bullets);
    public event ChangeBullets OnChangeBullets;

    /// <summary>
    /// Возвращает вектор движения игрока - вперед
    /// </summary>
    /// <param name="transform">Трансформ игрока</param>
    public Vector3 GetVectorMoveToForward (Transform transform)
    {
        return transform.forward * _playerData.speed * Time.deltaTime;
    }

    /// <summary>
    /// Поворот игрока в сторону мыши
    /// </summary>
    /// <param name="transform">Трансформ игрока</param>
    public void RotationToMouse(Transform transform)
    {
        Vector3 dir = GetMousePositionRayCast();
        Vector3 direct = new Vector3(dir.x, transform.position.y, dir.z) - transform.position; // получение направления относительно текущей позиции
        if (Vector3.Angle(direct, transform.forward) > 3f && !direct.Equals(Vector3.zero)) { }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direct), _playerData.rotationSpeed * Time.deltaTime); // поворот
        RestrictionOfMovementOnScreen(transform); // Ограничение движения игрока видом камеры
    }

    /// <summary>
    /// Выстрел
    /// </summary>
    public void Shot (Transform transformPlayer)
    {
       if(SpendBullet()) 
       { 
           GameObject bullet = CreateBullet(transformPlayer);
           bullet.GetComponent<Rigidbody>().AddForce(transformPlayer.forward *_playerData.speedBullet, ForceMode.Impulse);
            Camera.main.transform.rotation = Quaternion.Euler(Vector3.right * 90f); // возвращение базового поворота камеры
            Camera.main.DOShakeRotation(0.2f, 1f, 20); // встряска камеры
       }
    }

    /// <summary>
    /// Добавляет патрону и вызывает событие ChangeBullets
    /// </summary>
    public void AddBullet ()
    {
        _playerData.bullets++;
        OnChangeBullets(_playerData.bullets);
    }

    /// <summary>
    /// Тратит патрон и возвращает true, если он действительно потратился, вызывает событие ChangeBullets
    /// </summary>
    /// <returns></returns>
    private bool SpendBullet()
    {
        if (_playerData.bullets > 0)
        {
            _playerData.bullets--;
            OnChangeBullets(_playerData.bullets);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Ограничение движения игрока видом камеры
    /// </summary>
    /// <param name="transform">Трансформ игрока</param>
    private void RestrictionOfMovementOnScreen(Transform transform)
    {
        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, ViewPortSizeInfo.leftBot.x, ViewPortSizeInfo.rightTop.x);
        clampedPos.z = Mathf.Clamp(clampedPos.z, ViewPortSizeInfo.leftBot.z, ViewPortSizeInfo.rightTop.z);
        transform.position = clampedPos;
    }

    /// <summary>
    /// Получение позиции мыши в мировых координатах на коллайдере
    /// </summary>
    private Vector3 GetMousePositionRayCast()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        return hit.point;
    }

    /// <summary>
    /// Создание патроны перед игроком
    /// </summary>
    private GameObject CreateBullet(Transform transformPlayer)
    {
        Vector3 positionBullet = transformPlayer.position + transformPlayer.forward * 2f + transformPlayer.up;
        GameObject bullet2 = Object.Instantiate(_bullet, positionBullet, Quaternion.identity);
        return bullet2;
    }
}

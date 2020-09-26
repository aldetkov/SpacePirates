using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Иницилизирует игровой процесс, содержит главный Update-метод, взаимодействует с UI
/// </summary>
public class GameManager : MonoBehaviour
{
    private PlayerController _playerController;
    private EnemyController _enemyController;
    private UIView _uiView;
    private List<GameObject> _gameObjectLoad = new List<GameObject>();
    private List<GameObject> _gameObjectInScene = new List<GameObject>();

    private void Awake()
    {
        ResourcesLoadedAndCreate(); // загрузка ресурсов
    }

    private void Update()
    {
        if (_playerController != null) _playerController.UpdateController();
    }

    /// <summary>
    /// Начинает игру - загрузка объектов на сцену, инициализация контроллеров, подписка на события UI
    /// </summary>
    public void StartGame()
    {
        CreateGameObjectOnScene();
        InitGame();
        UIViewSubscriberEvents();
    }

    /// <summary>
    /// Рестарт игры - удаление всех объектов на сцене и заново загружает их
    /// </summary>
    public void RestartGame()
    {
        EndGame();
        StartGame();
    }

    /// <summary>
    /// Инициализирует переменные класса - контроллеры и UI
    /// </summary>
    void InitGame()
    {
        _playerController = new PlayerController(new PlayerModel(), FindObjectOfType<PlayerView>());
        _enemyController = new EnemyController(new EnemyModel(), FindObjectOfType<EnemyView>());
        _uiView = FindObjectOfType<UIView>();
    }

    /// <summary>
    /// Загрузка ресурсов из Resources
    /// </summary>
    void ResourcesLoadedAndCreate()
    {
        ResourcesLoadInList("Player");
        ResourcesLoadInList("Background");
        ResourcesLoadInList("EnemySpawner");
        ResourcesLoadInList("ColliderForMovement");
    }

    /// <summary>
    /// Создание объектов на сцене из загруженных ресурсов
    /// </summary>
    void CreateGameObjectOnScene()
    {
        for (int i = 0; i < _gameObjectLoad.Count; i++)
        {
            _gameObjectInScene.Add(Instantiate(_gameObjectLoad[i]));
        }
    }

    /// <summary>
    /// Загрузка ресурса из Resources по имени и добавление его в список загруженных ресурсов
    /// </summary>
    /// <param name="name">Имя ресурса</param>
    void ResourcesLoadInList (string name)
    {
        _gameObjectLoad.Add(Resources.Load(name) as GameObject);
    }

    /// <summary>
    /// Завершение текущей игры - удаление всех объектов со сцены, обнуление переменных данного класса
    /// </summary>
    public void EndGame()
    {
        for (int i = 0; i < _gameObjectInScene.Count; i++) Destroy(_gameObjectInScene[i]);

        _gameObjectInScene = new List<GameObject>();
        _playerController = null;
        _enemyController = null;
        _uiView = null;
        Time.timeScale = 1f; // восстановление нормального течения времени игры
    }

    /// <summary>
    /// Подписка событий UIview на события контроллеров
    /// </summary>
    void UIViewSubscriberEvents()
    {

        _enemyController.GetEnemyView().OnGameOver += _uiView.GameOver;
        _playerController.GetPlayerModel().OnChangeBullets += _uiView.ChangeBullets;
        _enemyController.GetEnemyModel().OnChangeScore += _uiView.ChangeScore;
    }

}

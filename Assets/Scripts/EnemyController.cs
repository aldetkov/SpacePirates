using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер системы врагов
/// </summary>
public class EnemyController : BasicController
{
    private EnemyModel _model;
    private EnemyView _view;

    /// <summary>
    /// Подписывает на события EnemyView методы EnemyModel
    /// </summary>
    public EnemyController(EnemyModel model, EnemyView view)
    {
        _model = model;
        _view = view;

        _view.OnSpawn += _model.SpawnEnemy;
        _view.OnDestroySelf += _model.DestroyEnemy;
        _view.OnGameOver += _model.GameOver;
        _view.OnGetSpawnTime += _model.GetEnemySpawnTime;
    }

    public override void UpdateController()
    {
        _view.UpdateView();
    }
    
    public EnemyView GetEnemyView()
    {
        return _view;
    }

    public EnemyModel GetEnemyModel()
    {
        return _model;
    }
}

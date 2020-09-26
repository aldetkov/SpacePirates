using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Представление отдельного юнита врага
/// </summary>
public class EnemyUnitView : BasicView
{

    public override void UpdateView() { }

    private void Start()
    {
        StartCoroutine(DestroyObj(10f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Обработка столкновения врага с врагом или пулей
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet"))
        {
            GetComponentInParent<EnemyView>().SendEventOnDestroySelf(gameObject, collision, false); // Вызов события OnDestroySelf из EnemyView
            Destroy(collision.gameObject);
        }
        // Вызов события OnDestroySelf и OnGameOver из EnemyView при стокновении с Игроком
        else if (collision.gameObject.CompareTag("Player")) GetComponentInParent<EnemyView>().StartCoroutineGameOver(gameObject, collision); 
    }

}

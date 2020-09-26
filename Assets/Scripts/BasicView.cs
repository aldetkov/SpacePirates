using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Родитель всех View в проекте
/// </summary>
public abstract class BasicView : MonoBehaviour
{
    /// <summary>
    /// Обновляет view, при необходимости возможна реализация в update-методах MonoBehaviour
    /// </summary>
    public abstract void UpdateView();

    /// <summary>
    /// Самоуничтожение объекта с задержкой
    /// </summary>
    protected IEnumerator DestroyObj (float second)
    {
        yield return new WaitForSeconds(second);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Представление частиц с самоуничтожением
/// </summary>
public class ParticleView : BasicView
{
    [SerializeField]
    private float second = 3f;

    public override void UpdateView()
    {
    }

    private void Start()
    {
        StartCoroutine(DestroyObj(second));
    }

}

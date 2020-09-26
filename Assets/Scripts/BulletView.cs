using UnityEngine;

/// <summary>
/// Представление пули, содержит её самоуничтожение
/// </summary>
public class BulletView : BasicView
{

    [SerializeField]
    private PlayerData playerData = null;

    public override void UpdateView() { }

    private void Awake()
    {
        StartCoroutine(DestroyObj(playerData.timeLifeBullet));
    }
}

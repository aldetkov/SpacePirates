using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : BasicView
{
    /// <summary>
    /// Событие нажатия ЛКМ, возвращает вектор движения
    /// </summary>
    /// <param name="transform">Трансформ игрока</param>
    public delegate Vector3 PlayerInputMouse0(Transform transform);
    public event PlayerInputMouse0 OnInputMouse0;

    /// <summary>
    /// Событие нажатия ПКМ
    /// </summary>
    /// <param name="transform">Трансформ игрока</param>
    public delegate void PlayerInputMouse1(Transform transform);
    public event PlayerInputMouse1 OnInputMouse1;

    /// <summary>
    /// Событие поворота игрока за мышкой
    /// </summary>
    /// <param name="transform">Трансформ игрока</param>
    public delegate void PlayerRotationToMouse(Transform transform);
    public event PlayerRotationToMouse OnRotationToMouse;

    /// <summary>
    /// Событие добавления патрон
    /// </summary>
    public delegate void PlayerAddBullet();
    public event PlayerAddBullet OnAddBullet;

    [SerializeField]
    private ParticleSystem _moveParticle = null;
    [SerializeField]
    private AudioSource _moveSound = null;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        StartCoroutine(AddBullet());
    }

    public override void UpdateView()
    {
        // Событие поворота за мышкой
        OnRotationToMouse(transform);

        // Обработка нажатия ЛКМ
        if (Input.GetKey(KeyCode.Mouse0))
        {
            rb.AddForce(OnInputMouse0(transform));

            // включение частиц и звука
            _moveParticle.Play();
            if (!_moveSound.isPlaying) _moveSound.Play();
        }
        // Выключение частиц и звука при отжатии ЛКМ
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _moveParticle.Stop();
            _moveSound.Stop();
        }

        // обработка нажатия ПКМ
        if (Input.GetKeyDown(KeyCode.Mouse1)) 
            OnInputMouse1(transform);
    }

    /// <summary>
    /// Вызов события добавления патрон каждые 5 секунд
    /// </summary>
    /// <returns></returns>
    IEnumerator AddBullet ()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            OnAddBullet();
        }
    }

}


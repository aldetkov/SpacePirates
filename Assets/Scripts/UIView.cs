using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIView : BasicView
{
     
    [SerializeField]
    private TextMeshProUGUI _textScore = null;
    [SerializeField]
    private TextMeshProUGUI _textBullets = null;
    [SerializeField]
    private GameObject _gameOverPanel = null;
    [SerializeField]
    private GameObject _gameInterfacePanel = null;
    [SerializeField]
    private GameObject _gameMenuPanel = null;
 
    public override void UpdateView() { }

    /// <summary>
    /// Отрытие игрового меню
    /// </summary>
    public void OpenGameMenu ()
    {
        _gameMenuPanel.SetActive(true);
        _gameOverPanel.SetActive(false);
        _gameInterfacePanel.SetActive(false);
    }

    /// <summary>
    /// Начало игры - обновление количества патрон и игровых очков в UI, включение игрового интерфейса
    /// </summary>
    public void StartGame()
    {
        _gameMenuPanel.SetActive(false);
        _gameOverPanel.SetActive(false);
        _gameInterfacePanel.SetActive(true);
        ChangeScore(0);
        ChangeBullets(3f);

    }

    /// <summary>
    /// Проигрыш - включени меню конца игры
    /// </summary>
    public void GameOver()
    {
        _gameOverPanel.SetActive(true);
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Изменение количества очков в UI
    /// </summary>
    /// <param name="score"></param>
    public void ChangeScore (float score)
    {
        _textScore.text = $"Score: {score}";
    }

    /// <summary>
    /// Обновление количества патрон в UI
    /// </summary>
    /// <param name="bullets"></param>
    public void ChangeBullets (float bullets)
    {
        _textBullets.text = $" Количество зарядов: {bullets}";
    }

}

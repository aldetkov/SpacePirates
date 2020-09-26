/// <summary>
/// Контроллер игрока
/// </summary>
public class PlayerController : BasicController
{
    PlayerModel _model;
    PlayerView _view;

    /// <summary>
    /// Инициализирует представление и модель, подписывает модель на все события представления
    /// </summary>
    /// <param name="model">Модель игрока</param>
    /// <param name="view">Представление игрока</param>
    public PlayerController(PlayerModel model, PlayerView view)
    {
        _model = model;
        _view = view;

        _view.OnRotationToMouse += _model.RotationToMouse;
        _view.OnInputMouse0 += _model.GetVectorMoveToForward;
        _view.OnInputMouse1 += _model.Shot;
        _view.OnAddBullet += _model.AddBullet;
    }

    public override void UpdateController()
    {
        if (_view != null ) _view.UpdateView();
    }

    public PlayerView GetPlayerView ()
    {
        return _view;
    }

    public PlayerModel GetPlayerModel()
    {
        return _model;
    }
}

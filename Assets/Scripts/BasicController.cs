
/// <summary>
/// Родитель всех контроллеров
/// </summary>
public abstract class BasicController
{
    /// <summary>
    /// Обновление контроллера, которое может включать обновление view, при его необходимости должен быть реализован в update-методах MoneBehaviour
    /// </summary>
    public abstract void UpdateController();

}
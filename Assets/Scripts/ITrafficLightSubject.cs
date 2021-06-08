// Interface for the observable object
public interface ITrafficLightSubject
{
    void RegisterObserver(ICarObserver car);
    void UnregisterObserver(ICarObserver car);
    void NotifyObservers();
}

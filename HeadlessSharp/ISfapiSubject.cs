namespace HeadlessSharp;

public interface ISfapiSubject
{
    // register an observer
    void RegisterObserver(IObserver observer);
    
    // unregister an observer
    void UnregisterObserver(IObserver observer);
    
    // update all observers
    void NotifyObservers(string data);
}
namespace HeadlessSharp;

public interface IObserver
{
    // Receive Notification from Subject
    void Update(string shopInfo);
}
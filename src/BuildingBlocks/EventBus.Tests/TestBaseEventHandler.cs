namespace EventBus.Tests;

public class TestBaseEventHandler : IBaseEventHandler<TestBaseEvent>
{
    public bool Handled { get; private set; }
    public TestBaseEventHandler()
    {
        Handled = false;
    }
    public async Task Handle(TestBaseEvent @event)
    {
        Handled = true;
    }
}
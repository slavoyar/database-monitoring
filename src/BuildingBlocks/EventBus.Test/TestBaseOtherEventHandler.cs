using DatabaseMonitoring.BuildingBlocks.EventBus.Abstractions;

namespace EventBus.Test;

public class TestBaseOtherEventHandler : IBaseEventHandler<TestBaseEvent>
{
    public bool Handled { get; private set; }
    public TestBaseOtherEventHandler()
    {
        Handled = false;
    }
    public async Task Handle(TestBaseEvent @event)
    {
        Handled = true;
    }
}
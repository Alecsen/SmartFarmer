using Application.Events;
using Application.LogicInterface;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.EventHandlers;

public class WeatherDataUpdatedHandler : INotificationHandler<WeatherUpdateEvent>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public WeatherDataUpdatedHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Handle(WeatherUpdateEvent notification, CancellationToken cancellationToken)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var fieldLogic = scope.ServiceProvider.GetRequiredService<IFieldLogic>();
            await fieldLogic.PerformCalculation();
        }
    }
}
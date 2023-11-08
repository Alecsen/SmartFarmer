using Application.DAOInterface;

namespace Application.Logic;

public class SensorLogic
{
    private readonly ISensorDao sensorDao;

    public SensorLogic(ISensorDao sensorDao)
    {
        this.sensorDao = sensorDao;
    }
}
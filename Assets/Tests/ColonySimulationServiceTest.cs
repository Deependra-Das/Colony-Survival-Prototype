using NUnit.Framework;
using ColonySurvivalPrototype.Colony;
using ColonySurvivalPrototype.Event;

public class ColonySimulationServiceTest
{
    private ColonySimulationService _simulationService;

    [SetUp]
    public void SetUp()
    {
        var eventBusService = new EventBusService();
        _simulationService = new ColonySimulationService(eventBusService);
    }

    [Test]
    public void SimulationMathTest()
    {
        var populationData = new PopulationData
        {
            villagersCount = 20,
            foodReserve = 1000f,
            waterReserve = 700f
        };

        var consumptionData = new ConsumptionData
        {
            foodConsumptionPerVillagerPerDay = 2f,
            waterConsumptionPerVillagerPerDay = 3f
        };

        _simulationService.AddNewColony(populationData, consumptionData);

        _simulationService.AdvanceOneDayForVillageById(0);
        _simulationService.AdvanceOneDayForVillageById(0);
        _simulationService.AdvanceOneDayForVillageById(0);

        ColonyData colony = _simulationService.GetColonyDataByColonyId(0);

        Assert.AreEqual(880f, colony.FoodReserve);
        Assert.AreEqual(520f, colony.WaterReserve);
    }

}

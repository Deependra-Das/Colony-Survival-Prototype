## Colony Survival Prototype

A small Unity prototype focused on simulating a colony's population and resource consumption over time.

- Unity Version Used : 6.000.4.0f1
  
---

## How to Open and Run

- Open the project using Unity
- Open the GameplayScene scene from the project window : Assets/Scenes/GameplayScene.unity
- Press Play in the Unity Editor to run the prototype.
- Once the scene loads, click on the Start Simulation button at the bottom of the UI.

## Running the Unit Tests

The project includes a unit test for the colony simulation service.

- Test Script : ColonySimulationServiceTest
- Test Method : SimulationMathTest

### How to Run
- Open the project in Unity.
- Go to Window → General → Test Runner.
- Open the EditMode tests.
- Locate ColonySimulationServiceTest.
- Click on the Run Selected or Run All.

The test creates a colony with:
- 20 villagers
- 1000 food
- 700 water
- 2 food consumed per villager per day
- 3 water consumed per villager per day

It advances the simulation by three days and verifies that the final reserves are:
- Food: 880
- Water: 520

---

## AI tools used

I used ChatGPT to create a basic schema for the code structure. The implementation was done manually by adapting the schema and updating it as per the need. Also, I used ChatGPT to write the test case because my current skill set doesn't cover unit testing. All the core project decisions and integration were handled by myself.

## Decisions & trade-offs

The prototype focuses on the core colony simulation, so certain edge cases were simplified due to the prototype scope with priority given to demonstrating the simulation logic of resource consumption, and basic test coverage.
Initially I only had planned to make the simulation for once colony object, but I wanted the whole system to be as flexible as possible, hence implemented creation of colony with the data and then add that for simulation. Down the line more colonies can be added with their own unique initial values. I utilized design patterns to make the code architecture flexible as well.

---

## Demo Video
https://youtu.be/0Quye1Nq79A


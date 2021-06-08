using UnityEngine;

public class Simulation : MonoBehaviour
{
    private void Start()
    {
        // When we start the simulation, immediately spawn some cars
        Invoke(nameof(SpawnCar), 0f);
    }

    private void SpawnCar()
    {
        // Cars are spawned in random locations on the road piece before the traffic light
        Instantiate(GetRandomCarPrefab().transform, new Vector3(Random.Range(22f,30f), 1.05f, -0.8f), Quaternion.identity);
        Instantiate(GetRandomCarPrefab().transform, new Vector3(Random.Range(22f,30f), 1.05f, 1.6f), Quaternion.identity);
        
        // New cars are spawned in random intervals
        Invoke(nameof(SpawnCar), Random.Range(2f, 3f));
    }

    // To have some variety...
    private GameObject GetRandomCarPrefab()
    {
        GameObject carObject = null;
        
        switch (Random.Range(1, 4))
        {
            case 1:
                carObject = Resources.Load("Prefabs/RedCar") as GameObject;
                break;
            case 2:
                carObject = Resources.Load("Prefabs/BlueCar") as GameObject;
                break;
            case 3:
                carObject = Resources.Load("Prefabs/YellowCar") as GameObject;
                break;
        }

        return carObject;
    }
}

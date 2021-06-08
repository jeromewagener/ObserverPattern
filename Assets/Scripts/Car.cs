using UnityEngine;
using Object = UnityEngine.Object;

public class Car : MonoBehaviour, ICarObserver
{
    private bool _allowedToMove = false;

    // Unity setup method.
    // Registers the observer (car) to the subject (traffic light)
    private void Start()
    {
        GameObject trafficLight = GameObject.Find("TrafficLight");
        trafficLight.GetComponent<ITrafficLightSubject>().RegisterObserver(this);
    }

    // Unity "Update" method with different method signature, not related to the Observer Pattern...
    // Updates the 3D scene by moving the car objects over the road but only if it is "_allowedToMove"
    void Update()
    {
        if (_allowedToMove)
        {
            // Move the car
            transform.Translate(Vector3.left * (10f * Time.deltaTime));

            // Unregister once we go past the traffic light
            if (transform.position.x < 10)
            {
                GameObject trafficLight = GameObject.Find("TrafficLight");
                trafficLight.GetComponent<ITrafficLightSubject>().UnregisterObserver(this);
                _allowedToMove = true;
            }

            // Destroy the car object after some more driving
            if (transform.position.x < -20)
            {
                Object.Destroy(this.gameObject);
            }
        }
    }

    // ***Observer update method*** Change the "allowToMove" state according to the traffic lights green color state!
    public void Update(bool isTrafficLightGreen)
    {
        _allowedToMove = isTrafficLightGreen;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour, ITrafficLightSubject
{
    // Materials needed to "light-on" the bulbs of the traffic light according to the timer
    public Material redLightMaterial;
    public Material orangeLightMaterial;
    public Material greenLightMaterial;

    // TextBox showing the content of the observer collection
    public TMPro.TMP_Text observersTextBox;
    
    // Current state of traffic light
    private bool _isTrafficLightGreen = true;
    
    // Observer collection
    private readonly List<ICarObserver> _carObservers = new List<ICarObserver>();

    // Switch from green to red and from red to green in intervals of 1 second 
    private void Start()
    {
        InvokeRepeating(nameof(ChangeLights), 0f, 1f);
    }

    // Unity "Update" method with different method signature, not related to the Observer Pattern..
    // Updates the TextBox with the observer collection content
    private void Update()
    {
        String label = "";
        if (_carObservers.Count != 0) {
            foreach (var carObserver in _carObservers)
            {
                label += carObserver.ToString().Replace("(Clone)","") + "\n";
            }
            
            observersTextBox.SetText(label);
        }
    }

    // Method which toggles the traffic light state and then calls the observer notification method
    private void ChangeLights()
    {
        if (_isTrafficLightGreen)
        {
            TurnRed();
            NotifyObservers();
        }
        else
        {
            TurnGreen();
            NotifyObservers();
        }
    }
    
    void TurnGreen()
    {
        greenLightMaterial.EnableKeyword("_EMISSION");
        orangeLightMaterial.DisableKeyword("_EMISSION");
        redLightMaterial.DisableKeyword("_EMISSION");
        _isTrafficLightGreen = true;
    }

    void TurnRed()
    {
        greenLightMaterial.DisableKeyword("_EMISSION");
        orangeLightMaterial.DisableKeyword("_EMISSION");
        redLightMaterial.EnableKeyword("_EMISSION");
        _isTrafficLightGreen = false;
    }

    public void RegisterObserver(ICarObserver car)
    {
        _carObservers.Add(car);
    }

    public void UnregisterObserver(ICarObserver car)
    {
        _carObservers.Remove(car);
    }

    public void NotifyObservers()
    {
        foreach (var carObserver in _carObservers)
        {
            carObserver.Update(_isTrafficLightGreen);
        }
    }
}

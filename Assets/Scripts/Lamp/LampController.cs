using System.Collections.Generic;
using UnityEngine;


public class LampController : Singleton<LampController>
{
    List<LampContainer> lampContainers = new List<LampContainer>();


    //Flag
    private bool lampsAvailable;

    //Spawning
    private GameObject emptyGameObject;

    //Components
    private Transform trans;


    private void Awake()
    {
        //Spawning
        emptyGameObject = new GameObject();

        //Components
        trans = transform;
    }


    public void AddLamp(HueLamp _lamp)
    {
        lampsAvailable = true;

        GameObject newLamp = Instantiate(emptyGameObject, trans);
        LampContainer lampContainer = newLamp.AddComponent<LampContainer>();

        lampContainers.Add(lampContainer);
        lampContainer.SetLamp(_lamp);
    }


    public void SetColor(Color _mainColor, ColorType _colorType, bool _overrideAll, float _time)
    {
        UpdateColor(_mainColor, Color.white, _colorType, _overrideAll, _time);
    }


    public void SetColor(Color _mainColor, Color _lerpColor, ColorType _colorType, bool _overrideAll, float _time)
    {
        UpdateColor(_mainColor, _lerpColor, _colorType, _overrideAll, _time);
    }


    private void UpdateColor(Color _mainColor, Color _lerpColor, ColorType _colorType, bool _overrideAll, float _time)
    {
        if (!lampsAvailable) return;

        if (_overrideAll)
        {
            foreach (LampContainer lamp in lampContainers)
            {
                lamp.ActivateLamp(_colorType, new ColorSet(_mainColor, _lerpColor), _time);
            }
        }
        else
        {
            lampContainers[Random.Range(0, lampContainers.Count)].ActivateLamp(_colorType, new ColorSet(_mainColor, _lerpColor), _time);
        }
    }


    public void DeactivateColor(ColorType _colorType)
    {
        if (_colorType == ColorType.Ambient || _colorType == ColorType.Music)
        {
            foreach (LampContainer lamp in lampContainers)
            {
                lamp.DeactivateLamp(_colorType);
            }
        }
    }
}
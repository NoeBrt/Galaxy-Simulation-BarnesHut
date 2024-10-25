using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulation.Struct;

namespace Simulation
{
    public enum SimulationType
    {
        Galaxy,
        Collision,
        Universe,
        Photo
    }
    public class SimulationParameter : MonoBehaviour
    {

        public float BodiesCount{get; set;}

        public float Radius{get; set;}
        public float Thickness{get; set;}//utile seulement pour le type galaxies

        public float InitialVelocity{get; set;}

        public float SmoothingLength{get; set;}

        public float BlackHoleMass{get; set;}
        public float Distance{get; set;}

        public float InteractionRate{get; set;}
        public float TimeStep{get; set;}

        public SimulationType simulationType { get; set; }

    private DynamicColor _color;

    public DynamicColor Color
    {
        get { return _color; }
        set { _color = value; }
    }

    public void setColorStart(Vector3 colorStart)
    {
        _color.colorStart = new Color(colorStart.x, colorStart.y, colorStart.z, 0.5f);
    }

    public void setColorEnd(Vector3 colorEnd)
    {
        _color.colorEnd = new Color(colorEnd.x, colorEnd.y, colorEnd.z, 0.5f);
    }

    public void setColorStartAlpha(float alpha)
    {
        _color.colorStart.a = alpha;
    }

    public void setColorEndAlpha(float alpha)
    {
        _color.colorEnd.a = alpha;
    }

    public void setDivider(float divider)
    {
        _color.divider = divider;
    }

    public void Init(int simulationType)
    {
        this.simulationType = (SimulationType)simulationType;
        SimulationDefaults defaults = GlobalManager.Instance.DefaultsList[simulationType];
        _color = defaults.color;
    }}
}

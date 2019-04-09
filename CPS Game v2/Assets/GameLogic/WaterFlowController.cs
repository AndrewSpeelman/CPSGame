using Assets.Modules.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the water flow between modules
/// </summary>
public class WaterFlowController : MonoBehaviour
{
    public Reservoir Reservoir;

    private Module firstModule;

    private void Start()
    {
        Module currMod = Reservoir;
        //while (currMod.PreviousModule)
        //{
         //   currMod = currMod.PreviousModule;
        //}
        //this.firstModule = currMod;
        //this.firstModule.Water = new WaterObject();
    }

    /// <summary>
    /// Makes time move (tick) forward for the modules.  Ticking time forward allows for the water to flow through
    /// the system.
    /// </summary>
    public void TickModules()
    {
        // Flow water through the reservoir, to start flow through everything else
        try
        {
            WaterObject water = this.Reservoir.OnFlow(new WaterObject());

            var currentModule = this.Reservoir.NextModule;
            while (currentModule != null)
            {
                water = currentModule.OnFlow(water);
                if (water == null)
                {
                    // Flow is blocked

                }
                currentModule = currentModule.NextModule;
            }
        }
        catch (Exception e)
        {

        }
    }

    /// <summary>
    /// Starts ticking time forward for the modules in regular intervals
    /// </summary>
    /// <param name="secondsBetweenTicks">The amount of time in between ticks</param>
    public void StartWaterFlow(float secondsBetweenTicks)
    {
        this.InvokeRepeating("TickModules", 0.1f, secondsBetweenTicks);
    }
}

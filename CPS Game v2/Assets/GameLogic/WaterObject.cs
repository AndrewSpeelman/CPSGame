using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObject
{ 
    public bool[] purity = new bool[] { false, false, false };


    public bool Purity1
    {
        get
        {
            return purity[0];
        }
        set
        {
            purity[0] = value;
        }
    }

    public bool Purity2
    {
        get
        {
            return purity[1];
        }
        set
        {
            purity[1] = value;
        }
    }

    public bool Purity3
    {
        get
        {
            return purity[2];
        }
        set
        {
            purity[2] = value;
        }
    }


    public WaterObject Copy()
    {
        WaterObject cpy = new WaterObject();
        cpy.Purity1 = this.Purity1;
        cpy.Purity2 = this.Purity2;
        cpy.Purity3 = this.Purity3;

        return cpy;
    }

}

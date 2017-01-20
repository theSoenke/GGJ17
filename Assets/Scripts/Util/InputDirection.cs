using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


struct InputDirection
{
    private float _x;
    private float _y;

    public float X
    {
        get
        {
            return _x;
        }

        set
        {
            _x = value;
        }
    }

    public float Y
    {
        get
        {
            return _y;
        }

        set
        {
            _y = value;
        }
    }
    public InputDirection(float x, float y)
    {
        _x = x;
        _y = y;
    }
}


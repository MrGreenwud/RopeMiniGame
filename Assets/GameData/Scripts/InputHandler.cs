using System;
using UnityEngine;

public static class InputHandler
{
    public static Platform Platform = Platform.PC;

    public static bool BeginTouchScreen()
    {
        switch(Platform)
        {
            case Platform.PC:
                return Input.GetMouseButtonDown(0);
            case Platform.Mobile:
                return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        }

        throw new NotImplementedException($"BeginTouchScreen not implemented for platform: {Platform}");
            
    }

    public static bool EndTouchScreen()
    {
        switch (Platform)
        {
            case Platform.PC:
                return Input.GetMouseButtonUp(0);
            case Platform.Mobile:
                return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
        }

        throw new NotImplementedException($"EndTouchScreen not implemented for platform: {Platform}");
    }

    public static Vector2 TouchPosition()
    {
        switch (Platform)
        {
            case Platform.PC:
                return Input.mousePosition;
            case Platform.Mobile:
                return Input.touchCount > 0 ? Input.GetTouch(0).position : Vector2.zero;
        }

        throw new NotImplementedException($"TouchPosition not implemented for platform: {Platform}");
    }
}
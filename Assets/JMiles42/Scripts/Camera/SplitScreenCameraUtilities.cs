using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SplitScreenCameraUtilities
{
    public static Rect SetCameraRect(CameraMode mode)
    {
        switch (mode)
        {
            case CameraMode.TwoPlayerLeft:
                return new Rect(0f, 0f, 0.5f, 1);
            case CameraMode.TwoPlayerRight:
                return new Rect(0.5f, 0f, 0.5f, 1);
            case CameraMode.FourPlayerLeftUpper:
                return new Rect(0f, 0f, 0.5f, 0.5f);
            case CameraMode.FourPlayerLeftLower:
                return new Rect(0f, 0.5f, 0.5f, 0.5f);
            case CameraMode.FourPlayerRightUpper:
                return new Rect(0.5f, 0f, 0.5f, 0.5f);
            case CameraMode.FourPlayerRightLower:
                return new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            case CameraMode.FullScreen:
            default:
                return new Rect(0f, 0f, 1f, 1f);
        }
    }
    public enum CameraMode
    {
        FullScreen,
        TwoPlayerLeft,
        TwoPlayerRight,
        FourPlayerLeftUpper,
        FourPlayerLeftLower,
        FourPlayerRightUpper,
        FourPlayerRightLower
    }
}

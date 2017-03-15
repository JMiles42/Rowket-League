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
            case CameraMode.TwoPlayerUpper:
                return new Rect(0f, 0f, 1f, 0.5f);
            case CameraMode.TwoPlayerLower:
                return new Rect(0.5f, 0f, 1f, 0.5f);
            case CameraMode.ThreePlayerLowerMiddle:
                return new Rect(0.25f, 0.5f, 0.5f, 0.5f);
            case CameraMode.FourPlayerLeftUpper:
                return new Rect(0f, 0f, 0.5f, 0.5f);
            case CameraMode.FourPlayerLeftLower:
                return new Rect(0f, 0.5f, 0.5f, 0.5f);
            case CameraMode.FourPlayerRightUpper:
                return new Rect(0.5f, 0f, 0.5f, 0.5f);
            case CameraMode.FourPlayerRightLower:
                return new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            default:
                return new Rect(0f, 0f, 1f, 1f);
        }
    }

    public enum CameraMode
    {
        FullScreen,
        TwoPlayerLeft,
        TwoPlayerRight,
        TwoPlayerUpper,
        TwoPlayerLower,
        ThreePlayerLowerMiddle,
        FourPlayerLeftUpper,
        FourPlayerLeftLower,
        FourPlayerRightUpper,
        FourPlayerRightLower
    }
}
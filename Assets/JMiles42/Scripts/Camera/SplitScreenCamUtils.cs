using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SplitScreenCamUtils
{
    public static Rect SetCameraRect(CameraMode mode)
    {
        const float fifth = 1f / 5f;
        const float third = 1f / 3f;
        const float forth = 1f / 4f;
        const float thir2 = third + third;
        const float half_ = 0.5f;
        const float zero_ = 0f;
        const float quart = 0.25f;
        const float one__ = 1f;

        switch (mode)
        {
            default:
                return new Rect(zero_, zero_, one__, one__);

            case CameraMode.TwoPlayerLeft:
                return new Rect(zero_, zero_, half_, one__);
            case CameraMode.TwoPlayerRight:
                return new Rect(half_, zero_, half_, one__);
            case CameraMode.TwoPlayerUpper:
                return new Rect(zero_, half_, one__, half_);
            case CameraMode.TwoPlayerLower:
                return new Rect(half_, zero_, one__, half_);
            case CameraMode.ThreePlayerLowerMiddle:
                return new Rect(one__, zero_, half_, half_);
            case CameraMode.FourPlayerLeftUpper:
                return new Rect(zero_, half_, half_, half_);
            case CameraMode.FourPlayerLeftLower:
                return new Rect(zero_, zero_, half_, half_);
            case CameraMode.FourPlayerRightUpper:
                return new Rect(half_, half_, half_, half_);
            case CameraMode.FourPlayerRightLower:
                return new Rect(half_, zero_, half_, half_);

            case CameraMode.MiddleOfScreen:
                return new Rect(quart, quart, half_, half_);
            case CameraMode.MiddleOfScreenSmall:
                return new Rect(third, third, third, third);
            case CameraMode.MiddleOfScreenMiny:
                return new Rect(fifth * 2, fifth * 2, fifth, fifth);

            case CameraMode.SixUpper10:
                return new Rect(zero_, thir2, half_, third);
            case CameraMode.SixUpper01:
                return new Rect(half_, thir2, half_, third);
            case CameraMode.SixMiddl10:
                return new Rect(zero_, third, half_, third);
            case CameraMode.SixMiddl01:
                return new Rect(half_, third, half_, third);
            case CameraMode.SixLower10:
                return new Rect(zero_, zero_, half_, third);
            case CameraMode.SixLower01:
                return new Rect(half_, zero_, half_, third);


            case CameraMode.NineUpper100:
                return new Rect(zero_, thir2, third, third);
            case CameraMode.NineMiddl100:
                return new Rect(zero_, third, third, third);
            case CameraMode.NineLower100:
                return new Rect(zero_, zero_, third, third);
            case CameraMode.NineUpper010:
                return new Rect(third, thir2, third, third);
            case CameraMode.NineMiddl010:
                return new Rect(third, third, third, third);
            case CameraMode.NineLower010:
                return new Rect(third, zero_, third, third);
            case CameraMode.NineUpper001:
                return new Rect(thir2, thir2, third, third);
            case CameraMode.NineMiddl001:
                return new Rect(thir2, third, third, third);
            case CameraMode.NineLower001:
                return new Rect(thir2, zero_, third, third);


            case CameraMode.TenUpper10000:
                return new Rect(zero_, half_, fifth, half_);
            case CameraMode.TenUpper01000:
                return new Rect(fifth, half_, fifth, half_);
            case CameraMode.TenUpper00100:
                return new Rect(fifth * 2, half_, fifth, half_);
            case CameraMode.TenUpper00010:
                return new Rect(fifth * 3, half_, fifth, half_);
            case CameraMode.TenUpper00001:
                return new Rect(fifth * 4, half_, fifth, half_);
            case CameraMode.TenLower10000:
                return new Rect(zero_, zero_, fifth, half_);
            case CameraMode.TenLower01000:
                return new Rect(fifth, zero_, fifth, half_);
            case CameraMode.TenLower00100:
                return new Rect(fifth * 2, zero_, fifth, half_);
            case CameraMode.TenLower00010:
                return new Rect(fifth * 3, zero_, fifth, half_);
            case CameraMode.TenLower00001:
                return new Rect(fifth * 4, zero_, fifth, half_);

            case CameraMode.TelveUpper1000:
                return new Rect(zero_, thir2, forth, third);
            case CameraMode.TelveUpper0100:
                return new Rect(forth, thir2, forth, third);
            case CameraMode.TelveUpper0010:
                return new Rect(forth * 2, thir2, forth, third);
            case CameraMode.TelveUpper0001:
                return new Rect(forth * 3, thir2, forth, third);

            case CameraMode.TelveMiddl1000:
                return new Rect(zero_, third, forth, third);
            case CameraMode.TelveMiddl0100:
                return new Rect(forth, third, forth, third);
            case CameraMode.TelveMiddl0010:
                return new Rect(forth * 2, third, forth, third);
            case CameraMode.TelveMiddl0001:
                return new Rect(forth * 3, third, forth, third);

            case CameraMode.TelveLower1000:
                return new Rect(zero_, zero_, forth, third);
            case CameraMode.TelveLower0100:
                return new Rect(forth, zero_, forth, third);
            case CameraMode.TelveLower0010:
                return new Rect(forth * 2, zero_, forth, third);
            case CameraMode.TelveLower0001:
                return new Rect(forth * 3, zero_, forth, third);
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
        FourPlayerRightLower,
        MiddleOfScreen,
        MiddleOfScreenSmall,
        MiddleOfScreenMiny,

        SixUpper01,
        SixUpper10,
        SixMiddl10,
        SixMiddl01,
        SixLower10,
        SixLower01,

        NineUpper100,
        NineMiddl100,
        NineLower100,
        NineUpper010,
        NineMiddl010,
        NineLower010,
        NineUpper001,
        NineMiddl001,
        NineLower001,

        TenUpper10000,
        TenUpper01000,
        TenUpper00100,
        TenUpper00010,
        TenUpper00001,
        TenLower10000,
        TenLower01000,
        TenLower00100,
        TenLower00010,
        TenLower00001,

        TelveUpper1000,
        TelveUpper0100,
        TelveUpper0010,
        TelveUpper0001,

        TelveMiddl1000,
        TelveMiddl0100,
        TelveMiddl0010,
        TelveMiddl0001,

        TelveLower1000,
        TelveLower0100,
        TelveLower0010,
        TelveLower0001,
    }
}
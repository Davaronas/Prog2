using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Controls
{
    public enum MovementDirections { Forward, Back, Left, Right };

    public static KeyCode Forward = KeyCode.W;
    public static KeyCode Left = KeyCode.A;
    public static KeyCode Right = KeyCode.D;
    public static KeyCode Back = KeyCode.S;

    public static KeyCode Jetpack = KeyCode.Space;

    public static KeyCode MainAttack = KeyCode.Mouse0;


    public static KeyCode SwitchToSecondary = KeyCode.Alpha1;
    public static KeyCode SwitchToMain = KeyCode.Alpha2;


}

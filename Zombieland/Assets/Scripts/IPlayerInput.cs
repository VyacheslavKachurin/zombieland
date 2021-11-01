using System;
using UnityEngine;
public interface IPlayerInput
{
    public event Action<Vector3> OnMouseMoved;
    public event Action<float, float> OnAxisMoved;
    public event Action<bool> OnShootingInput;
    public event Action<bool> OnScrollWheelSwitched;
    public event Action OnReloadPressed;
}

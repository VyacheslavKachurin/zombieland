using System;
using UnityEngine;
public interface IPlayerInput
{
    public event Action<Vector3> CursorMoved;
    public event Action<float, float> Moved;
    public event Action<bool> OnShootingInput;
    public event Action<bool> OnScrollWheelSwitched;
    public event Action OnReloadPressed;
    public event Action<bool> SprintingSwitched;
    public event Action JumpPressed;
    public event Action<bool> AimedWeapon;
    public event Action HolsteredWeapon;
    public event Action InteractButtonPressed;
}

using UnityEngine;

public static class PlayerInputs
{
    public static bool PlayerOneJump()
    {
        return Input.GetButtonDown("JumpOne");
    }

    public static bool PlayerTwoJump()
    {
        return Input.GetButtonDown("JumpTwo");
    }

    public static float PlayerOneHorizontal()
    {
        return Input.GetAxis("PlayerOneHorizontal");
    }

    public static float PlayerTwoHorizontal()
    {
        return Input.GetAxis("PlayerTwoHorizontal");
    }
}

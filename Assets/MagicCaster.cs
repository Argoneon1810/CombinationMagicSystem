using System;
using UnityEngine;

public class MagicCaster : MonoBehaviour
{
    [SerializeField] MagicRealizer realizer;
    [SerializeField] SkillSlot slot;
    [SerializeField] bool instantMagicMode = false;
    [SerializeField] int instantMagicIndex = 0;

    [SerializeField] private Transform castingPosition;

    public Vector3 CastingPosition() => castingPosition.position;

    private void Start()
    {
        realizer = MagicRealizer.Instance;
    }

    private void Update()
    {
        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Instant Magic Mode Activated.");
            instantMagicMode = true;
        }
        if (instantMagicMode)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Instant Magic Mode Deactivated");
                instantMagicMode = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                instantMagicIndex = instantMagicIndex * 10 + 1;
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                instantMagicIndex = instantMagicIndex * 10 + 2;
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
                instantMagicIndex = instantMagicIndex * 10 + 3;
            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
                instantMagicIndex = instantMagicIndex * 10 + 4;
            if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
                instantMagicIndex = instantMagicIndex * 10 + 5;
            if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
                instantMagicIndex = instantMagicIndex * 10 + 6;
            if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
                instantMagicIndex = instantMagicIndex * 10 + 7;
            if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
                instantMagicIndex = instantMagicIndex * 10 + 8;
            if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
                instantMagicIndex = instantMagicIndex * 10 + 9;
            if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
                instantMagicIndex = instantMagicIndex * 10;
            if (Input.GetKeyDown(KeyCode.Backspace))
                instantMagicIndex /= 10;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Debug.Log("Instant Magic Casted: " + instantMagicIndex);
                if(slot.TryGetMagic(instantMagicIndex, out MemorizeMagic magicToCast))
                    realizer.RealizeMagic(magicToCast, this);
                instantMagicIndex = 0;
            }
        }
    }
}

using UnityEngine;

public class MagicCaster : MonoBehaviour
{
    MemorizeMagic[] skillSlots = new MemorizeMagic[10];
    void Update()
    {
        if (Input.GetKeyDown("1"))
            skillSlots[0].CastMagic(this);
        if (Input.GetKeyDown("2"))
            skillSlots[1].CastMagic(this);
        if (Input.GetKeyDown("3"))
            skillSlots[2].CastMagic(this);
        if (Input.GetKeyDown("4"))
            skillSlots[3].CastMagic(this);
        if (Input.GetKeyDown("5"))
            skillSlots[4].CastMagic(this);
        if (Input.GetKeyDown("6"))
            skillSlots[5].CastMagic(this);
        if (Input.GetKeyDown("7"))
            skillSlots[6].CastMagic(this);
        if (Input.GetKeyDown("8"))
            skillSlots[7].CastMagic(this);
        if (Input.GetKeyDown("9"))
            skillSlots[8].CastMagic(this);
        if (Input.GetKeyDown("0"))
            skillSlots[9].CastMagic(this);
    }
}
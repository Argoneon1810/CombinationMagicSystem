using UnityEngine;

public class RigidbodyfyMagicExtra : MagicExtraScript
{
    private void Start()
    {
        var rb = gameObject.AddComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }
}

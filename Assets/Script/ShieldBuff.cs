using UnityEngine;
using System.Collections;

public class ShieldBuff : MonoBehaviour
{
    public float shieldDuration = 5f; // Durasi tameng dalam detik

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Ambil SpriteRenderer Player untuk efek transparan
            SpriteRenderer playerSprite = collision.GetComponent<SpriteRenderer>();
            
            if (playerSprite != null)
            {
                // Jalankan efek tameng di Player tanpa mengganggu script ini yang akan dihancurkan
                collision.gameObject.AddComponent<ShieldEffect>().ActivateShield(shieldDuration, playerSprite);
            }

            Destroy(gameObject); // Hancurkan item di map
        }
    }
}

// Script pembantu otomatis untuk memproses durasi shield pada player
public class ShieldEffect : MonoBehaviour
{
    public void ActivateShield(float duration, SpriteRenderer sprite)
    {
        StartCoroutine(ShieldRoutine(duration, sprite));
    }

    private IEnumerator ShieldRoutine(float duration, SpriteRenderer sprite)
    {
        
        // Buat player agak transparan (Alpha = 0.5f)
        Color originalColor = sprite.color;
        sprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);

        // TODO: Jika kamu punya sistem damage musuh, kamu bisa menambahkan variabel 
        // bool isInvulnerable di script Health player dan set ke true di sini.

        yield return new WaitForSeconds(duration);

        // Kembalikan ke normal (Alpha = 1f)
        sprite.color = originalColor;
        
        Destroy(this); // Hancurkan komponen pembantu ini
    }
}
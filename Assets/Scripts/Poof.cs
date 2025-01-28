using UnityEngine;
using System.Collections;

/// <summary>
/// This script handles the balloon popping animation on the destroyed Balloon. 
/// It requires a SpriteRenderer, as the SpriteRenderer is used in the script to cycle through the sprites of the animation.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class Poof : MonoBehaviour
{

    [Tooltip("The individual sprites of the animation")]
    public Sprite[] frames;
    [Tooltip("How fast does the animation play")]
    public float framesPerSecond;
    [Tooltip("An Audioclip with the sound that is played when the poof happens")]
    public AudioClip poofSound;
    SpriteRenderer spriteRenderer;

    // Wait this amount of seconds to destroy the game object.
    float destroyTimer = 1;

    /// <summary>
    /// Start is called by Unity. This will play our poof sound
    /// </summary>
    void Start()
    {
		if (poofSound != null) {
			AudioSource.PlayClipAtPoint(poofSound, transform.position);
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // In Update you should add the sprite animation as shown on the lecture slides.
    private void Update()
    {
        destroyTimer -= Time.deltaTime;
        if(destroyTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}

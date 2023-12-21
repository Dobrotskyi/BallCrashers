using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    private AudioSource _as;

    private void Awake()
    {
        _as = GetComponent<AudioSource>();
        _as.volume = SoundSettings.EFFECTS_VOLUME;
        TestFunc();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_as.enabled && !SoundSettings.AudioMuted)
            _as.Play();
    }

    private void TestFunc()
    {
        if (name == "PlayerBall")
        {
            GetComponent<Rigidbody2D>().AddForce(240 * -transform.right);
        }
        else
            GetComponent<Rigidbody2D>().AddForce(100 * transform.right);
    }
}

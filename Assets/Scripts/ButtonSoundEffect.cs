using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundEffect : MonoBehaviour
{
    public AudioClip clickSound;  // Ŭ���� �� ����� ȿ����

    private void Start()
    {
        clickSound = AudioManager.Instance.click;
        // �� ������Ʈ(�θ�) ���� ��� Button ������Ʈ�� �����ɴϴ�.
        Button[] buttons = GetComponentsInChildren<Button>();

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlayClickSound);
        }
    }

    void PlayClickSound()
    {
        if (clickSound != null)
        {
            AudioManager.Instance.sfxSource.PlayOneShot(clickSound);
        }
    }
}

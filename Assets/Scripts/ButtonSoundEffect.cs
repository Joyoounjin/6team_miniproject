using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundEffect : MonoBehaviour
{
    public AudioClip clickSound;  // 클릭할 때 재생할 효과음

    private void Start()
    {
        clickSound = AudioManager.Instance.click;
        // 이 오브젝트(부모) 안의 모든 Button 컴포넌트를 가져옵니다.
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

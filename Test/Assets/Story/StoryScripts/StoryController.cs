using System.Collections;
using UnityEngine;
using TMPro;
using Crosstales.RTVoice.Tool;
using Michsky.UI.Shift;
using UnityEngine.UI;

public class StoryController : MonoBehaviour
{
    [Header ("Story texts")]
    [TextArea(1, 10)]
    [SerializeField] private string[] text;

    [Header ("Story")]
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private ControllerStoryScene controller;
    [SerializeField] private StoryText currentText;

    [Header ("Text Speed")]
    [SerializeField] private float textSpeed;

    [Header ("Speaker")]
    [SerializeField] private SpeechText speakerScript;

    [Header ("Audio")]
    [SerializeField] private AudioSource audioSource;

    [Header("Spacebar To Continue Animation")]
    [SerializeField] private CanvasGroup spaceBarCanvas;
    [SerializeField] private Animator textAnim;

    [Header("Loading")]
    [SerializeField] private CanvasGroup loading;

    [Header("TimedEvent")]
    [SerializeField] private TimedEvent timedEventScript;

    [Header("Start Game Script")]
    [SerializeField] private StartGameScene startGameScript;


    private int _sentenceIndex = -1;
    private int _textIndex = -1;
    private int _textBlinkIndex = 0;

    private bool _isHidden = false;

    private State state = State.COMPLETED;
    private Animator _animator;

    private enum State
    {
        PLAYING, COMPLETED
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            startGameScript.StartGame();
        }
    }
    private void Speak(int index)
    {
        speakerScript.text = text[index];
        speakerScript.Speak();
    }

    public void Hide()
    {
        if(!_isHidden)
        {
            _animator.SetTrigger("Hide");
            _isHidden = true;
        }
    }

    public void Show()
    {
        _animator.SetTrigger("Show");
        _isHidden = false;
    }

    public void ClearText()
    {
        storyText.text = "";
    }
    public void PlayScene(StoryText text)
    {
        currentText = text;
        _sentenceIndex = -1;
        controller.currentTextIndex += 1;
        _textIndex += 1;
        controller.ChangeBackgroundImage();
        if (_textBlinkIndex == 1)
        {
            StartCoroutine(DisableSpaceBarCanvas());
        }
        else
        {
            spaceBarCanvas.alpha = 0;
        }
        if (_textIndex != 8)
        {
            Speak(_textIndex);
            PlayNextSentence();
            StartCoroutine(TypeSound());
        }
        else
        {
            loading.alpha = 1;
            timedEventScript.StartIEnumerator();
        }
    }

    public void PlayNextSentence()
    {
        StartCoroutine(TypeText(currentText.sentences[++_sentenceIndex].text));
    }

    public bool IsLastSentence()
    {
        return _sentenceIndex + 1 == currentText.sentences.Count;
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    private IEnumerator TypeText(string text)
    {
        yield return new WaitForSeconds(0.3f);
        storyText.text = "";
        state = State.PLAYING;
        int wordIndex = 0;

        while (state != State.COMPLETED)
        {
            storyText.text += text[wordIndex];
            yield return new WaitForSeconds(textSpeed);
            if(++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                audioSource.Stop();
                StartCoroutine(EnableSpaceBarCanvas());
                _textBlinkIndex = 1;
                break;
            }
        }
    }

    private IEnumerator TypeSound()
    {
        yield return new WaitForSeconds(0.3f);
        audioSource.Play();
    }

    private IEnumerator EnableSpaceBarCanvas()
    {
        spaceBarCanvas.alpha = 0f;
        yield return new WaitForSeconds(0.15f);
        spaceBarCanvas.alpha = 0.3f;
        yield return new WaitForSeconds(0.15f);
        spaceBarCanvas.alpha = 0.6f;
        yield return new WaitForSeconds(0.15f);
        spaceBarCanvas.alpha = 1f;
        textAnim.SetTrigger("TriggerStart");
    }

    private IEnumerator DisableSpaceBarCanvas()
    {
        spaceBarCanvas.alpha = 1f;
        yield return new WaitForSeconds(0.03f);
        spaceBarCanvas.alpha = 0.6f;
        yield return new WaitForSeconds(0.03f);
        spaceBarCanvas.alpha = 0.3f;
        yield return new WaitForSeconds(0.03f);
        spaceBarCanvas.alpha = 0f;
        textAnim.SetTrigger("TriggerStop");
    }
}

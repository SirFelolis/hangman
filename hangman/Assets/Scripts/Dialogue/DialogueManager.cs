using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text dialogueText;

    [SerializeField]
    private Animator animator;

    private AudioSource audioSource;

    private AudioClip[] voiceSound;

    private float voicePitch = 1f;

    private bool pitchVariation = false;

    private float talkSpeed = 0.05f;

    public static bool dialogueStarted = false;

    // When the typewriter effect is going on, this is true.
    public bool writing = false;

    private Queue<string> sentences;
    private string sentence;

    #region Singleton

    public static DialogueManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    #endregion

    private void Start()
    {
        sentences = new Queue<string>();

        audioSource = GetComponent<AudioSource>();
    }

    public void StartDialogue( Dialogue dialogue )
    {
        Debug.Log("Starting conversation with " + dialogue.name);

        dialogueStarted = true;

        voiceSound = dialogue.voiceSound;
        voicePitch = dialogue.voicePitch;
        pitchVariation = dialogue.pitchVariation;
        talkSpeed = dialogue.talkSpeed;

        animator.SetBool("isOpen", true);

        sentences.Clear();

        nameText.text = dialogue.name;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {

        string cleanSentence = CleanText(sentence);

        dialogueText.text = "";
        char[] letters = cleanSentence.ToCharArray();
        float talkSpeedMultiplier = 1;
        float pitchMultiplier = 1;
        writing = true;

        for (int i = 0; i < letters.Length; i++)
        {
            /*            if (letters[i] == '/' && char.IsDigit(letters[i + 1]) && char.IsDigit(letters[i + 3]))
                        {
                            talkSpeedMultiplier = float.Parse(letters[i + 1].ToString());
                            talkSpeedMultiplier += float.Parse(letters[i + 3].ToString()) / 10;
                            i += 4;
                        }

                        if (letters[i] == '#' && char.IsDigit(letters[i + 1]) && char.IsDigit(letters[i + 3]))
                        {
                            pitchMultiplier = float.Parse(letters[i + 1].ToString());
                            pitchMultiplier += float.Parse(letters[i + 3].ToString()) / 10;
                            i += 4;
                        }*/


            dialogueText.text += letters[i];
            if (letters[i] != ' ')
            {
                if (pitchVariation)
                    audioSource.pitch = Random.Range(voicePitch - 1.2f, voicePitch + 1.2f);

                audioSource.pitch = voicePitch * pitchMultiplier;
                audioSource.PlayOneShot(voiceSound[Random.Range(0, voiceSound.Length)]);

                yield return new WaitForSeconds(talkSpeed * talkSpeedMultiplier);
            }
            else
            {
                yield return null;
            }
        }
        writing = false;
    }

    public void SkipWriting()
    {
        char[] letters = sentence.ToCharArray();
        sentence = null;
        for (int i = 0; i < letters.Length; i++)
        {
            if (letters[i] == '/' && char.IsDigit(letters[i + 1]) && char.IsDigit(letters[i + 3]))
            {
                i += 4;
            }

            if (letters[i] == '#' && char.IsDigit(letters[i + 1]) && char.IsDigit(letters[i + 3]))
            {
                i += 4;
            }

            sentence += letters[i];
        }

        writing = false;
        StopAllCoroutines();
        dialogueText.text = sentence;
    }

    private string CleanText( string text , bool doReturnTag = false)
    {
        bool loop = false;
        string ret = "";
        string tag = "";

        foreach (char x in text)
        {
            if (x == '<')
            {
                loop = true;
                continue;
            }
            else if (x == '>')
            {
                loop = false;
                continue;
            }
            else if (loop)
            {
                tag += x;
                continue;
            }
            ret += x;
        }

        if (doReturnTag)
            return tag;

        return ret;
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation.");
        animator.SetBool("isOpen", false);
        dialogueStarted = false;
        writing = false;
        StopAllCoroutines();
    }

}

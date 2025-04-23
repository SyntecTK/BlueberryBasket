using System.Collections;
using UnityEngine;
using TMPro;

public class WordByWordTextDisplay : MonoBehaviour {
    [Header("UI Components")]
    public TMP_Text uiText;

    [Header("Typing Settings")]
    [TextArea(3, 10)]
    public string fullText;
    public float wordDelay = 0.5f;
    public float sentenceDelay = 1.0f;

    private Coroutine typingCoroutine;

    void Start() {
        if (uiText != null) {
            StartTyping();
        }
    }

    public void StartTyping() {
        if (typingCoroutine != null) {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeTextWordByWord());
    }

    IEnumerator TypeTextWordByWord() {
        uiText.text = "";
        string[] words = fullText.Split(' ');

        for (int i = 0; i < words.Length; i++) {
            uiText.text += words[i];

            // Add space unless it's the last word
            if (i < words.Length - 1)
                uiText.text += " ";

            // Check if the word ends with a sentence-ending punctuation
            char lastChar = words[i][words[i].Length - 1];
            if (lastChar == '.' || lastChar == '!' || lastChar == '?') {
                yield return new WaitForSeconds(sentenceDelay);
            } else {
                yield return new WaitForSeconds(wordDelay);
            }
        }
    }
}
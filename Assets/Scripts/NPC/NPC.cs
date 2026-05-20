using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    private bool playerNearby;

    [Header("UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_InputField playerInput;
    [SerializeField] private Button sendButton;

    [Header("NPC Personality")]
    [TextArea]
    [SerializeField]
    private string npcContext =
        "You are a friendly fantasy blacksmith named Borin.";

    [Header("Groq API Key")]
    [SerializeField]
    private string apiKey = "YOUR_GROQ_API_KEY";

    private const string endpoint =
        "https://api.groq.com/openai/v1/chat/completions";

    private void Awake()
    {
        dialoguePanel.SetActive(false);

        playerInput.gameObject.SetActive(false);
        sendButton.gameObject.SetActive(false);

        sendButton.onClick.AddListener(SendMessageToNPC);
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.W))
        {
            OpenDialogue();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseDialogue();
        }

        if (dialoguePanel.activeSelf &&
            Input.GetKeyDown(KeyCode.Return))
        {
            SendMessageToNPC();
        }
    }

    private void OpenDialogue()
    {
        dialoguePanel.SetActive(true);

        playerInput.gameObject.SetActive(true);
        sendButton.gameObject.SetActive(true);

        dialogueText.text = "Hello traveler.";

        playerInput.ActivateInputField();
    }

    private void CloseDialogue()
    {
        dialoguePanel.SetActive(false);

        playerInput.gameObject.SetActive(false);
        sendButton.gameObject.SetActive(false);
    }

    public void SendMessageToNPC()
    {
        string playerMessage = playerInput.text.Trim();

        if (!string.IsNullOrEmpty(playerMessage))
        {
            StartCoroutine(GetGroqResponse(playerMessage));
        }
    }

    private IEnumerator GetGroqResponse(string playerMessage)
    {
        dialogueText.text = "Thinking...";

        string fullPrompt =
            npcContext +
            "\nPlayer: " + playerMessage +
            "\nNPC:";

        string jsonBody =
            "{"
            + "\"model\":\"llama-3.1-8b-instant\","
            + "\"messages\":["
            + "{"
            + "\"role\":\"user\","
            + "\"content\":\"" + EscapeJson(fullPrompt) + "\""
            + "}"
            + "]"
            + "}";

        Debug.Log(jsonBody);

        UnityWebRequest request =
            new UnityWebRequest(endpoint, "POST");

        byte[] bodyRaw =
            Encoding.UTF8.GetBytes(jsonBody);

        request.uploadHandler =
            new UploadHandlerRaw(bodyRaw);

        request.downloadHandler =
            new DownloadHandlerBuffer();

        request.SetRequestHeader(
            "Content-Type",
            "application/json"
        );

        request.SetRequestHeader(
            "Authorization",
            "Bearer " + apiKey
        );

        yield return request.SendWebRequest();

        Debug.Log("STATUS: " + request.responseCode);
        Debug.Log(request.downloadHandler.text);

        if (request.result ==
            UnityWebRequest.Result.Success)
        {
            string response =
                request.downloadHandler.text;

            string aiText =
                ExtractContent(response);

            if (!string.IsNullOrEmpty(aiText))
            {
                dialogueText.text = aiText;
            }
            else
            {
                dialogueText.text =
                    GetFallbackDialogue(playerMessage);
            }
        }
        else
        {
            Debug.LogError(request.error);
            Debug.LogError(request.downloadHandler.text);

            dialogueText.text =
                GetFallbackDialogue(playerMessage);
        }

        playerInput.text = "";

        playerInput.ActivateInputField();
    }

    private string ExtractContent(string json)
    {
        string search = "\"content\":\"";

        int start = json.IndexOf(search);

        if (start == -1)
        {
            return null;
        }

        start += search.Length;

        StringBuilder result = new StringBuilder();

        bool escape = false;

        for (int i = start; i < json.Length; i++)
        {
            char c = json[i];

            if (escape)
            {
                switch (c)
                {
                    case 'n':
                        result.Append('\n');
                        break;

                    case '"':
                        result.Append('"');
                        break;

                    case '\\':
                        result.Append('\\');
                        break;

                    default:
                        result.Append(c);
                        break;
                }

                escape = false;
            }
            else
            {
                if (c == '\\')
                {
                    escape = true;
                }
                else if (c == '"')
                {
                    break;
                }
                else
                {
                    result.Append(c);
                }
            }
        }

        return result.ToString();
    }

    private string EscapeJson(string text)
    {
        return text
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"")
            .Replace("\n", "\\n")
            .Replace("\r", "");
    }

    private string GetFallbackDialogue(string playerMessage)
    {
        playerMessage =
            playerMessage.ToLower();

        if (playerMessage.Contains("hello") ||
            playerMessage.Contains("hi"))
        {
            return "Greetings traveler.";
        }

        if (playerMessage.Contains("weapon"))
        {
            return "I forge the finest weapons in the kingdom.";
        }

        if (playerMessage.Contains("bye"))
        {
            return "Safe travels.";
        }

        return "The forge is hot today.";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = false;

            CloseDialogue();
        }
    }
}
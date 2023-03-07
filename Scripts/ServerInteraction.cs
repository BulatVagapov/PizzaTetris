using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ServerInteraction : MonoBehaviour
{
    private string token;
    private string base_URL;
    string responseEncoded;


    static System.Random rnd = new System.Random();

    const string KEY_1 = "supersecretkey";
    const string KEY_2 = "anothersecretkey";

    public static string EncodeString(string s)
    {
        var bytes1 = new List<byte>();
        int ki = 0;
        foreach (var c in s)
        {
            char k = KEY_1[ki];
            char k1 = (char)((k - '0') << 1);
            bytes1.Add((byte)(c ^ k ^ k1));

            ki = (ki + 1) % KEY_1.Length;
        }
        var s1 = Convert.ToBase64String(bytes1.ToArray());

        string s2 = "";
        ki = 0;
        const char minC = '(';
        const char maxC = 'Z';

        for (int i = 0; i < s1.Length; i++)
        {
            char c = s1[i];
            int d = ((c - minC) + (KEY_2[ki] - 'a'));
            while (d >= maxC - minC)
            {
                s2 += (char)(' ' + rnd.Next('\'' - ' '));
                d -= maxC - minC;
            }

            s2 += (char)(minC + d);
            ki = (ki + 1) % KEY_2.Length;
        }

        return s2;
    }

    public static string DecodeString(string s2)
    {

        int ki = 0;
        char minC = '(';
        char maxC = 'Z';
        int minusFlag = 0;
        string s1 = "";
        for (int i = 0; i < s2.Length; i++)
        {
            char c = s2[i];
            if (c >= ' ' && c <= '\'')
            {
                minusFlag++;
                continue;
            }
            int nc = (c - minC) - (KEY_2[ki] - 'a');
            if (minusFlag > 0)
            {
                nc += minusFlag * (maxC - minC);
                minusFlag = 0;
            }
            s1 += (char)(minC + nc);
            ki = (ki + 1) % KEY_2.Length;
        }

        var s = Convert.FromBase64String(s1);

        var res = "";
        ki = 0;
        foreach (var c in s)
        {
            char k = KEY_1[ki];
            char k1 = (char)((k - '0') << 1);
            res += (char)(c ^ k ^ k1);

            ki = (ki + 1) % KEY_1.Length;
        }
        return res;

    }

    public void SetTokenFromIndex(string tokenFromJS, string URL)
    {
        token = tokenFromJS;
        base_URL = URL;
        StartCoroutine(GetID());
    }

    IEnumerator GetID()
    {
        UnityWebRequest webRequest = new UnityWebRequest(base_URL + "api/user/bonusGameStart");
        webRequest.SetRequestHeader("Authorization", token);
        DownloadHandlerBuffer dH = new DownloadHandlerBuffer();
               
        webRequest.downloadHandler = dH;
        yield return webRequest.SendWebRequest();

        string sessionId = webRequest.downloadHandler.text;

        string sessionDecoded = DecodeString(sessionId);
        // генерируем ответ
        // 1. разбиваем идетификатор сессии по знаку #, переставляем местами и скливаем назад
        string responseRaw = string.Join("#", sessionDecoded.Split('#').Reverse()) + "#" + rnd.Next();
        // 2. кодируем
        responseEncoded = EncodeString(responseRaw);
    }

    public IEnumerator SendMessageToServer()
    {
        UnityWebRequest webRequest = UnityWebRequest.Post(base_URL + "api/user/bonusGameFinish", responseEncoded);
        webRequest.SetRequestHeader("Authorization", token);
        yield return webRequest.SendWebRequest();
    }

    public void SendButton()
    {
        StartCoroutine(SendMessageToServer());
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

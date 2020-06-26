using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using UnityEngine.UI;
using TMPro;

public class AnswerRecorder : MonoBehaviour
{
    private string _email = "";
    private string _password = "";

    [SerializeField]
    private bool _recordAnswers;

    [SerializeField]
    private bool _sendEmail;

    [Space(5)]
    [SerializeField]
    private TMP_InputField _inputField;

    [SerializeField]
    private TextMeshProUGUI _submittedClarification;

    [SerializeField]
    private string _fileName;

    private Stream sr;

    private List<string> _fullRecordedText = new List<string>();



    private void Start()
    {
        if (_recordAnswers)
        {
            var sr = File.CreateText(_fileName);
            sr.Close();
        }
    }

    public void SendEmail()
    {
        string name = _inputField.text;
        if (_sendEmail)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_email);
            mail.To.Add(_email);
            mail.Subject = "Romeo and Juliet Test Data From " + name;
            string text = "";
            for (int i = 0; i < _fullRecordedText.Count; i++)
                text += _fullRecordedText[i];
            mail.Body = text;
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

            smtpServer.Port = 587;
            smtpServer.Credentials = new NetworkCredential(_email, _password);
            smtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
            smtpServer.Send(mail);
            StartCoroutine(DisplaySubmitClarification());
        }
    }


    public void WriteLine(string s, string speakerName)
    {
        _fullRecordedText.Add(speakerName + ": " + s.Trim());
        _fullRecordedText.Add("\n");
        _fullRecordedText.Add("\n");
        if (_recordAnswers)
        {
            var sr = new StreamWriter(_fileName);
            for(int i = 0;i < _fullRecordedText.Count; i++)
            {
                sr.WriteLine(_fullRecordedText[i]);

            }
            
            sr.Close();
        }
    }

    IEnumerator DisplaySubmitClarification()
    {
        yield return new WaitForSeconds(1f);
        _submittedClarification.gameObject.SetActive(true); 
    }

   
}

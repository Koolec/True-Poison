using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Borshevik.EnemyControl
{
    public class DataSender : MonoBehaviour
    {
        string URL = "https://docs.google.com/forms/d/e/1FAIpQLSf5QhB8a2vrsmjFIiI4Mj1cLwXU7s49UyBwFpX5ZIEn-GIiPA/formResponse";
        string model = "";

        private void Start()
        {
            model = SystemInfo.deviceUniqueIdentifier;
        }

        public void Send(string type)
        {
            string[] parameters =  new string[] {type};
            StartCoroutine(Post(model, parameters));
        }

        IEnumerator Post(string model, string[] statistics)
        {
            WWWForm form = new WWWForm();
            form.AddField("entry.271066105", model);
            form.AddField("entry.219496594", statistics[0]);
            form.AddField("entry.305119309", "-");

            UnityWebRequest www = UnityWebRequest.Post(URL, form);
            
            yield return www.SendWebRequest();

        }
    }
}
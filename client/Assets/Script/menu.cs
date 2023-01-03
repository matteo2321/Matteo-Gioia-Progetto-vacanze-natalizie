using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Net.Sockets;
using System.IO.Pipes;
using System.IO;
using System.Text;
using System.Runtime.InteropServices.ComTypes;

public class menu : MonoBehaviour
{
    string ip;
    string port;
    public GameObject inputFieldIp;
    public GameObject inputFieldPort;
    public GameObject connection_switch_color;
    public GameObject Bt_Play;


    public bool IsConnected = false;



    private void Start()
    {
        
        connection_switch_color.GetComponent<Image>().color = new Color(255, 0, 0, 255);//imposto segnalino a rosso
        Bt_Play.SetActive(IsConnected);//nascondo il bottone play

    }
    private void Update()
    {
        Bt_Play.SetActive(IsConnected);//aggiorno l'hiding del pulsante play 

    }



    public void tryConnect()
    {
        Bt_Play.SetActive(IsConnected);
        TcpClient tcpClient = new TcpClient();
        ip = inputFieldIp.GetComponent<TMP_Text>().text;
        port = inputFieldPort.GetComponent<TMP_Text>().text;
        try
        {
            int a = 0;
            int.TryParse(port, out a);
            NetworkStream networkStream = tcpClient.GetStream();
            StreamReader reader = new StreamReader(networkStream, Encoding.UTF8);
            StreamWriter writer = new StreamWriter(networkStream, Encoding.UTF8);

            //il tcp client si connette con ip e porta al server
            //invia il suo ip e la sua porta per farsi riconoscere 
            tcpClient.Connect(ip, a);
            string messagge = "host: " + ip + ":" + port + "-->" + "avaible";




            byte[] buffer = Encoding.UTF8.GetBytes(messagge);
            networkStream.Write(buffer, 0, buffer.Length);

            buffer = new Byte[256];

            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = networkStream.Read(buffer, 0, buffer.Length);
            responseData = System.Text.Encoding.UTF8.GetString(buffer, 0, bytes);

            if (responseData != null && responseData == "accepted")
            {
                connection_switch_color.GetComponent<Image>().color = new Color(0, 255, 0, 255);
                IsConnected = true;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }



    public void play()
    {
        if (connection_switch_color.GetComponent<Image>().color == new Color(0,255,0,255) && IsConnected==true)//controlla che il segnaletto sia verde quindi che ci sia la connessione 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void exit()
    {
        Application.Quit();
    }

}

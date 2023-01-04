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
using System.Net;

public class menu : MonoBehaviour
{
    //variabili statiche 
    public static TcpClient tcpClient { get; set; }
    public static string ip { get; set; }
    public static string port { get; set; }
    public static NetworkStream networkStream { get; set; }
    public static StreamReader reader { get; set; }
    public static StreamWriter writer { get; set; }
    public static IPEndPoint ipEndPoint { get; set; }

    //variabili locali alla scena
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
        ip = inputFieldIp.GetComponent<TMP_Text>().text;
        port = inputFieldPort.GetComponent<TMP_Text>().text;
        long _ip;
        long.TryParse(ip, out _ip);
        int _port;
        int.TryParse(port, out _port);
        IPEndPoint ipEndPoint = new IPEndPoint(_ip, _port);


        menu.tcpClient = new TcpClient(ipEndPoint);

        try
        {
            menu.networkStream = tcpClient.GetStream();
            menu.reader = new StreamReader(networkStream, Encoding.UTF8);
            menu.writer = new StreamWriter(networkStream, Encoding.UTF8);

            //il tcp client si connette con ip e porta al server
            //invia il suo ip e la sua porta per farsi riconoscere 
            tcpClient.Connect(ipEndPoint);
            string messagge = "host--> " + ip + ":" + port + "-->" + "avaible";
            byte[] buffer = Encoding.UTF8.GetBytes(messagge);
            networkStream.Write(buffer, 0, buffer.Length);



            buffer = new Byte[256];

            String responseData = String.Empty;

            
            Int32 bytes = networkStream.Read(buffer, 0, buffer.Length);
            responseData = System.Text.Encoding.UTF8.GetString(buffer, 0, bytes);

            if (responseData != null && responseData == "host-->" + ip + ":" + port + "-->confirmed")
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


    public async void AsyncTryConnect()//metodo try connnect asyncrono
    {
        ip = inputFieldIp.GetComponent<TMP_Text>().text;
        port = inputFieldPort.GetComponent<TMP_Text>().text;
        long _ip;
        long.TryParse(ip, out _ip);
        int _port;
        int.TryParse(port, out _port);
        IPEndPoint ipEndPoint = new IPEndPoint(_ip, _port);


        Socket client = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        
        
        await client.ConnectAsync(ipEndPoint);

        //invio messaggio di host avaible
        string messagge = "host--> " + ip + ":" + port + "-->" + "avaible";
        byte[] byteMessagge = Encoding.UTF8.GetBytes(messagge);
        await client.SendAsync(byteMessagge, SocketFlags.None);

        //ricevo ack
        byte[] buffer = new byte[1024];
        string response = Encoding.UTF8.GetString(buffer, 0,await client.ReceiveAsync(buffer, SocketFlags.None));

        if(response== "host-->"+ip+":"+port+"-->confirmed")
        {
            connection_switch_color.GetComponent<Image>().color = new Color(0, 255, 0, 255);
            IsConnected = true;
        }                                                 

    





    }



    public void play()
    {
        //if (connection_switch_color.GetComponent<Image>().color == new Color(0,255,0,255) && IsConnected==true)//controlla che il segnaletto sia verde quindi che ci sia la connessione 
        //{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       // }
    }

    public void exit()
    {
        Application.Quit();
    }

}

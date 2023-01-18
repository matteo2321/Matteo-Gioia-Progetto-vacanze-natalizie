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
    public int PORT { get; set; } = 8080;
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
        try
        {
            ip = inputFieldIp.GetComponent<TMP_Text>().text;
            int _port;
            int.TryParse(port, out _port);
            Debug.Log(ip);
            //Debug.Log(_port.ToString());
            //Debug.Log(port);

            tcpClient = new TcpClient("192.168.1.85", PORT );
            //questa è la richiesta 
            string messagge = "5;192.168.1.85;8080;request;\r\n";
            Byte[] buffer = Encoding.UTF8.GetBytes(messagge);



            networkStream = tcpClient.GetStream();
            networkStream.Write(buffer, 0, buffer.Length);

            Debug.Log("CLIENT SENT: " + messagge);


            buffer = new Byte[512];
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = networkStream.Read(buffer, 0, buffer.Length);
            responseData = System.Text.Encoding.UTF8.GetString(buffer, 0, bytes);
            Debug.Log("CLIENT RECEIVED: " + responseData);


            //5;192.168.1.85;8080;confirmed;

            if (!responseData.Equals("5;192.168.1.85;8080;confirmed;"))
            {
                connection_switch_color.GetComponent<Image>().color = new Color(0, 255, 0, 255);
                IsConnected = true;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            throw;
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
        else
        {
            connection_switch_color.GetComponent<Image>().color = new Color(255, 0, 0, 255);
            IsConnected = false;
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

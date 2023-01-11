//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Net.Sockets;
//using System.Net;
//using UnityEngine;
//using TMPro;
//using UnityEngine.UI;
//using Unity.VisualScripting;
//
//public class Logic : MonoBehaviour
//{
//    public GameObject puck;
//    public GameObject PlayerLocal;
//    public GameObject PlayerOpponent;
//    private TcpClient tcpClient { get; set; }
//    private string ip { get; set; }
//    private string port { get; set; }
//    private NetworkStream networkStream { get; set; }
//    private StreamReader reader { get; set; }
//    private StreamWriter writer { get; set; }
//    private IPEndPoint ipEndPoint { get; set; }
//
//
//    void Start()
//    {
//        //appena inizia il gioco vado a portare gli oggetti statici utili alla connessione in questo contesto
//        this.tcpClient = menu.tcpClient;
//        this.ip = menu.ip;
//        this.port = menu.port;
//        this.networkStream = menu.networkStream;
//        this.reader = menu.reader;
//        this.writer = menu.writer;
//        this.ipEndPoint = menu.ipEndPoint;
//
//
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//        var Receive = new Thread(Receive);
//        var Send = new Thread(Send);
//        Receive.Start();        
//        Send.Start();
//    }
//    //metodo che riceve coordinatwe server e le settaq
//    //metodo che invia coordinate a server
//    private void Receive()
//    {
//        buffer = new Byte[256];
//        String responseData = String.Empty;
//        Int32 bytes = networkStream.Read(buffer, 0, buffer.Length);
//        responseData = System.Text.Encoding.UTF8.GetString(buffer, 0, bytes);
//
//        //setto coordinate 
//
//
//    }
//    private void Send()
//    {
//        int x=PlayerLocal.GetComponent<Trasform>().position.x;
//        int y=PlayerLocal.GetComponent<Trasform>().position.y;
//        string messagge = "0;"+x.toString()+";"+y.toString()+";";
//        byte[] buffer = Encoding.UTF8.GetBytes(messagge);
//        networkStream.Write(buffer, 0, buffer.Length);
//    }}
//
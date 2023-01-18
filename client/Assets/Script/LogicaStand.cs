using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Threading;
using System;
using System.Text;

public class LogicaStand : MonoBehaviour
{
    public GameObject puck;
    public GameObject PlayerLocal;
    public GameObject PlayerOpponent;
    private TcpClient tcpClient { get; set; }
    private string ip { get; set; }
    private string port { get; set; }
    private NetworkStream networkStream { get; set; }
    private StreamReader reader { get; set; }
    private StreamWriter writer { get; set; }
    private IPEndPoint ipEndPoint { get; set; }
    public static bool coll { get; set; }
    public string[] campi;



    void Start()
    {
        //appena inizia il gioco vado a portare gli oggetti statici utili alla connessione in questo contesto
        this.tcpClient = menu.tcpClient;
        this.ip = menu.ip;
        this.port = menu.port;
        this.networkStream = menu.networkStream;
        this.reader = menu.reader;
        this.writer = menu.writer;
        this.ipEndPoint = menu.ipEndPoint;
    }

    // Update is called once per frame
    void Update()
    {
        var SenStandard = new Thread(SendStandard);
        var RecStandard = new Thread(ReceiveStandard);
        var SenCollision = new Thread(SendCollision);
        Byte[] buffer = new Byte[256];
        String responseData = String.Empty;
        Int32 bytes = networkStream.Read(buffer, 0, buffer.Length);
        responseData = System.Text.Encoding.UTF8.GetString(buffer, 0, bytes);
        campi = responseData.Split(";");
        SenStandard.Start();
        switch (campi[0])
        {
            case "0":
                SenCollision.Start();
                float XPlayer = 0, YPlayer = 0;
                float.TryParse(campi[1], out XPlayer);
                float.TryParse(campi[2], out YPlayer);
                PlayerOpponent.GetComponent<Transform>().position = new Vector2(-XPlayer, -YPlayer);
                break;
            case "1":
                SenStandard.Start();
                XPlayer = 0;
                YPlayer = 0;
                float XPuck = 0;
                float YPuck = 0;
                
                float.TryParse(campi[1], out XPlayer);
                float.TryParse(campi[2], out YPlayer);
                float.TryParse(campi[3], out XPuck);
                float.TryParse(campi[4], out YPuck);

                PlayerOpponent.GetComponent<Transform>().position = new Vector2(-XPlayer, -YPlayer);
                puck.GetComponent<Transform>().position = new Vector2(XPuck, YPuck);
                break;
            case "6":
                coll = true;
                break;
              
        }





    }
    //metodo che riceve coordinatwe server e le settaq
    //metodo che invia coordinate a server
    private void ReceiveStandard()
    {
        coll = LogicaColl.coll;

        if (coll == false)
        {
            //non avviene nessuna collisione, la partita per questo player continua in maniera standard
            //ricevo le coordinate del player opposto e della puck
            Byte[] buffer = new Byte[256];
            String responseData = String.Empty;
            Int32 bytes = networkStream.Read(buffer, 0, buffer.Length);
            responseData = System.Text.Encoding.UTF8.GetString(buffer, 0, bytes);
            string[] campi = responseData.Split(";");
            if (campi[0] == "1")
            {
                float XPlayer, YPlayer, XPuck, YPuck;
                float.TryParse(campi[1], out XPlayer);
                float.TryParse(campi[2], out YPlayer);
                float.TryParse(campi[3], out XPuck);
                float.TryParse(campi[4], out YPuck);

                PlayerOpponent.GetComponent<Transform>().position = new Vector2(-XPlayer, -YPlayer);
                puck.GetComponent<Transform>().position = new Vector2(XPuck, YPuck);
            }
        }
        else if (coll == true)
        {
            //avviene una collisione tra player locale e puck
            //ricevo solamente le coordinate del player opposto 
            Byte[] buffer = new Byte[256];
            String responseData = String.Empty;
            Int32 bytes = networkStream.Read(buffer, 0, buffer.Length);
            responseData = System.Text.Encoding.UTF8.GetString(buffer, 0, bytes);
            string[] campi = responseData.Split(";");
            if (campi[0] == "1")//flag 
            {
                float XPlayer, YPlayer, XPuck, YPuck;
                float.TryParse(campi[1], out XPlayer);
                float.TryParse(campi[2], out YPlayer);
                float.TryParse(campi[3], out XPuck);
                float.TryParse(campi[4], out YPuck);

                PlayerOpponent.GetComponent<Transform>().position = new Vector2(-XPlayer, -YPlayer);
                puck.GetComponent<Transform>().position = new Vector2(XPuck, YPuck);
            }


        }
    }
    private void SendStandard()
    {

        float _x = PlayerLocal.GetComponent<Transform>().position.x;
        float _y = (int)PlayerLocal.GetComponent<Transform>().position.y;
        string x = _x.ToString();
        string y = _y.ToString();
        string s = "0;" + x + ";" + y + ";";
        byte[] buffer = Encoding.UTF8.GetBytes(s);
        networkStream.Write(buffer, 0, buffer.Length);
    }
    private void SendCollision()
    {
        float _x = PlayerLocal.GetComponent<Transform>().position.x;
        float _y = (int)PlayerLocal.GetComponent<Transform>().position.y;
        float _xPuck = puck.GetComponent<Transform>().position.x;
        float _yPuck = (int)puck.GetComponent<Transform>().position.y;

        string x = _x.ToString();
        string y = _y.ToString();
        string xPuck = _xPuck.ToString();
        string yPuck = _yPuck.ToString();
        string s = "1;" + x + ";" + y + ";" + xPuck + ";" + yPuck + ";";
        byte[] buffer = Encoding.UTF8.GetBytes(s);
        networkStream.Write(buffer, 0, buffer.Length);


    }

}

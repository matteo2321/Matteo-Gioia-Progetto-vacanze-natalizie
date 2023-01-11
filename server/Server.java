package server;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;

public class Server  extends Thread{
    
    private int PORT;
    private ServerSocket s;
    public Server(int PORT) throws IOException
    {
        this.PORT=PORT;
        s = new ServerSocket(PORT);
    }
    @Override
    public void run() {
        shared inst=shared.getInstance(); // classe per gestire la lista di client (socket)
        while(true) //TODO: gestire la chiusura del server
        {
            System.out.println("Server in ascolto");
            try {
                Socket socket = s.accept();
                System.out.println("Client");
                MySocket ms= new MySocket(socket, inst.getLengthSocketList());
                if( inst.addSocket(ms) )        //solo se le ho aggiunte ( ovvero c'era posto ) 
                {
                    clientThread ct = new clientThread(ms); //classe che gestisce la comunicazione con il client
                    ct.start();
                }

            } catch (IOException e) {
                //errore ... fa niente, ritorno ad aspettare
            }
        }
    
    }

}

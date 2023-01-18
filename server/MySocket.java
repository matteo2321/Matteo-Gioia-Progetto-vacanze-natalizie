package server;
import java.io.*;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.Socket;

public class MySocket {
    int id;
    Socket socket;
    private PrintWriter out;
    private BufferedReader in;

    public MySocket(Socket socket, int id) throws IOException {
        this.id = id;
        this.socket = socket;
        out = new PrintWriter(new BufferedWriter(new OutputStreamWriter(socket.getOutputStream())), true);
        in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
    }

    @Override
    public boolean equals(Object obj) {
        if (obj == null)
            return false;

        if (!(obj instanceof MySocket))
            return false;

        MySocket tmp = (MySocket) obj;
        return tmp.id == id;
    }

    public void sendMessage(String message) {
        out.println(message);
    }

    public String getip() {
        InetAddress address = socket.getInetAddress();
        String hostIP = address.getHostAddress() ;
        return hostIP;
      
    }

    public String getport() {

        return  Integer.toString(socket.getPort());

    }

    public BufferedReader getIn(){
        return in;
    }
}

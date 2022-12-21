package server;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.Socket;

public class MySocket {
    int id;
    Socket socket;
    private PrintWriter out;
    
    public MySocket(Socket socket, int id) throws IOException
    {
        this. id=id;
        this.socket=socket;
        out = new PrintWriter(new BufferedWriter(new OutputStreamWriter(socket.getOutputStream())),true);
    }

    

    @Override
    public boolean equals(Object obj) {
        if (obj==null) return false;

        if(! (obj instanceof MySocket)) return false;

        MySocket tmp =(MySocket)obj;
        return tmp.id == id;
    }

    public void sendMessage(String message){
        out.println(message);
    }
}

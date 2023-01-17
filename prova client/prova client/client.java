import java.beans.Encoder;
import java.io.*;
import java.net.*;
import java.util.*;

class client {

    public static void main(String[] args) {
        try {
            Socket socket = new Socket(InetAddress.getByName("localhost"), 1234);
            DataOutputStream outToServer = new DataOutputStream(socket.getOutputStream());
            
            byte[] message = new byte[100];

            for(int i=0;i<100;i++){
                message[i]=(byte)i;
            }
            outToServer.writeInt(message.length);
            outToServer.write(message);
            
            DataInputStream inFromClient = new DataInputStream(socket.getInputStream());

            int length = inFromClient.readInt(); 

            byte[] messageFromServer = new byte[length];


        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}




//            TimerTask tt = new TimerTask() {
//                int i = 0;
//
//                @Override
//                public void run() {
//                    try {
//                        outToServer.writeChars("String.valueOf(i++)");
//                        System.out.print(inFromUser.readLine());
//                    } catch (Exception e) {
//
//                    }
//                }
//            };
//          Timer timer = new Timer();
//          timer.schedule(tt, 0, 25);
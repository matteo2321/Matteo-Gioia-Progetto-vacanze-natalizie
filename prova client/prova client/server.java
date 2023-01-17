import java.io.*;
import java.net.*;
import java.util.*;

public class server {
    public static void main(String[] args) {
        try {
            ServerSocket welcomeSocket = new ServerSocket(1234);
            Socket connectionSocket = welcomeSocket.accept();
            DataInputStream inFromClient =
                        new DataInputStream(connectionSocket.getInputStream()); 
            DataOutputStream outToClient =
                        new DataOutputStream(connectionSocket.getOutputStream());
            
            
            do {
                int length = inFromClient.readInt(); 
                System.out.println(length); // print array

                byte[]message = new byte[length];
                for(int i = 0; i < message.length; i++) {
                    message[i] = inFromClient.readByte();
                }


                System.out.println(Arrays.toString(message));

 
            } while (true);

        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}

//Thread thread = new Thread(() -> {
//    while (true) {
//        try {
//            String recive = String.valueOf(inFromClient.read());
//            System.out.println(recive);
//            outToClient.writeChars(recive);
//        } catch (IOException e) {
//            // TODO Auto-generated catch block
//            e.printStackTrace();
//        }
//    }
//});
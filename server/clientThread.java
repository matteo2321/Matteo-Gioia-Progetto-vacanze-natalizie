package server;
import java.io.*;
import java.nio.charset.*;

import javax.swing.plaf.TreeUI;

public class clientThread extends Thread {

    MySocket _socket = null;
    BufferedReader in;
    int r1=700,r2=2;

    public clientThread(MySocket socket) throws IOException {
        _socket = socket;
       in = socket.getIn();
      //  DataInputStream inFromClient = new DataInputStream(_socket.socket.getInputStream());
    }

    @Override
    public void run() {
       // Byte[] risposta = new Byte[50];
        String r="";
           
            shared inst = shared.getInstance();
            while (true) {
                try {
                    //Byte[] messagge=Byte[512];
                    //inFromClient.read(message, 0, message.length); 
                    String s = in.readLine();
                    System.out.println(s);

                    //QUI C'E' IL PROBLEMA 
                    //String s = new String(message, StandardCharsets.UTF_8);

                    String[] campo = s.split(";");
                    if(campo[0].equals("5"))                  {
                        r+="5;";
                        r+=campo[1]+";"+campo[2]+";confirmed;";
                        System.out.println(r);
                        //byte[] byteArrray = inputString.getBytes();
                       // risposta = r.getBytes();
                    }
                    if (campo[0].equals("0")) {
                        r+="0;";
                        for (int i = 1; i < 3; i++) {
                          //  risposta[i] = msg[i];
                          int foo = Integer.parseInt(campo[i]);
                          int temp = foo*-1;
                            r+=temp+";";
                            //risposta = r.getBytes();
                        }
                        
                    }
                    else if(campo[0].equals("1")){
                        r+= "1;";
                        //if(campo[5]<500/*volocità massima reale ipotetica per evitare cheating */){
                        for (int i = 1; i < 5; i++) {
                            int foo = Integer.parseInt(campo[i]);
                            int temp = foo*(-1);
                           // if(controllo(Integer.parseInt(campo[1]),Integer.parseInt(campo[3]),Integer.parseInt(campo[2]),Integer.parseInt(campo[4]),r1,r2/*,v */)==true)
                              r+=temp+";";
                          //  risposta = r.getBytes();

                        }//}
                        //else 
                          //  risposta[0] = "errore vel non reale";
                          //per il momento verifichiam vada senza check velocità poi accerteremo in un secondo momento che il giocatore non stia manomettendo il gioco.
                          //controllo velocità e magari controllo "collisione effettuata"
                    }
                    //5=conferma connessione
                    //0/1;xr;yr;xp;yp
                   // risposta 0= risp normale solo coordinate
                   //risosta 1= risp evento coordinate racchettina(x;y) + pallina(x;y)

                    /* 1 versione :scartata
                     * if (msg[0] == 1) {
                     * risposta[i] = msg[1];
                     * i++;
                     * risposta[i] = msg[2];
                     * }
                     * else if(msg[0] == 2)
                     * {
                     * risposta[i] = msg[1];
                     * i++;
                     * risposta[i] = msg[2];
                     * i++;
                     * risposta[i] = msg[1];
                     * i++;
                     * risposta[i] = msg[2];
                     * 
                     * 
                     * }
                     */
                    //String[] crisp = in.readLine().split(";");
                  //  if(campo[0].equals("5"))
                   
                    
                    System.out.println("OK");

                   // System.out.println(_socket.id + " ha ricevuto : " + str);
                   /*  MySocket destinatario = inst.findDifferentSocketById(_socket.id); // ritorna la socket dell'altro player
                    if (destinatario != null/*&& velocita<100*/
                       // destinatario.sendMessage(r); // richiamo il metodo di MySocket che scrive sul buffer
                 
                    if(campo[0].equals("5"))
                    _socket.sendMessage(r);
                    else{
                   // System.out.println(_socket.id + " ha ricevuto : " + str);
                    MySocket destinatario = inst.findDifferentSocketById(_socket.id); // ritorna la socket dell'altro player
                    if (destinatario != null/*&& velocita<100*/)
                        destinatario.sendMessage(r); // richiamo il metodo di MySocket che scrive sul buffer
                   
                }} catch (IOException e) {
                    // connessione chiusa
                    break; // e vado a rimuovere la socket
                }
            }

           // inst.removeSocket(_socket);
       

    }
static public  boolean   controllo(int xc1,int xc2,int yc1,int yc2,int r1,int r2,int v){
boolean temp=false;
if(Math.sqrt(Math.pow((xc2-xc1), 2)+(Math.pow((yc1+yc2),2)))<=r1+r2/*||v<200 */)//collisione
temp=true;

return temp;
    
}
}



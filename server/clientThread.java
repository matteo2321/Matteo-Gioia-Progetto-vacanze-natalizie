package server;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class clientThread extends Thread {

    MySocket _socket = null;
    BufferedReader in;

    public clientThread(MySocket socket) throws IOException {
        _socket = socket;
        in = new BufferedReader(new InputStreamReader(_socket.socket.getInputStream()));

    }

    @Override
    public void run() {
        risposta = new Byte[n];
        int i = 0;

        shared inst = shared.getInstance();
        while (true) {
            try {

                String[] msg = in.readLine().split(";");
                if(campo[0]==1){

                }
                for (String campo : msg) {
                    if(campo){}
                    
                }
             /*    if (msg[0] == 1) {
                    risposta[i] = msg[1];
                    i++;
                    risposta[i] = msg[2];
                }
                else if(msg[0] == 2)
                {
                    risposta[i] = msg[1];
                    i++;
                    risposta[i] = msg[2];
                    i++;
                    risposta[i] = msg[1];
                    i++;
                    risposta[i] = msg[2];


                }*/

                System.out.println(_socket.id + " ha ricevuto : " + str);
                MySocket destinatario = inst.findDifferentSocketById(_socket.id); //ritorna la socket dell'altro player
                if (destinatario != null)
                    destinatario.sendMessage(str); //richiamo il metodo di MySocket che scrive sul buffer 

            } catch (IOException e) {
                // connessione chiusa
                break; // e vado a rimuovere la socket
            }
        }

        inst.removeSocket(_socket);

    }

}

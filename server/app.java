package server;

import java.io.IOException;

public class app {
    public static void main(String[] args) throws IOException {
        Server s = new Server(8080);
        s.start();
    }
}

import java.awt.event.KeyListener;
import java.io.IOException;
import java.io.Reader;
import java.security.Key;

import javafx.scene.input.KeyEvent;

public class consoleCapture implements KeyListener {

    public static void main(String[] args) {
        
    }

    public static void Start(){
        
        new consoleCapture();
    }

    public void keyPressed(KeyEvent e) {
        e.consume();
        
    }
    
}
//https://docs.oracle.com/javase/tutorial/uiswing/examples/events/index.html#KeyEventDemo
private class KeyHandler implements KeyListener { 

}
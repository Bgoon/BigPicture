using System.Collections.Generic;
using System.Runtime.InteropServices;

public enum KeyCode {
    None,
    Backspace,
    Delete,
    Tab,
    Clear,
    Return,
    Pause,
    Escape,
    Space,
    Keypad0,
    Keypad1,
    Keypad2,
    Keypad3,
    Keypad4,
    Keypad5,
    Keypad6,
    Keypad7,
    Keypad8,
    Keypad9,
    KeypadPeriod,
    KeypadDivide,
    KeypadMultiply,
    KeypadMinus,
    KeypadPlus,
    KeypadEnter,
    KeypadEquals,
    UpArrow,
    DownArrow,
    RightArrow,
    LeftArrow,
    Insert,
    Home,
    End,
    PageUp,
    PageDown,
    F1,
    F2,
    F3,
    F4,
    F5,
    F6,
    F7,
    F8,
    F9,
    F10,
    F11,
    F12,
    F13,
    F14,
    F15,
    Alpha0,
    Alpha1,
    Alpha2,
    Alpha3,
    Alpha4,
    Alpha5,
    Alpha6,
    Alpha7,
    Alpha8,
    Alpha9,
    Exclaim,
    DoubleQuote,
    Hash,
    Dollar,
    Ampersand,
    Quote,
    LeftParen,
    RightParen,
    Asterisk,
    Plus,
    Comma,
    Minus,
    Period,
    Slash,
    Colon,
    Semicolon,
    Less,
    Equals,
    Greater,
    Question,
    At,
    LeftBracket,
    Backslash,
    RightBracket,
    Caret,
    Underscore,
    BackQuote,
    A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,
    Numlock,
    CapsLock,
    ScrollLock,
    RightShift,
    LeftShift,
    RightControl,
    LeftControl,
    RightAlt,
    LeftAlt,
    LeftCommand,
    LeftApple,
    LeftWindows,
    RightCommand,
    RightApple,
    RightWindows,
    AltGr,
    Help,
    Print,
    SysReq,
    Break,
    Menu,
    LeftMouse,
    RightMouse,
    Mouse2,
    Mouse3,
    Mouse4,
    Mouse5,
    Mouse6,
    JoystickButton0,
    JoystickButton1,
    JoystickButton2,
    JoystickButton3,
    JoystickButton4,
    JoystickButton5,
    JoystickButton6,
    JoystickButton7,
    JoystickButton8,
    JoystickButton9,
    JoystickButton10,
    JoystickButton11,
    JoystickButton12,
    JoystickButton13,
    JoystickButton14,
    JoystickButton15,
    JoystickButton16,
    JoystickButton17,
    JoystickButton18,
    JoystickButton19,
    Joystick1Button0,
    Joystick1Button1,
    Joystick1Button2,
    Joystick1Button3,
    Joystick1Button4,
    Joystick1Button5,
    Joystick1Button6,
    Joystick1Button7,
    Joystick1Button8,
    Joystick1Button9,
    Joystick1Button10,
    Joystick1Button11,
    Joystick1Button12,
    Joystick1Button13,
    Joystick1Button14,
    Joystick1Button15,
    Joystick1Button16,
    Joystick1Button17,
    Joystick1Button18,
    Joystick1Button19,
    Joystick2Button0,
    Joystick2Button1,
    Joystick2Button2,
    Joystick2Button3,
    Joystick2Button4,
    Joystick2Button5,
    Joystick2Button6,
    Joystick2Button7,
    Joystick2Button8,
    Joystick2Button9,
    Joystick2Button10,
    Joystick2Button11,
    Joystick2Button12,
    Joystick2Button13,
    Joystick2Button14,
    Joystick2Button15,
    Joystick2Button16,
    Joystick2Button17,
    Joystick2Button18,
    Joystick2Button19

}
public class WinInput {
    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
    protected static extern short GetAsyncKeyState(int keyCode);

	const byte textKeyDelay = 20;
	static byte textKeyTimer;
	static KeyCode textKeyBuffer;
	static Queue<KeyCode> keyDownQueue = new Queue<KeyCode>();
	static List<KeyCode> keyAutoList = new List<KeyCode>();
	static Queue<KeyCode> keyUpQueue = new Queue<KeyCode>();

	public static void Update() {
		for(int i=0; i < keyUpQueue.Count; i++) {
			keyUpQueue.Dequeue();
		}
		for(int i=0; i < keyDownQueue.Count; i++) {
			keyDownQueue.Dequeue();
		}
		for(int i=keyAutoList.Count-1; i >= 0; i--) {
			if (GetAsyncKeyState(KeyCodeToVkey(keyAutoList[i])) == 0) {
				keyUpQueue.Enqueue(keyAutoList[i]);
				keyAutoList.RemoveAt(i);
			}
		}
	}
    private static int KeyCodeToVkey(KeyCode key) {
        int VK = 0;
        switch (key) {
            case KeyCode.A:
            case KeyCode.B:
            case KeyCode.C:
            case KeyCode.D:
            case KeyCode.E:
            case KeyCode.F:
            case KeyCode.G:
            case KeyCode.H:
            case KeyCode.I:
            case KeyCode.J:
            case KeyCode.K:
            case KeyCode.L:
            case KeyCode.M:
            case KeyCode.N:
            case KeyCode.O:
            case KeyCode.P:
            case KeyCode.Q:
            case KeyCode.R:
            case KeyCode.S:
            case KeyCode.T:
            case KeyCode.U:
            case KeyCode.V:
            case KeyCode.W:
            case KeyCode.X:
            case KeyCode.Y:
            case KeyCode.Z:
                VK = 0x41 + ((int)key - (int)KeyCode.A);
                break;
            case KeyCode.Backspace:
                VK = 0x08;
                break;
            case KeyCode.Tab:
                VK = 0x09;
                break;
            case KeyCode.Clear:
                VK = 0x0C;
                break;
            case KeyCode.Return:
                VK = 0x0D;
                break;
            case KeyCode.Pause:
                VK = 0x13;
                break;
            case KeyCode.Escape:
                VK = 0x1B;
                break;
            case KeyCode.Space:
                VK = 0x20;
                break;
            case KeyCode.Exclaim:
                VK = 0x31;
                break;
            case KeyCode.DoubleQuote:
                VK = 0xDE;
                break;
            case KeyCode.Hash:
                VK = 0x33;
                break;
            case KeyCode.Dollar:
                VK = 0x34;
                break;
            case KeyCode.Ampersand:
                VK = 0x37;
                break;
            case KeyCode.Quote:
                VK = 0xDE;
                break;
            case KeyCode.LeftParen:
                VK = 0x39;
                break;
            case KeyCode.RightParen:
                VK = 0x30;
                break;
            case KeyCode.Asterisk:
                VK = 0x13;
                break;
            case KeyCode.Equals:
            case KeyCode.Plus:
                VK = 0xBB;
                break;
            case KeyCode.Less:
            case KeyCode.Comma:
                VK = 0xBC;
                break;
            case KeyCode.Underscore:
            case KeyCode.Minus:
                VK = 0xBD;
                break;
            case KeyCode.Greater:
            case KeyCode.Period:
                VK = 0xBE;
                break;
            case KeyCode.Question:
            case KeyCode.Slash:
                VK = 0xBF;
                break;

            case KeyCode.Alpha0:
            case KeyCode.Alpha1:
            case KeyCode.Alpha2:
            case KeyCode.Alpha3:
            case KeyCode.Alpha4:
            case KeyCode.Alpha5:
            case KeyCode.Alpha6:
            case KeyCode.Alpha7:
            case KeyCode.Alpha8:
            case KeyCode.Alpha9:
                VK = 0x30 + ((int)key - (int)KeyCode.Alpha0);
                break;
            case KeyCode.Colon:
            case KeyCode.Semicolon:
                VK = 0xBA;
                break;

            case KeyCode.At:
                VK = 0x32;
                break;
            case KeyCode.LeftBracket:
                VK = 0xDB;
                break;
            case KeyCode.Backslash:
                VK = 0xDC;
                break;
            case KeyCode.RightBracket:
                VK = 0xDD;
                break;
            case KeyCode.Caret:
                VK = 0x36;
                break;
            case KeyCode.BackQuote:
                VK = 0xC0;
                break;

            case KeyCode.Delete:
                VK = 0x2E;
                break;

			case KeyCode.LeftMouse:
			case KeyCode.RightMouse:
				VK = 0x01 + ((int)key - (int)KeyCode.LeftMouse);
				break;
            case KeyCode.Keypad0:
            case KeyCode.Keypad1:
            case KeyCode.Keypad2:
            case KeyCode.Keypad3:
            case KeyCode.Keypad4:
            case KeyCode.Keypad5:
            case KeyCode.Keypad6:
            case KeyCode.Keypad7:
            case KeyCode.Keypad8:
            case KeyCode.Keypad9:
                VK = 0x60 + ((int)key - (int)KeyCode.Keypad0);
                break;

            case KeyCode.KeypadPeriod:
                VK = 0x6E;
                break;
            case KeyCode.KeypadDivide:
                VK = 0x6F;
                break;
            case KeyCode.KeypadMultiply:
                VK = 0x6A;
                break;
            case KeyCode.KeypadMinus:
                VK = 0x6D;
                break;
            case KeyCode.KeypadPlus:
                VK = 0x6B;
                break;
            case KeyCode.KeypadEnter:
                VK = 0x6C;
                break;
            case KeyCode.UpArrow:
                VK = 0x26;
                break;
            case KeyCode.DownArrow:
                VK = 0x28;
                break;
            case KeyCode.RightArrow:
                VK = 0x27;
                break;
            case KeyCode.LeftArrow:
                VK = 0x25;
                break;
            case KeyCode.Insert:
                VK = 0x2D;
                break;
            case KeyCode.Home:
                VK = 0x24;
                break;
            case KeyCode.End:
                VK = 0x23;
                break;
            case KeyCode.PageUp:
                VK = 0x21;
                break;
            case KeyCode.PageDown:
                VK = 0x22;
                break;

            case KeyCode.F1:
            case KeyCode.F2:
            case KeyCode.F3:
            case KeyCode.F4:
            case KeyCode.F5:
            case KeyCode.F6:
            case KeyCode.F7:
            case KeyCode.F8:
            case KeyCode.F9:
            case KeyCode.F10:
            case KeyCode.F11:
            case KeyCode.F12:
            case KeyCode.F13:
            case KeyCode.F14:
            case KeyCode.F15:
                VK = 0x70 + ((int)key - (int)KeyCode.F1);
                break;

            case KeyCode.Numlock:
                VK = 0x90;
                break;
            case KeyCode.CapsLock:
                VK = 0x14;
                break;
            case KeyCode.ScrollLock:
                VK = 0x91;
                break;
            case KeyCode.RightShift:
                VK = 0xA1;
                break;
            case KeyCode.LeftShift:
                VK = 0xA0;
                break;
            case KeyCode.RightControl:
                VK = 0xA3;
                break;
            case KeyCode.LeftControl:
                VK = 0xA2;
                break;
            case KeyCode.RightAlt:
                VK = 0xA5;
                break;
            case KeyCode.LeftAlt:
                VK = 0xA4;
                break;
            case KeyCode.Help:
                VK = 0xE3;
                break;
            case KeyCode.Print:
                VK = 0x2A;
                break;
            case KeyCode.SysReq:
                VK = 0x2C;
                break;
            case KeyCode.Break:
                VK = 0x03;
                break;
        }
        return VK;
    }
    public static bool GetKey(KeyCode key) {
		if(keyAutoList.Contains(key)) {
			return true;
		}
        if (GetAsyncKeyState(KeyCodeToVkey(key)) != 0) {
			keyAutoList.Add(key);
			keyDownQueue.Enqueue(key);
			return true;
        } else {
            return false;
        }
    }
	public static bool GetKeyDown(KeyCode key) {
		if(keyDownQueue.Contains(key)) {
			return true;
		}
		if (keyAutoList.Contains(key) == false) {
			if (GetAsyncKeyState(KeyCodeToVkey(key)) != 0) {
				keyAutoList.Add(key);
				keyDownQueue.Enqueue(key);
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}
	public static bool GetKeyText(KeyCode key) {
		if (GetKeyDown(key)) {
			textKeyBuffer = key;
			textKeyTimer = textKeyDelay;
			return true;
		} else {
			if (key == textKeyBuffer && GetKey(key)) {
				if (textKeyTimer == 1) {
					textKeyTimer = 2;
					return true;
				} else {
					textKeyTimer--;
					return false;
				}
			} else {
				return false;
			}
		}
	}
	public static bool GetKeyUp(KeyCode key) {
		if(keyUpQueue.Contains(key)) {
			return true;
		} else {
			return false;
		}
	}

    public static bool GetKeyVK(int VKey) {
        return (GetAsyncKeyState(VKey) != 0);
    }
}
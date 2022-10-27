
using Iot.Device.Tm1637;

using Tm1637 tm1637 = new(20, 21);
tm1637.Brightness = 7;
tm1637.ScreenOn = true;

Dictionary<string, (byte, Character)> ScoreBoardDigits = new()
{
    { "H1", ((byte)0, (Character)0b0100_0000) },
    { "H2", ((byte)0, (Character)0b1000_0000) },
    { "H3", ((byte)1, (Character)0b1000_0000) },
    { "H4", ((byte)2, (Character)0b1000_0000) },
    { "H5", ((byte)2, (Character)0b0100_0000) },
    { "H6", ((byte)2, (Character)0b0010_0000) },
    { "H7", ((byte)2, (Character)0b0001_0000) },
    { "H8", ((byte)2, (Character)0b0000_1000) },
    { "H9", ((byte)2, (Character)0b0000_0100) },
    { "H10", ((byte)2, (Character)0b0000_0010) },
    { "H11", ((byte)2, (Character)0b0000_0001) },
    { "V1", ((byte)5, (Character)0b0100_0000) },
    { "V2", ((byte)5, (Character)0b1000_0000) },
    { "V3", ((byte)4, (Character)0b1000_0000) },
    { "V4", ((byte)3, (Character)0b1000_0000) },
    { "V5", ((byte)3, (Character)0b0100_0000) },
    { "V6", ((byte)3, (Character)0b0010_0000) },
    { "V7", ((byte)3, (Character)0b0001_0000) },
    { "V8", ((byte)3, (Character)0b0000_1000) },
    { "V9", ((byte)3, (Character)0b0000_0100) },
    { "V10", ((byte)3, (Character)0b0000_0010) },
    { "V11", ((byte)3, (Character)0b0000_0001) }
};

Character[] AllLeds = new Character[6] {
    (Character)0b1111_1111,
    (Character)0b1111_1111,
    (Character)0b1111_1111,
    (Character)0b1111_1111,
    (Character)0b1111_1111,
    (Character)0b1111_1111
};

Character[] NoLeds = new Character[6] {
    (Character)0b0000_0000,
    (Character)0b0000_0000,
    (Character)0b0000_0000,
    (Character)0b0000_0000,
    (Character)0b0000_0000,
    (Character)0b0000_0000
};

Character[] HomeLeds = new Character[6] {
    (Character)0b1100_0000,
    (Character)0b1000_0000,
    (Character)0b1111_1111,
    (Character)0b0000_0000,
    (Character)0b0000_0000,
    (Character)0b0000_0000
};

Character[] VisitorLeds = new Character[6] {
    (Character)0b0000_0000,
    (Character)0b0000_0000,
    (Character)0b0000_0000,
    (Character)0b1111_1111,
    (Character)0b1000_0000,
    (Character)0b1100_0000
};

while (true)
{
    //Knight Rider
    for (int i = 11; i >= 1; i--)
    {
        tm1637.ClearDisplay();
        tm1637.Display(ScoreBoardDigits[$"H{i}"].Item1, ScoreBoardDigits[$"H{i}"].Item2);
        Thread.Sleep(100);
    }
    for (int i = 1; i <= 11; i++)
    {
        tm1637.ClearDisplay();
        tm1637.Display(ScoreBoardDigits[$"V{i}"].Item1, ScoreBoardDigits[$"V{i}"].Item2);
        Thread.Sleep(100);
    }
    for (int i = 11; i >= 1; i--)
    {
        tm1637.ClearDisplay();
        tm1637.Display(ScoreBoardDigits[$"V{i}"].Item1, ScoreBoardDigits[$"V{i}"].Item2);
        Thread.Sleep(100);
    }
    for (int i = 1; i <= 11; i++)
    {
        tm1637.ClearDisplay();
        tm1637.Display(ScoreBoardDigits[$"H{i}"].Item1, ScoreBoardDigits[$"H{i}"].Item2);
        Thread.Sleep(100);
    }

    //Knight rider, but centered
    for (int i = 1; i <= 11; i++)
    {
        tm1637.ClearDisplay();
        tm1637.Display(ScoreBoardDigits[$"H{i}"].Item1, ScoreBoardDigits[$"H{i}"].Item2);
        tm1637.Display(ScoreBoardDigits[$"V{i}"].Item1, ScoreBoardDigits[$"V{i}"].Item2);
        Thread.Sleep(100);
    }
    for (int i = 11; i >= 1; i--)
    {
        tm1637.ClearDisplay();
        tm1637.Display(ScoreBoardDigits[$"H{i}"].Item1, ScoreBoardDigits[$"H{i}"].Item2);
        tm1637.Display(ScoreBoardDigits[$"V{i}"].Item1, ScoreBoardDigits[$"V{i}"].Item2);
        Thread.Sleep(100);
    }

    tm1637.ResetDisplay();


    // Flash home and visitor
    for (int i=0;i<4;i++)
    {
        tm1637.ClearDisplay();
        tm1637.Display(HomeLeds);
        Thread.Sleep(250);
        tm1637.ClearDisplay();
        tm1637.Display(VisitorLeds);
        Thread.Sleep(250);
    }

    // All on, flash brightness
    tm1637.Display(AllLeds);
    for (int i = 0; i < 5; i++)
    {
        tm1637.Brightness = 0;
        Thread.Sleep(500);
        tm1637.Brightness = 7;
        Thread.Sleep(500);
    }

    tm1637.ClearDisplay();
}

static public class Extensions
{
    // No idea why I need this, I'm hypothesizing that the Knight Rider
    // code is somehow leaving the chip in a bad state. This is a workaround.
    static public void ResetDisplay(this Tm1637 tm1637)
    {
        tm1637.ScreenOn = false;
        Thread.Sleep(100);
        tm1637.ScreenOn = true;
    }
}
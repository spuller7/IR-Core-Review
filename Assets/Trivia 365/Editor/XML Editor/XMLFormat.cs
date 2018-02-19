using UnityEngine;

public class XMLFormat
{
    public static XMLSheetWindow Window
    {
        get
        {
            return XMLSheetWindow.WindowInstance;
        }
    }
    public Rect position
    {
        get
        {
            return Window.position;
        }
    }

    /// <summary>
    /// Draw the GUI content
    /// </summary>
    public virtual void OnGUI() { }
    /// <summary>
    /// Draw the header with the names
    /// </summary>
    public virtual void OnDrawHeader() { }
    /// <summary>
    /// Draw the columns with the data
    /// </summary>
    public virtual void OnDrawColumns() { }
    /// <summary>
    /// Draw the bottom bar with the buttons
    /// </summary>
    public virtual void OnDrawBottomBar() { }
    /// <summary>
    /// Reset all info
    /// </summary>
    public virtual void ResetInfo() { }

    public static string UTF8ByteArrayToString(byte[] characters)
    {
        return System.Text.Encoding.UTF8.GetString(characters, 0, characters.Length);
    }
    public static byte[] StringToUTF8ByteArray(string text)
    {
        return System.Text.Encoding.UTF8.GetBytes(text);
    }
}
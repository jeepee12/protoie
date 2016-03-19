static public class CurrentMode
{
    static private bool m_EndlessMode = false;

    static public bool isEndlessMode()
    {
        return m_EndlessMode;
    }

    static public void setEndlessMode(bool value=true)
    {
        m_EndlessMode = value;
    }
}

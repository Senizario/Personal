/* 
 * Andres Dario Serna Isaza - 28/05/20
 * V - 1.0
*/

using System.Collections.Generic;

public class CustomEvent<T>
{
    #region Delegate
    public delegate void ConcatenedAction(Dictionary<string, T> data = null, System.Action outputMethod = null);
    #endregion

    #region Information
    ConcatenedAction inputMethod;
    public ConcatenedAction PinputMethod
    {
        get { return inputMethod; }
    }
    Dictionary<string, T> data;
    public Dictionary<string, T> Pdata
    {
        get { return data; }
    }
    System.Action outputMethod;
    #endregion

    public CustomEvent(ConcatenedAction inputMethod, Dictionary<string, T> data = null, System.Action outputMethod = null)
    {
        this.inputMethod = inputMethod;

        if (data != null)
            this.data = data;

        if (outputMethod != null)
            this.outputMethod = outputMethod;
    }

    public void Execute()
    {
        inputMethod(data: data ?? null, outputMethod: outputMethod ?? null);
    }
}
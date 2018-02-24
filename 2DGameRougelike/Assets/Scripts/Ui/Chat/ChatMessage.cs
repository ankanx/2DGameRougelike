using System.Collections;
using System.Collections.Generic;


public class ChatMessage {

    public string SenderIp;
    public string SenderName;
    public string Message;
    public bool IsBroadCast;

    public ChatMessage(string _SenderIp, string _SenderName, string _Message,bool _IsBroadCast)
    {
        this.SenderIp = _SenderIp;
        this.SenderName = _SenderName;
        this.Message = _Message;
        this.IsBroadCast = _IsBroadCast;
    }


}

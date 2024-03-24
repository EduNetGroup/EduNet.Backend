namespace EduNet.Backend.Service.Exceptions;

public class EduNetException : Exception
{
    public int statusCode;

    public EduNetException(int Code, string Message) : base(Message)
    {
        statusCode = Code;
    }
}

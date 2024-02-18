namespace EduNet.Backend.Service.Exceptions;

partial class EduNetException : Exception
{
    public int statusCode;

    public EduNetException(int Code, string Message) : base(Message)
    {
        statusCode = Code;
    }
}

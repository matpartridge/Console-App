namespace WOPR.Infrastructure.Classes
{
    public class OperationResult
    {
        public bool Result { get; private set; }
        public string Message { get; private set; }    
        public OperationResult(bool result, string message)
        {
            Result = result;
            Message = message;
        }
    }
}

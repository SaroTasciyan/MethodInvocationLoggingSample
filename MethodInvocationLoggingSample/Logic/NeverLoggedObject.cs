namespace MethodInvocationLoggingSample.Logic
{
    public class NeverLoggedObject : INeverLoggedObject
    {
        public void NotLoggedMethod()
        {
        }
    }
}
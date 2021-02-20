namespace System
{
    /// <summary>
    /// 서비스 생성자 정의를 위한 특성
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
    public class ServiceConstructorAttribute : Attribute
    {
    }
}

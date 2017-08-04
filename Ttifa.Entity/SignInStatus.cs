/// <summary>
/// 登录状态
/// </summary>
public enum SignInStatus
{
    /// <summary>
    /// Sign in was successful
    /// </summary>
    Success = 0,
    /// <summary>
    /// User is locked out
    /// </summary>
    LockedOut = 1,
    /// <summary>
    /// Sign in requires addition verification (i.e. two factor)
    /// </summary>
    RequiresVerification = 2,
    /// <summary>
    /// Sign in failed
    /// </summary>
    Failure = 3
}

public enum LoginStatus
{
    StateError,//用户被冻结
    ValueNull,//用户名或密码为空
    ValueError//用户名或密码错误
}

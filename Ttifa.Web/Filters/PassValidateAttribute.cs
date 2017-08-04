﻿using System;

namespace Ttifa.Web.Filters
{
    /// <summary>
    /// 跳过接口参数验证
    /// 可作用于Controller或Action
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public sealed class PassValidateAttribute : Attribute
    {

    }
}
﻿#if LEGACY_NAMESPACE
using AutoFilterer.Enums;
#endif
using AutoFilterer.Extensions;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoFilterer.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class OperatorComparisonAttribute : FilteringOptionsBaseAttribute
{
    public OperatorComparisonAttribute(OperatorType operatorType)
    {
        this.OperatorType = operatorType;
    }

    public OperatorType OperatorType { get; }

    public override Expression BuildExpression(Expression expressionBody, PropertyInfo targetProperty, PropertyInfo filterProperty, object value)
    {
        var prop = Expression.Property(expressionBody, targetProperty.Name);
        var param = Expression.Constant(value);

        if (targetProperty.PropertyType.IsNullable())
            prop = Expression.Property(prop, nameof(Nullable<bool>.Value));

        switch (OperatorType)
        {
            case OperatorType.Equal:
                return Expression.Equal(prop, param);
            case OperatorType.NotEqual:
                return Expression.NotEqual(prop, param);
            case OperatorType.GreaterThan:
                return Expression.GreaterThan(prop, param);
            case OperatorType.GreaterThanOrEqual:
                return Expression.GreaterThanOrEqual(prop, param);
            case OperatorType.LessThan:
                return Expression.LessThan(prop, param);
            case OperatorType.LessThanOrEqual:
                return Expression.LessThanOrEqual(prop, param);
        }

        return Expression.Empty();
    }

    #region Static

    public static OperatorComparisonAttribute Equal { get; }
    public static OperatorComparisonAttribute NotEqual { get; }
    public static OperatorComparisonAttribute GreaterThan { get; }
    public static OperatorComparisonAttribute GreaterThanOrEqual { get; }
    public static OperatorComparisonAttribute LessThan { get; }
    public static OperatorComparisonAttribute LessThanOrEqual { get; }

    static OperatorComparisonAttribute()
    {
        Equal = new OperatorComparisonAttribute(OperatorType.Equal);
        NotEqual = new OperatorComparisonAttribute(OperatorType.NotEqual);
        GreaterThan = new OperatorComparisonAttribute(OperatorType.GreaterThan);
        GreaterThanOrEqual = new OperatorComparisonAttribute(OperatorType.GreaterThanOrEqual);
        LessThan = new OperatorComparisonAttribute(OperatorType.LessThan);
        LessThanOrEqual = new OperatorComparisonAttribute(OperatorType.LessThanOrEqual);
    }
    #endregion
}

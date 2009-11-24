using System.Linq.Expressions;
using System.Reflection;

namespace Balder.Silverlight.Extensions
{
	public static class ExpressionExtensions
	{
		public static MemberExpression GetMemberExpression(this Expression expression)
		{
			var lambda = expression as LambdaExpression;
			MemberExpression memberExpression;
			if (lambda.Body is UnaryExpression)
			{
				var unaryExpression = lambda.Body as UnaryExpression;
				memberExpression = unaryExpression.Operand as MemberExpression;
			}
			else
			{
				memberExpression = lambda.Body as MemberExpression;
			}
			return memberExpression;
		}

		public static FieldInfo GetFieldInfo(this Expression expression)
		{
			var memberExpression = GetMemberExpression(expression);
			var fieldInfo = memberExpression.Member as FieldInfo;
			return fieldInfo;
		}

		public static PropertyInfo GetPropertyInfo(this Expression expression)
		{
			var memberExpression = GetMemberExpression(expression);
			var propertyInfo = memberExpression.Member as PropertyInfo;
			return propertyInfo;
		}

		public static object GetInstance(this Expression expression)
		{
			var memberExpression = GetMemberExpression(expression);
			var constantExpression = memberExpression.Expression as ConstantExpression;
			if (null == constantExpression)
			{
				
				var innerMember = memberExpression.Expression as MemberExpression;
				constantExpression = innerMember.Expression as ConstantExpression;
				if( null != innerMember && innerMember.Member is PropertyInfo )
				{
					var value = ((PropertyInfo) innerMember.Member).GetValue(constantExpression.Value, null);
					return value;
				}
			}
			return constantExpression.Value;
			
		}
		public static T GetInstance<T>(this Expression expression)
		{
			return (T)GetInstance(expression);
		}
	}
}
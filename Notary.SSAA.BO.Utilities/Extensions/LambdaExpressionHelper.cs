using System.Linq.Expressions;
using System.Reflection;


namespace Notary.SSAA.BO.Utilities.Extensions
{
    public enum LinqExpressionOparator
    {
        Equal = 1,
        Between = 2,
        Contain = 3,
        In = 4,
        GreaterThan = 5,
        LessThan = 6,
        GreaterThanOrEqual = 7,
        LessThanOrEqual = 8,
        NotEqual = 9
    }
    public enum LinqExpressionConnector
    {
        And = 1,
        Or = 2
    }
    public class LinqExpressionInfo
    {
        public string PropertyName { get; set; }
        public LinqExpressionOparator Operator { get; set; }
        public object Value { get; set; }
        public object Value2 { get; set; }
        public LinqExpressionConnector Connector { get; set; }
        public LinqExpressionInfo()
        {

        }
        public LinqExpressionInfo(string propertyName, LinqExpressionOparator @operator, object value, LinqExpressionConnector connector, object value2 = null)
        {
            PropertyName = propertyName;
            Operator = @operator;
            Value = value;
            Connector = connector;
            Value2 = value2;
        }
       public bool IsMainCriteria { get; set; }
    }
    public class LambdaExpressionHelper
    {
        public static Expression<Func<T, bool>> CreateLinqLambdaExpression<T>(List<LinqExpressionInfo> list)
        {
            if (list == null || list.Count == 0) return null;
            var lst = list.Where(e => e.IsMainCriteria == true).ToList();

            list = list.OrderBy(l => l.Connector).ToList();
            
            var param = Expression.Parameter(typeof(T), "e");
            Expression exp1 = null;
            Expression exp2 = null;
            Expression exp = null;
            foreach (LinqExpressionInfo ei in list)
            {
                if (ei.Connector == LinqExpressionConnector.Or) continue;
                if (ei.Operator == LinqExpressionOparator.In)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    if (IsList(ei.PropertyName))
                    {
                        exp = ProcessListStatement(param, ei);
                    }
                    else exp = CreateInExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp2 == null)
                    {                        
                        exp2 = exp;
                    }
                    else
                    {
                        exp1 = exp2;
                        exp2 = exp;
                    }
                }
                if (ei.Operator == LinqExpressionOparator.Equal)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    if (IsList(ei.PropertyName))
                    {
                        exp = ProcessListStatement(param, ei);
                    }
                    else exp = CreateEqualExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp2 == null)
                    {                        
                        exp2 = exp;
                    }
                    else
                    {
                        exp1 = exp2;
                        exp2 = exp;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.Contain)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue; 
                    if (IsList(ei.PropertyName))
                    {
                        exp = ProcessListStatement(param, ei);
                    }
                    else exp = CreateContainsExpression<T>(param, ei.PropertyName, ei.Value.ToString());
                    if (exp2 == null)
                    {                        
                        exp2 = exp;
                    }
                    else
                    {
                        exp1 = exp2;
                        exp2 = exp;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.Between)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    if (ei.Value2 == null) continue;
                    if (IsList(ei.PropertyName))
                    {
                        exp = ProcessListStatement(param, ei);
                    }
                    else exp = CreateBetweenExpression<T>(param, ei.PropertyName, ei.Value, ei.Value2);
                    if (exp2 == null)
                    {                        
                        exp2 = exp;
                    }
                    else
                    {
                        exp1 = exp2;
                        exp2 = exp;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.GreaterThan)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    
                    if (IsList(ei.PropertyName))
                    {
                        exp = ProcessListStatement(param, ei);
                    }
                    else exp = CreateGreatThanExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp2 == null)
                    {                        
                        exp2 = exp;
                    }
                    else
                    {
                        exp1 = exp2;
                        exp2 = exp;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.GreaterThanOrEqual)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    
                    if (IsList(ei.PropertyName))
                    {
                        exp = ProcessListStatement(param, ei);
                    }
                    else exp = CreateGreatThanOrEqualExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp2 == null)
                    {                        
                        exp2 = exp;
                    }
                    else
                    {
                        exp1 = exp2;
                        exp2 = exp;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.LessThan)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    
                    if (IsList(ei.PropertyName))
                    {
                        exp = ProcessListStatement(param, ei);
                    }
                    else exp = CreateLessThanExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp2 == null)
                    {                        
                        exp2 = exp;
                    }
                    else
                    {
                        exp1 = exp2;
                        exp2 = exp;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.LessThanOrEqual)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    
                    if (IsList(ei.PropertyName))
                    {
                        exp = ProcessListStatement(param, ei);
                    }
                    else exp = CreateLessThanOrequalExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp2 == null)
                    {                        
                        exp2 = exp;
                    }
                    else
                    {
                        exp1 = exp2;
                        exp2 = exp;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.NotEqual)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    if (IsList(ei.PropertyName))
                    {
                        exp = ProcessListStatement(param, ei);
                    }
                    else exp = CreateNotEqualExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp2 == null)
                    {                        
                        exp2 = exp;
                    }
                    else
                    {
                        exp1 = exp2;
                        exp2 = exp;
                    }
                }
                if (ei.Connector == LinqExpressionConnector.Or)
                {
                    if(lst.Count > 0)
                    {
                        lst.Add(ei);
                        exp2 = GetExpression<T>(param, lst);
                    }
                    exp2 = exp1 == null ? exp2 : Expression.OrElse(exp1, exp2);
                }
                else if (ei.Connector == LinqExpressionConnector.And)
                    exp2 = exp1 == null ? exp2 : Expression.AndAlso(exp1, exp2);
            }

            var lstOr = list.Where(x => x.Connector == LinqExpressionConnector.Or).ToList();
            Expression exp11 = null;
            Expression exp22 = null;
            Expression exp0 = null;
            foreach (LinqExpressionInfo ei in lstOr)
            {
                if (ei.Operator == LinqExpressionOparator.In)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    if (IsList(ei.PropertyName))
                    {
                        exp0 = ProcessListStatement(param, ei);
                    }
                    else exp0 = CreateInExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp22 == null)
                    {                        
                        exp22 = exp0;
                    }
                    else
                    {
                        exp11 = exp22;
                        exp22 = exp0;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.Equal)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    if (IsList(ei.PropertyName))
                    {
                        exp0 = ProcessListStatement(param, ei);
                    }
                    else exp0 = CreateEqualExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp22 == null)
                    {                        
                        exp22 = exp0;
                    }
                    else
                    {
                        exp11 = exp22;
                        exp22 = exp0;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.Contain)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    if (IsList(ei.PropertyName))
                    {
                        exp0 = ProcessListStatement(param, ei);
                    }
                    else exp0 = CreateContainsExpression<T>(param, ei.PropertyName, ei.Value.ToString());
                    if (exp22 == null)
                    {                        
                        exp22 = exp0;
                    }
                    else
                    {
                        exp11 = exp22;
                        exp22 = exp0;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.Between)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    if (ei.Value2 == null) continue;
                    if (IsList(ei.PropertyName))
                    {
                        exp0 = ProcessListStatement(param, ei);
                    }
                    else exp0 = CreateBetweenExpression<T>(param, ei.PropertyName, ei.Value, ei.Value2);
                    if (exp22 == null)
                    {                        
                        exp22 = exp0;
                    }
                    else
                    {
                        exp11 = exp22;
                        exp22 = exp0;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.GreaterThan)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    
                    if (IsList(ei.PropertyName))
                    {
                        exp0 = ProcessListStatement(param, ei);
                    }
                    else exp0 = CreateGreatThanExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp22 == null)
                    {                        
                        exp22 = exp0;
                    }
                    else
                    {
                        exp11 = exp22;
                        exp22 = exp0;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.GreaterThanOrEqual)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    
                    if (IsList(ei.PropertyName))
                    {
                        exp0 = ProcessListStatement(param, ei);
                    }
                    else exp0 = CreateGreatThanOrEqualExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp22 == null)
                    {                        
                        exp22 = exp0;
                    }
                    else
                    {
                        exp11 = exp22;
                        exp22 = exp0;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.LessThan)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    
                    if (IsList(ei.PropertyName))
                    {
                        exp0 = ProcessListStatement(param, ei);
                    }
                    else exp0 = CreateLessThanExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp22 == null)
                    {                        
                        exp22 = exp0;
                    }
                    else
                    {
                        exp11 = exp22;
                        exp22 = exp0;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.LessThanOrEqual)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    
                    if (IsList(ei.PropertyName))
                    {
                        exp0 = ProcessListStatement(param, ei);
                    }
                    else exp0 = CreateLessThanOrequalExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp22 == null)
                    {                        
                        exp22 = exp0;
                    }
                    else
                    {
                        exp11 = exp22;
                        exp22 = exp0;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.NotEqual)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    if (IsList(ei.PropertyName))
                    {
                        exp0 = ProcessListStatement(param, ei);
                    }
                    else exp0 = CreateNotEqualExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp22 == null)
                    {                        
                        exp22 = exp0;
                    }
                    else
                    {
                        exp11 = exp22;
                        exp22 = exp0;
                    }
                }

                if (lst.Count > 0)
                {
                    
                    List<LinqExpressionInfo> lst2 = lst.ToList();
                    lst2.Add(ei);
                    exp22 = GetExpression<T>(param, lst2);
                }
                exp22 = exp11 == null ? exp22 : Expression.OrElse(exp11, exp22);


            }
            Expression finalExpression = null;
            if (exp2 == null)
                finalExpression = exp22 != null ? exp22 : null;
            else
            {
                if (exp22 != null)
                    finalExpression = Expression.AndAlso(exp2, exp22);
                else
                    finalExpression = exp2;
            }
            var lambda = Expression.Lambda<Func<T, bool>>(finalExpression, param);
            
            return lambda;
        }

        private static Expression GetExpression<T>(ParameterExpression param,List<LinqExpressionInfo> list)
        {
           
            Expression exp1 = null;
            Expression exp2 = null;
            Expression exp = null;
            foreach (LinqExpressionInfo ei in list)
            {

                if (ei.Operator == LinqExpressionOparator.Equal)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    if (IsList(ei.PropertyName))
                    {
                        exp = ProcessListStatement(param, ei);
                    }
                    else exp = CreateEqualExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp2 == null)
                    {                        
                        exp2 = exp;
                    }
                    else
                    {
                        exp1 = exp2;
                        exp2 = exp;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.Contain)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    if (IsList(ei.PropertyName))
                    {
                        exp = ProcessListStatement(param, ei);
                    }
                    else exp = CreateContainsExpression<T>(param, ei.PropertyName, ei.Value.ToString());
                    if (exp2 == null)
                    {                        
                        exp2 = exp;
                    }
                    else
                    {
                        exp1 = exp2;
                        exp2 = exp;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.Between)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    if (ei.Value2 == null) continue;
                    if (IsList(ei.PropertyName))
                    {
                        exp = ProcessListStatement(param, ei);
                    }
                    else exp = CreateBetweenExpression<T>(param, ei.PropertyName, ei.Value, ei.Value2);
                    if (exp2 == null)
                    {                        
                        exp2 = exp;
                    }
                    else
                    {
                        exp1 = exp2;
                        exp2 = exp;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.GreaterThan)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    
                    if (IsList(ei.PropertyName))
                    {
                        exp = ProcessListStatement(param, ei);
                    }
                    else exp = CreateGreatThanExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp2 == null)
                    {                        
                        exp2 = exp;
                    }
                    else
                    {
                        exp1 = exp2;
                        exp2 = exp;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.GreaterThanOrEqual)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    
                    if (IsList(ei.PropertyName))
                    {
                        exp = ProcessListStatement(param, ei);
                    }
                    else exp = CreateGreatThanOrEqualExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp2 == null)
                    {                        
                        exp2 = exp;
                    }
                    else
                    {
                        exp1 = exp2;
                        exp2 = exp;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.LessThan)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    
                    if (IsList(ei.PropertyName))
                    {
                        exp = ProcessListStatement(param, ei);
                    }
                    else exp = CreateLessThanExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp2 == null)
                    {                        
                        exp2 = exp;
                    }
                    else
                    {
                        exp1 = exp2;
                        exp2 = exp;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.LessThanOrEqual)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;                    
                    if (IsList(ei.PropertyName))
                    {
                        exp = ProcessListStatement(param, ei);
                    }
                    else exp = CreateLessThanOrequalExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp2 == null)
                    {                        
                        exp2 = exp;
                    }
                    else
                    {
                        exp1 = exp2;
                        exp2 = exp;
                    }
                }
                else if (ei.Operator == LinqExpressionOparator.NotEqual)
                {
                    if (string.IsNullOrWhiteSpace(ei.PropertyName)) continue;
                    if (ei.Value == null) continue;
                    if (IsList(ei.PropertyName))
                    {
                        exp = ProcessListStatement(param, ei);
                    }
                    else exp = CreateNotEqualExpression<T>(param, ei.PropertyName, ei.Value);
                    if (exp2 == null)
                    {                        
                        exp2 = exp;
                    }
                    else
                    {
                        exp1 = exp2;
                        exp2 = exp;
                    }
                }

                exp2 = exp1 == null ? exp2 : Expression.AndAlso(exp1, exp2);
            }

            return exp2;
        }
        private static bool IsList(string propertyName)
        {
            return propertyName.Contains('[') && propertyName.Contains(']');
        }
        private static MethodCallExpression ProcessListStatement(ParameterExpression param, LinqExpressionInfo statement)
        {
            var basePropertyName = statement.PropertyName.Substring(0, statement.PropertyName.IndexOf('['));
            var propertyName = statement.PropertyName.Substring(statement.PropertyName.IndexOf('[') + 1).Replace("]", string.Empty);
            var type = param.Type.GetProperty(basePropertyName).PropertyType.GetGenericArguments()[0];
            ParameterExpression listItemParam = Expression.Parameter(type, "i");
            var lambda = Expression.Lambda(GetExpression(listItemParam, statement, propertyName), listItemParam);            
            Expression member = null;
            if (basePropertyName.Contains('.'))
            {
                member = param;
                foreach (var namePart in basePropertyName.Split('.'))
                {
                    member = Expression.Property(member, namePart);
                }
            }
            else member = Expression.Property(param, basePropertyName);
            var enumerableType = typeof(Enumerable);
            var anyInfo = enumerableType.GetMethods(BindingFlags.Static | BindingFlags.Public).First(m => m.Name == "Any" && m.GetParameters().Length == 2);
            anyInfo = anyInfo.MakeGenericMethod(type);
            return Expression.Call(anyInfo, member, lambda);
        }
        private static Expression GetExpression(ParameterExpression param, LinqExpressionInfo statement, string propertyName = null)
        {
            
            var memberName = propertyName ?? statement.PropertyName;
            
            Expression member = null;
            if (memberName.Contains('.'))
            {
                member = param;
                foreach (var namePart in memberName.Split('.'))
                {
                    member = Expression.Property(member, namePart);
                }
            }
            else member = Expression.Property(param, memberName);

            
            var constant1 = Expression.Constant(statement.Value);
            var constant2 = Expression.Constant(statement.Value2);

            Expression body = null;
            if (statement.Operator == LinqExpressionOparator.Equal)
                body = Expression.Equal(member, constant1);
            else if(statement.Operator== LinqExpressionOparator.Contain)
                body = Expression.Call(member, "Contains", Type.EmptyTypes, constant1);
            else if(statement.Operator==  LinqExpressionOparator.Between)
            {
                BinaryExpression expression1 = null;
                BinaryExpression expression2 = null;
                if (statement.Value.GetType() == typeof(string))
                {
                    Expression<Func<string>> idLambda = () => statement.Value.ToString();
                    var CallMethod = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });
                    Expression callExpr = Expression.Call(member, CallMethod, idLambda.Body);
                    expression1 = Expression.GreaterThanOrEqual(callExpr, Expression.Constant(0));
                    
                }
                else
                    expression1 = Expression.GreaterThanOrEqual(member, constant1);

                if (statement.Value2.GetType() == typeof(string))
                {
                    Expression<Func<string>> idLambda = () => statement.Value2.ToString();
                    var CallMethod = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });
                    Expression callExpr = Expression.Call(member, CallMethod, idLambda.Body);
                    expression1 = Expression.LessThanOrEqual(callExpr, Expression.Constant(0));

                }
                else
                    expression2 = Expression.LessThanOrEqual(member, constant2);

                 body = Expression.AndAlso(expression1, expression2);
            }
            else if(statement.Operator== LinqExpressionOparator.In)
            {                
                var propertyType = ((PropertyInfo)(member as MemberExpression).Member).PropertyType;                
                body = Expression.Call(typeof(Enumerable), "Contains", new[] { propertyType }, constant1, member);
            }
            else if(statement.Operator== LinqExpressionOparator.GreaterThan)
            {
                if (statement.Value.GetType() == typeof(string))
                {
                    Expression<Func<string>> idLambda = () => statement.Value.ToString();
                    var CallMethod = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });
                    Expression callExpr = Expression.Call(member, CallMethod, idLambda.Body);
                    Expression searchExpr = Expression.GreaterThan(callExpr, Expression.Constant(0));
                    body = searchExpr;
                }
                else
                {
                    var expression1 = Expression.GreaterThan(member, constant1);
                    body = expression1;
                }
            }
            else if (statement.Operator == LinqExpressionOparator.GreaterThanOrEqual)
            {
                if (statement.Value.GetType() == typeof(string))
                {
                    Expression<Func<string>> idLambda = () => statement.Value.ToString();
                    var CallMethod = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });
                    Expression callExpr = Expression.Call(member, CallMethod, idLambda.Body);
                    Expression searchExpr = Expression.GreaterThanOrEqual(callExpr, Expression.Constant(0));
                    body = searchExpr;
                }
                else
                {
                    var expression1 = Expression.GreaterThanOrEqual(member, constant1);
                    body = expression1;
                }
            }
            else if (statement.Operator == LinqExpressionOparator.LessThan)
            {
                if (statement.Value.GetType() == typeof(string))
                {
                    Expression<Func<string>> idLambda = () => statement.Value.ToString();
                    var CallMethod = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });
                    Expression callExpr = Expression.Call(member, CallMethod, idLambda.Body);
                    Expression searchExpr = Expression.LessThan(callExpr, Expression.Constant(0));
                    body = searchExpr;
                }
                else
                {
                    var expression1 = Expression.LessThan(member, constant1);
                    body = expression1;
                }
            }
            else if (statement.Operator == LinqExpressionOparator.LessThanOrEqual)
            {
                if (statement.Value.GetType() == typeof(string))
                {
                    Expression<Func<string>> idLambda = () => statement.Value.ToString();
                    var CallMethod = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });
                    Expression callExpr = Expression.Call(member, CallMethod, idLambda.Body);
                    Expression searchExpr = Expression.GreaterThanOrEqual(callExpr, Expression.Constant(0));
                    body = searchExpr;
                }
                else
                {
                    var expression1 = Expression.LessThanOrEqual(member, constant1);
                    body = expression1;
                }
            }
            else if (statement.Operator == LinqExpressionOparator.NotEqual)
                body = Expression.NotEqual(member, constant1);
            return body;
        }
        private static BinaryExpression CreateEqualExpression<T>(ParameterExpression param, string propertyName, object value)
        {
            
            Expression member = null;
            if (propertyName.Contains('.'))
            {
                member = param;
                foreach (var namePart in propertyName.Split('.'))
                {
                    member = Expression.Property(member, namePart);
                }
            }
            else member = Expression.Property(param, propertyName);
            var constant = Expression.Constant(value);
            var body = Expression.Equal(member, constant);
            return body;
        }
        private static BinaryExpression CreateNotEqualExpression<T>(ParameterExpression param, string propertyName, object value)
        {
            
            Expression member = null;
            if (propertyName.Contains('.'))
            {
                member = param;
                foreach (var namePart in propertyName.Split('.'))
                {
                    member = Expression.Property(member, namePart);
                }
            }
            else member = Expression.Property(param, propertyName);
            var constant = Expression.Constant(value);
            var body = Expression.NotEqual(member, constant);
            return body;
        }
        private static Expression<Func<T, bool>> CreateEqualExpression<T>(IDictionary<string, object> filters)
        {
            var param = Expression.Parameter(typeof(T), "e");
            Expression body = null;
            foreach (var pair in filters)
            {
                Expression member = null;
                if (pair.Key.Contains('.'))
                {
                    member = param;
                    foreach (var namePart in pair.Key.Split('.'))
                    {
                        member = Expression.Property(member, namePart);
                    }
                }
                else member = Expression.Property(param, pair.Key);
                var constant = Expression.Constant(pair.Value);
                var expression = Expression.Equal(member, constant);
                body = body == null ? expression : Expression.AndAlso(body, expression);
            }
            return Expression.Lambda<Func<T, bool>>(body, param);
        }
        private static MethodCallExpression CreateContainsExpression<T>(ParameterExpression param,string propertyName, string value)
        {
            
            Expression member = null;
            if (propertyName.Contains('.'))
            {
                member = param;
                foreach (var namePart in propertyName.Split('.'))
                {
                    member = Expression.Property(member, namePart);
                }
            }
            else member = Expression.Property(param, propertyName);
            var constant = Expression.Constant(value);
            var body = Expression.Call(member, "Contains", Type.EmptyTypes, constant);
            return body;
        }        
        private static BinaryExpression CreateBetweenExpression<T>(ParameterExpression param, string propertyName, object firstValue, object secondValue)
        {
                       
            Expression member = null;
            BinaryExpression expression1 = null;
            BinaryExpression expression2 = null;
            if (propertyName.Contains('.'))
            {
                member = param;
                foreach (var namePart in propertyName.Split('.'))
                {
                    member = Expression.Property(member, namePart);
                }
            }
            else member = Expression.Property(param, propertyName);
            if (firstValue.GetType() == typeof(string))
            {
                Expression<Func<string>> idLambda = () => firstValue.ToString();
                var CallMethod = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });
                Expression callExpr = Expression.Call(member, CallMethod, idLambda.Body);
                expression1 = Expression.GreaterThanOrEqual(callExpr, Expression.Constant(0));

            }
            else
            {
                var constant1 = Expression.Constant(firstValue);
                expression1 = Expression.GreaterThanOrEqual(member, constant1);
            }
            if (secondValue.GetType() == typeof(string))
            {
                Expression<Func<string>> idLambda = () => secondValue.ToString();
                var CallMethod = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });
                Expression callExpr = Expression.Call(member, CallMethod, idLambda.Body);
                expression1 = Expression.LessThanOrEqual(callExpr, Expression.Constant(0));

            }
            else
            {
                var constant2 = Expression.Constant(secondValue);
                 expression2 = Expression.LessThanOrEqual(member, constant2);
            }
            var body = Expression.AndAlso(expression1, expression2);
            return body;
        }
        private static Expression CreateGreatThanExpression<T>(ParameterExpression param, string propertyName, object Value)
        {
            Expression member = null;
            if (propertyName.Contains('.'))
            {
                member = param;
                foreach (var namePart in propertyName.Split('.'))
                {
                    member = Expression.Property(member, namePart);
                }
            }
            else member = Expression.Property(param, propertyName);

            if (Value.GetType() == typeof(string))
            {
                Expression<Func<string>> idLambda = () => Value.ToString();
                var CallMethod = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });
                Expression callExpr = Expression.Call(member, CallMethod, idLambda.Body);
                Expression searchExpr = Expression.GreaterThan(callExpr, Expression.Constant(0));
                return searchExpr;
            }
            else
            {
                var constant1 = Expression.Constant(Value);
                var expression1 = Expression.GreaterThan(member, constant1);
                var body = expression1;
                return body;
            }
        }
        private static Expression CreateGreatThanOrEqualExpression<T>(ParameterExpression param, string propertyName, object Value)
        {
            Expression member = null;
            if (propertyName.Contains('.'))
            {
                member = param;
                foreach (var namePart in propertyName.Split('.'))
                {
                    member = Expression.Property(member, namePart);
                }
            }
            else member = Expression.Property(param, propertyName);
            if (Value.GetType() == typeof(string))
            {
                Expression<Func<string>> idLambda = () => Value.ToString();
                var CallMethod = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });
                Expression callExpr = Expression.Call(member, CallMethod, idLambda.Body);
                Expression searchExpr = Expression.GreaterThanOrEqual(callExpr, Expression.Constant(0));
                return searchExpr;
            }
            else
            {
                var constant1 = Expression.Constant(Value);
                var expression1 = Expression.GreaterThanOrEqual(member, constant1);
                var body = expression1;
                return body;
            }
        }
        private static Expression CreateLessThanExpression<T>(ParameterExpression param, string propertyName, object Value)
        {
            Expression member = null;
            if (propertyName.Contains('.'))
            {
                member = param;
                foreach (var namePart in propertyName.Split('.'))
                {
                    member = Expression.Property(member, namePart);
                }
            }
            else member = Expression.Property(param, propertyName);

            if (Value.GetType() == typeof(string))
            {
                Expression<Func<string>> idLambda = () => Value.ToString();
                var CallMethod = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });
                Expression callExpr = Expression.Call(member, CallMethod, idLambda.Body);
                Expression searchExpr = Expression.LessThan(callExpr, Expression.Constant(0));
                return searchExpr;
            }
            else
            {
                var constant1 = Expression.Constant(Value);
                var expression1 = Expression.LessThan(member, constant1);
                var body = expression1;
                return body;
            }
        }
        private static Expression CreateLessThanOrequalExpression<T>(ParameterExpression param, string propertyName, object Value)
        {
            Expression member = null;
            if (propertyName.Contains('.'))
            {
                member = param;
                foreach (var namePart in propertyName.Split('.'))
                {
                    member = Expression.Property(member, namePart);
                }
            }
            else member = Expression.Property(param, propertyName);
            if (Value.GetType() == typeof(string))
            {
                Expression<Func<string>> idLambda = () => Value.ToString();
                var CallMethod = typeof(string).GetMethod("CompareTo", new[] { typeof(string) });
                Expression callExpr = Expression.Call(member, CallMethod, idLambda.Body);
                Expression searchExpr = Expression.LessThanOrEqual(callExpr, Expression.Constant(0));
                return searchExpr;
            }
            else
            {
                var constant1 = Expression.Constant(Value);
                var expression1 = Expression.LessThanOrEqual(member, constant1);
                var body = expression1;
                return body;
            }
        }
        public static Expression<Func<T, object>> GetOrderByExpression<T>(string property)
        {
            return ApplyOrder<T>(property);
        }

        static Expression<Func<T, object>> ApplyOrder<T>(string property)
        {

            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            if (property.Contains('.'))
            {
                string[] props = property.Split('.');
                foreach (string prop in props)
                {
                    PropertyInfo pi = type.GetProperty(prop);
                    expr = Expression.Property(expr, pi);
                    type = pi.PropertyType;
                }
            }
            else
            {
                PropertyInfo pi = type.GetProperty(property);
                expr = Expression.Property(expr, pi);
            }
            var lambda = Expression.Lambda<Func<T, object>>(expr, arg);
            return lambda;
        }

        private static MethodCallExpression CreateInExpression<T>(ParameterExpression param, string propertyName, object value)
        {            
            var member = Expression.Property(param, propertyName);
            var propertyType = ((PropertyInfo)member.Member).PropertyType;
            var constant = Expression.Constant(value);
            var body = Expression.Call(typeof(Enumerable), "Contains", new[] { propertyType }, constant, member);
            return body;
        }
    }
}

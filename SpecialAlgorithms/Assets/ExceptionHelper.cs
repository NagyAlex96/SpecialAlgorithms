using System.Runtime.CompilerServices;

namespace SpecialAlgorithms.Assets
{
    public enum Condition : byte
    {
        LessThan, LessOrEqualThan, GreaterThan, GreaterOrEqualThan, Equal, NotEqual
    }

    public sealed class ExceptionHelper
    {
        public static void ThrowIfArgumentOutOfRange(in int input, in int from, in int to, string message = "", [CallerArgumentExpression("argument")] string argumentExpression = default, [CallerFilePath] string callerPath = default, [CallerMemberName] string callerName = default)
        {
            if (input < from || input > to)
            {
                message = string.IsNullOrEmpty(message) ? $"Argument was outside of the range in {Path.GetFileNameWithoutExtension(callerPath)} {callerName}" : message;
                throw new ArgumentOutOfRangeException(argumentExpression, message);
            }
        }

        public static void ThrowIfArgumentOutOfRange(in int input, in int conditionValue, Condition condition, string message = "", [CallerArgumentExpression("argument")] string argumentExpression = default, [CallerFilePath] string callerPath = default, [CallerMemberName] string callerName = default)
        {
            message = string.IsNullOrEmpty(message) ? $"Argument was {condition} {conditionValue} in {Path.GetFileNameWithoutExtension(callerPath)} {callerName}" : message;
            switch (condition)
            {
                case Condition.LessThan:
                    if (input < conditionValue)
                        throw new ArgumentOutOfRangeException(argumentExpression, message);
                    break;
                case Condition.LessOrEqualThan:
                    if (input <= conditionValue)
                        throw new ArgumentOutOfRangeException(argumentExpression, message);
                    break;
                case Condition.GreaterThan:
                    if (input > conditionValue)
                        throw new ArgumentOutOfRangeException(argumentExpression, message);
                    break;
                case Condition.GreaterOrEqualThan:
                    if (input >= conditionValue)
                        throw new ArgumentOutOfRangeException(argumentExpression, message);
                    break;
                case Condition.Equal:
                    if (input == conditionValue)
                        throw new ArgumentOutOfRangeException(argumentExpression, message);
                    break;
                case Condition.NotEqual:
                    if (input != conditionValue)
                        throw new ArgumentOutOfRangeException(argumentExpression, message);
                    break;
            }
        }

        public static void ThrowIfArgumentIsNull<T>(T argument, string message = "", [CallerFilePath] string callerPath = default, [CallerMemberName] string callerName = default)
        {
            if (argument is null)
            {
                message = string.IsNullOrEmpty(message) ? $"Argument was null in {Path.GetFileNameWithoutExtension(callerPath)} {callerName}" : message;
                throw new InvalidOperationException(message);
            }
        }
    }
}

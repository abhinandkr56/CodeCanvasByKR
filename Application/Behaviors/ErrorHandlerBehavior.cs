using NServiceBus.Pipeline;

namespace Application.Behaviors;

public class ErrorHandlerBehavior : Behavior<IInvokeHandlerContext>
{
    public override async Task Invoke(IInvokeHandlerContext context, Func<Task> next)
    {
        try
        {
            await next().ConfigureAwait(false);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
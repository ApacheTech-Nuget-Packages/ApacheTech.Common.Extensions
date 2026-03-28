using System;
using System.Threading.Tasks;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace ApacheTech.Common.Extensions.DotNet;

/// <summary>
///     Extension methods to aid working with Tasks.
/// </summary>
public static class TaskExtensions
{
    /// <summary>
    ///     Executes an action when a task has been completed successfully.
    /// </summary>
    /// <param name="task">The task to await the completion of.</param>
    /// <param name="continuation">The action to perform after the task has completed.</param>
    /// <param name="continueOnCapturedContext"><c>true</c> to attempt to marshall the continuation back to the original context captured, otherwise <c>false</c>.</param>
    public static async Task OnCompletedSuccessfully(this Task task, Action continuation,
        bool continueOnCapturedContext = true)
    {
        await task.ConfigureAwait(continueOnCapturedContext);
        continuation();
    }

    /// <summary>
    ///     Executes an action when a task has been completed successfully.
    /// </summary>
    /// <param name="task">The task to await the completion of.</param>
    /// <param name="continuation">The function to perform after the task has completed.</param>
    /// <param name="continueOnCapturedContext"><c>true</c> to attempt to marshall the continuation back to the original context captured, otherwise <c>false</c>.</param>
    public static async Task OnCompletedSuccessfully(this Task task, Func<Task> continuation,
        bool continueOnCapturedContext = true)
    {
        await task.ConfigureAwait(continueOnCapturedContext);
        await continuation().ConfigureAwait(false);
    }

    /// <summary>
    ///     Executes an action when a task has been completed successfully.
    /// </summary>
    /// <param name="task">The task to await the completion of.</param>
    /// <param name="continuation">The action to perform after the task has completed.</param>
    /// <param name="continueOnCapturedContext"><c>true</c> to attempt to marshall the continuation back to the original context captured, otherwise <c>false</c>.</param>
    public static async Task OnCompletedSuccessfully<TResult>(
        this Task<TResult> task, Action<TResult> continuation,
        bool continueOnCapturedContext = true)
    {
        var result = await task.ConfigureAwait(continueOnCapturedContext);
        continuation(result);
    }

    /// <summary>
    ///     Executes an action when a task has been completed successfully.
    /// </summary>
    /// <param name="task">The task to await the completion of.</param>
    /// <param name="continuation">The function to perform after the task has completed.</param>
    /// <param name="continueOnCapturedContext"><c>true</c> to attempt to marshall the continuation back to the original context captured, otherwise <c>false</c>.</param>
    public static async Task OnCompletedSuccessfully<TResult>(
        this Task<TResult> task, Func<TResult, Task> continuation,
        bool continueOnCapturedContext = true)
    {
        var result = await task.ConfigureAwait(continueOnCapturedContext);
        await continuation(result).ConfigureAwait(false);
    }

    /// <summary>
    ///     Executes an action when a task has been completed successfully.
    /// </summary>
    /// <param name="task">The task to await the completion of.</param>
    /// <param name="continuation">The function to perform after the task has completed.</param>
    /// <param name="continueOnCapturedContext"><c>true</c> to attempt to marshall the continuation back to the original context captured, otherwise <c>false</c>.</param>
    public static async Task<TNewResult> OnCompletedSuccessfully<TResult, TNewResult>(
        this Task<TResult> task, Func<TResult, TNewResult> continuation,
        bool continueOnCapturedContext = true)
    {
        var result = await task.ConfigureAwait(continueOnCapturedContext);
        return continuation(result);
    }

    /// <summary>
    ///     Executes an action when a task has been completed successfully.
    /// </summary>
    /// <param name="task">The task to await the completion of.</param>
    /// <param name="continuation">The function to perform after the task has completed.</param>
    /// <param name="continueOnCapturedContext"><c>true</c> to attempt to marshall the continuation back to the original context captured, otherwise <c>false</c>.</param>
    public static async Task<TNewResult> OnCompletedSuccessfully<TResult, TNewResult>(
        this Task<TResult> task, Func<TResult, Task<TNewResult>> continuation,
        bool continueOnCapturedContext = true)
    {
        var result = await task.ConfigureAwait(continueOnCapturedContext);
        return await continuation(result).ConfigureAwait(false);
    }

    /// <summary>
    ///     Runs a <see cref="Task"/> synchronously, blocking the calling thread until completion.
    /// </summary>
    /// <param name="task">The <see cref="Task"/> to run.</param>
    /// <param name="exceptionHandler">An optional action to perform if an exception is thrown during the execution of the task.</param>
    public static void RunOnce(this Task task, Action<Exception>? exceptionHandler = null)
    {
        try
        {
            task.ConfigureAwait(false).GetAwaiter().GetResult();
        }
        catch (AggregateException ex)
        {
            if (exceptionHandler is null)
                throw ex.InnerException ?? ex;
            exceptionHandler(ex.InnerException ?? ex);
        }
    }

    /// <summary>
    ///     Runs a <see cref="ValueTask"/> synchronously, blocking the calling thread until completion.
    /// </summary>
    /// <param name="task">The <see cref="ValueTask"/> to run.</param>
    /// <param name="exceptionHandler">An optional action to perform if an exception is thrown during the execution of the task.</param>
    public static void RunOnce(this ValueTask task, Action<Exception>? exceptionHandler = null)
    {
        try
        {
            task.AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
        }
        catch (AggregateException ex)
        {
            if (exceptionHandler is null)
                throw ex.InnerException ?? ex;
            exceptionHandler(ex.InnerException ?? ex);
        }
    }

    /// <summary>
    ///     Runs a <see cref="Task{TResult}"/> synchronously, blocking the calling thread until completion.
    /// </summary>
    /// <param name="task">The <see cref="Task{TResult}"/> to run.</param>
    /// <param name="exceptionHandler">An optional action to perform if an exception is thrown during the execution of the task.</param>
    public static void RunOnce<TResult>(this Task<TResult> task, Action<Exception>? exceptionHandler = null)
    {
        try
        {
            task.ConfigureAwait(false).GetAwaiter().GetResult();
        }
        catch (AggregateException ex)
        {
            if (exceptionHandler is null)
                throw ex.InnerException ?? ex;
            exceptionHandler(ex.InnerException ?? ex);
        }
    }

    /// <summary>
    ///     Runs a <see cref="ValueTask{TResult}"/> synchronously, blocking the calling thread until completion.
    /// </summary>
    /// <param name="task">The <see cref="ValueTask{TResult}"/> to run.</param>
    /// <param name="exceptionHandler">An optional action to perform if an exception is thrown during the execution of the task.</param>
    public static void RunOnce<TResult>(this ValueTask<TResult> task, Action<Exception>? exceptionHandler = null)
    {
        try
        {
            task.AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
        }
        catch (AggregateException ex)
        {
            if (exceptionHandler is null)
                throw ex.InnerException ?? ex;
            exceptionHandler(ex.InnerException ?? ex);
        }
    }
}
﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Serilog;

namespace InterceptorLibrary;

public class DeleteConcurrencyInterceptor : ISaveChangesInterceptor
{
    public InterceptionResult ThrowingConcurrencyException(ConcurrencyExceptionEventData eventData, InterceptionResult result)
    {
        if (eventData.Entries.All(e => e.State == EntityState.Deleted))
        {
            Console.WriteLine(((RelationalConcurrencyExceptionEventData)eventData).Command.CommandText);
            Log.Error(eventData.Exception, $"Suppressing Concurrency violation for command\n" +
                                           $"{((RelationalConcurrencyExceptionEventData)eventData).Command.CommandText}\n");
            return InterceptionResult.Suppress();
        }

        return result;
    }

    public ValueTask<InterceptionResult> ThrowingConcurrencyExceptionAsync(ConcurrencyExceptionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = default)
        => new(ThrowingConcurrencyException(eventData, result));
}
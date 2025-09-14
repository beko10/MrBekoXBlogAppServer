using MrBekoXBlogAppServer.Application.Common.Results;
using System.Collections.Generic;

namespace MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;

public static class BusinessRuleEngine
{

    public static Result Run(params Result[] rules)
    {
        foreach(var rule in rules)
        {
            if (!rule.IsSuccess)
            {
                return rule;
            }
        }
        return Result.Success();
    }

    public static async Task<Result> RunAsync(params Func<Task<Result>>[] rules)
    {
        foreach (var rule in rules)
        {
            var result = await rule();
            if (!result.IsSuccess)
                return result;
        }
        return Result.Success();
    }

    public static async Task<Result> RunAsync(IEnumerable<Task<Result>> rules)
    {
        foreach (var rule in rules)
        {
            var result = await rule;
            if (!result.IsSuccess)
            {
                return result;
            }
        }
        return Result.Success();
    }
}

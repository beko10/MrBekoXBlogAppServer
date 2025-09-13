using FluentValidation;
using MediatR;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.Queries.GetByIdCategoryQuery;

namespace MrBekoXBlogAppServer.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Console.WriteLine($"validator count {_validators.Count()}");

        if (!_validators.Any())
        {
            Console.WriteLine("No validators found!");
            return await next();
        }

        // Request içeriğini kontrol et
        if (request is GetByIdCategoryQueryRequest categoryRequest)
        {
            Console.WriteLine($"📝 Request Id: '{categoryRequest.Id}'");
            Console.WriteLine($"📝 Id is null: {categoryRequest.Id == null}");
            Console.WriteLine($"📝 Id is empty: {string.IsNullOrEmpty(categoryRequest.Id)}");
            Console.WriteLine($"📝 Id is whitespace: {string.IsNullOrWhiteSpace(categoryRequest.Id)}");
        }

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        // Her validation result'unu kontrol et
        foreach (var result in validationResults)
        {
            Console.WriteLine($"🔍 Validation IsValid: {result.IsValid}");
            Console.WriteLine($"🔍 Errors Count: {result.Errors.Count}");

            foreach (var error in result.Errors)
            {
                Console.WriteLine($"❌ Error: {error.PropertyName} = '{error.ErrorMessage}'");
            }
        }

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        Console.WriteLine($"💥 Total failures: {failures.Count}");

        if (failures.Any())
        {
            Console.WriteLine("🚨 THROWING ValidationException!");
            throw new ValidationException(failures);
        }

        Console.WriteLine("✅ Validation passed - proceeding to next");
        return await next();
    }
}
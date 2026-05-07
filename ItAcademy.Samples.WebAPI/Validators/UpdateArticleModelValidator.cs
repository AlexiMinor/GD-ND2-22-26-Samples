using FluentValidation;
using ItAcademy.Samples.WebAPI.Models;

namespace ItAcademy.Samples.WebAPI.Validators;

public class UpdateArticleModelValidator : AbstractValidator<UpdateArticleModel>
{
    public UpdateArticleModelValidator()
    {
       RuleFor(uam => uam)
           .Must(uam => uam.Title is not null || uam.Rate.HasValue)
           .WithMessage("At least one of the Title or Rate must be provided.");

       When(uam => uam.Rate.HasValue, () =>
       {
           RuleFor(uam => uam.Rate)
               .InclusiveBetween(-10, 10)
               .WithMessage("Rate must be between -10 and 10.");
       });
    }
}
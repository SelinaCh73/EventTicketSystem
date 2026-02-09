using CarService.Models.Requests;
using FluentValidation;
using System;

namespace CarService.Host.Validators
{
    // Валидатор за добавяне на нов автомобил
    public class CarRequestValidator : AbstractValidator<AddCarRequest>
    {
        public CarRequestValidator()
        {
            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Моделът е задължителен.")
                .MinimumLength(2).WithMessage("Моделът трябва да е поне 2 символа.")
                .MaximumLength(50).WithMessage("Моделът не може да надвишава 50 символа.");

            RuleFor(x => x.Year)
                .InclusiveBetween(1886, DateTime.Now.Year + 1)
                .WithMessage($"Годината трябва да е между 1886 и {DateTime.Now.Year + 1}.");
        }
    }
}

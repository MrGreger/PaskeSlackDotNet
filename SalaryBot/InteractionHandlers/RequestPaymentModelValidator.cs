using FluentValidation;
using HttpSlackBot.Interactions.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalaryBot.InteractionHandlers
{
    public class RequestPaymentModelValidator : AbstractValidator<CreateRequestEvent>
    {
        public RequestPaymentModelValidator()
        {
            RuleFor(x => x.WorkTime.Value).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Введите часы работы").Must(BeNumeric).WithMessage("Часы работы должны быть числом");
            RuleFor(x => x.PaymentReason.Value).NotEmpty().WithMessage("Так что нужно оплатить-то?");
            RuleFor(x => x.ResultUrl.Value).NotEmpty().WithMessage("Добавьте ссылку на выполненную работу.");
            RuleFor(x => x.WherePay.Value).NotEmpty().WithMessage("Где оплатить?");
            RuleFor(x => x.WhoAccepted.SelectedUsers).NotEmpty().WithMessage("Кто принял работу?");
            RuleFor(x => x.PayUntil.SelectedDate).NotEmpty().WithMessage("Когда вам оплатить работу?");
        }

        private bool BeNumeric(string arg)
        {
            if(arg == null)
            {
                return false;
            }

            return arg.All(x => char.IsDigit(x) || x == ',' || x == '.');
        }
    }
}

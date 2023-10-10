using FluentValidation;
using projetofinalbloco2.Model;

namespace projetofinalbloco2.Validator
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(t => t.Tipo)
               .NotEmpty();
        }
    }
}

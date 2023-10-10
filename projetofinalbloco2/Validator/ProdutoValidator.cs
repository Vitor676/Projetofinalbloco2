using FluentValidation;
using projetofinalbloco2.Model;

namespace projetofinalbloco2.Validator
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(p => p.Nome)
                  .NotEmpty();

            RuleFor(p => p.Descricao)
                .NotEmpty();

            RuleFor(p => p.Preco)
               .NotNull()
               .GreaterThan(0)
               .PrecisionScale(20, 2, false);
        }
    }
}

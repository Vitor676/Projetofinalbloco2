﻿using projetofinalbloco2.Model;

namespace projetofinalbloco2.Service
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GettAll();

        Task<Produto?> GetById(long id);

        Task<IEnumerable<Produto>> GetByNome(string nome);

        Task<Produto?> Create(Produto produto);

        Task<Produto?> Update(Produto produto);

        Task Delete(Produto produto);
    }
}

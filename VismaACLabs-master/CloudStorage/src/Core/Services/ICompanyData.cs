using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface ICompanyData
    {
        IEnumerable<Company> GetAll(string query);
        Company Get(Guid id);
        Company Add(Company company);
        void Commit();
    }
}
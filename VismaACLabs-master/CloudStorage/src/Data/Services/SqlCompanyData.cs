using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Services;
using System.Linq;

namespace Data.Services
{
    public class SqlCompanyData : ICompanyData
    {
        private readonly CloudStorageDbContext _context;

        public SqlCompanyData(CloudStorageDbContext context)
        {
            _context = context;
        }

        public Company Add(Company company)
        {
            _context.Companies.Add(company);
            return company;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public Company Get(Guid id)
        {
            return _context.Companies.Find(id);
        }

        public IEnumerable<Company> GetAll(string query=null)
        {
            var companies = _context.Companies.OrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(query))
            {
                var companiesS = companies.Where(x => x.Name.Contains(query));
                return companiesS;
            }

            return companies;
        }
    }
}
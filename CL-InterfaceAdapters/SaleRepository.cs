﻿using CL_ApplicationLayer;
using CL_EnterpriseLayer;
using CL_InterfaceAdapters_Data;
using CL_InterfaceAdapters_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CL_InterfaceAdapters
{
    public class SaleRepository : IRepository<Sale>, IRepositorySearch<SaleModel, Sale>
    {
        private readonly AppDbContext _dbContext;

        public SaleRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Sale sale)
        {
            var saleModel = new SaleModel();
            saleModel.Total = sale.Total;
            saleModel.CreationDate = sale.Date;
            saleModel.Concepts = sale.Concepts.Select(c => new ConceptModel
            {
                UnitPrice = c.UnitPrice,
                IdBeer = c.IdBeer,
                Quantity = c.Quantity
            }).ToList();

            await _dbContext.Sales.AddAsync(saleModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        => await _dbContext.Sales
                .Select(s => new Sale(s.Id, s.CreationDate,
                                      _dbContext.Concepts
                                           .Where(c => c.IdSale == s.Id)
                                           .Select(c => new Concept(c.Quantity, c.IdBeer, c.UnitPrice))
                                           .ToList()
                                      )
                        )
                .ToListAsync();


        public async Task<Sale> GetByIdAsync(int id)
        {
            var saleModel = await _dbContext.Sales.FindAsync(id);
            return new Sale(saleModel.Id, saleModel.CreationDate,
                                      _dbContext.Concepts
                                           .Where(c => c.IdSale == saleModel.Id)
                                           .Select(c => new Concept(c.Quantity, c.IdBeer, c.UnitPrice))
                                           .ToList()
                            );
        }


        public async Task<IEnumerable<Sale>> GetAsync(Expression<Func<SaleModel, bool>> predicate)
        {
            var salesModel = await _dbContext.Sales.Include("Concepts").Where(predicate).ToListAsync();

            var sales = new List<Sale>();
          

            foreach (var saleModel in salesModel)
            {
                var concepts = new List<Concept>();
                foreach (var conceptModel in saleModel.Concepts)
                {
                    var concept = new Concept(conceptModel.Quantity, conceptModel.IdBeer, conceptModel.UnitPrice);
                    concepts.Add(concept);
                }
                var sale = new Sale(saleModel.Id, saleModel.CreationDate, concepts);
                sales.Add(sale);
            }

            return sales;
        }
     

    }
}

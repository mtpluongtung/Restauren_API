using Business;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.Constans;
using Models.DTO.Request.Order;
using Models.DTO.Response;
using Models.Entities;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class OrderRepository : IOrderSevices
    {
        private readonly RestaurentContext _context;
        public OrderRepository(RestaurentContext context)
        {
            _context = context;
        }
        public async Task<OrderResponse> Create(CreateOrder createOrder)
        {
            try
            {
                var order = createOrder.Adapt<Order>();
                var ban = await _context.Bans.FindAsync(createOrder.BanId);
                if (ban != null)
                {
                    ban.TrangThai = true;
                }

                await _context.Order.AddAsync(order);
                await _context.SaveChangesAsync();
                return order.Adapt<OrderResponse>();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return null;
            }
        }

        public async Task<List<MenuResponse>> GetMenu()
        {
            var result = new List<MenuResponse>();
            var monAn = await _context.MonAns.Select(x => new MenuResponse
            {
                Id = x.Id,
                Name = x.Name,
                Url = x.Url,
                Gia = x.Gia,
                Type = TypeMenu.MON_AN
            }
            ).ToListAsync();
            var set = await _context.Sets.Select(x => new MenuResponse
            {
                Id = x.Id,
                Name = x.Name,
                Url = x.Url,
                Gia = x.Gia,
                Type = TypeMenu.BUFFE
            }).ToListAsync();
            result.AddRange(monAn);
            result.AddRange(set);
            return result.OrderByDescending(x => x.Type).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BotFramework;
using BotFramework.Attributes;
using BotFramework.Setup;
using BotFramework.Utils;
using BotSeller_Telegram.Database;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace BotSeller_Telegram.Commands
{
    public class HandlersCmds : BotEventHandler
    {
        protected readonly Context _dbContext;

        protected bool isProductNameWrite = false;
        protected bool isProductDescWrite = false;
        protected bool isProducrPriceWrite = false;
        
        protected string productName;
        protected string productDesc;
        protected string price;

        public HandlersCmds(Context dbContext)
        {
            _dbContext = dbContext;
        }

        [Command(InChat.All, "products", CommandParseMode.Both)]
        public async Task showProducts()
        {
            var message = HtmlString.Empty;
            var productList = await _dbContext.ProductProps.Where(x => x.Owner.Id == From.Id).OrderBy(x => x.Id).ToArrayAsync();
             foreach (var product in productList)
             {
                 message.Code($"{product.Id}. {product.Name}\n\n{product.Desc}\n\nPrice: {product.Price}\nAuthor @{product.Owner.Username}");
             }

             await Bot.SendHtmlStringAsync(Chat, message);
        }

        [Command(InChat.All, "addProduct", CommandParseMode.Both)]
        public async Task addProduct()
        {
            await Bot.SendTextMessageAsync(Chat, "Введите название Вашего продукта: ");
            isProductNameWrite = true;
        }

        [TextMessage(InChat.All)]
        public async Task getProductName()
        {
            if (isProductNameWrite)
            {
                productName = RawUpdate.Message.Text;
                isProductNameWrite = false;
                isProductDescWrite = true;
            }
            else if (isProductDescWrite)
            {
                productDesc = RawUpdate.Message.Text;
                isProductDescWrite = false;
                isProducrPriceWrite = true;
            }
            else if (isProducrPriceWrite)
            {
                price = RawUpdate.Message.Text;
                isProducrPriceWrite = false;

                await ProcessCreateProduct();
            }
        }

        async Task ProcessCreateProduct()
        {
            if (await _dbContext.UserProps.AnyAsync(x => x.Id == From.Id))
            {
                _dbContext.ProductProps.Add(new ProductProps()
                {
                    Desc = productDesc, Name = productName, Owner = await _dbContext.UserProps.FirstOrDefaultAsync(x=>x.Id == From.Id),
                    Price = price
                });
                //_dbContext.UserProps.Find()
            }


            await _dbContext.SaveChangesAsync();
        }
    }
}

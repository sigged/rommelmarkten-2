﻿using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Features.NewsArticles.Domain;

namespace Rommelmarkten.Api.Features.NewsArticles.Application.Gateways
{
    public interface INewsArticlesDbContext : IApplicationDbContextBase
    {

        DbSet<NewsArticle> NewsArticles { get; set; }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using SK.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SK.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<AppUser> Users { get; set; }
        DbSet<Article> Articles { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<UserEvent> UserEvents { get; set; }
        DbSet<Discussion> Discussions { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<Photo> Photos { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

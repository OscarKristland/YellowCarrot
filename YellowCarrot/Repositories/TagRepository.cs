using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YellowCarrot.Data;
using YellowCarrot.Interface;
using YellowCarrot.Models;

namespace YellowCarrot.Repositories;

public class TagRepository : Repository<Tag>, ITagRepository
{
    private readonly AppDbContext _context;

    public TagRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}

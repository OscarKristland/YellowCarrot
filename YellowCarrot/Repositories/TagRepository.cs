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

public class TagRepository: ITagRepository
{
    private readonly AppDbContext _context;

    public TagRepository(AppDbContext context)
	{
		_context = context;
	}

    //Få tag på alla taggs
    public List<Tag> GetTags()
	{
		return _context.Tags.ToList();
	}

    //Lägg till en tagg
    public void AddTag(Tag tagToAdd)
	{
		_context.Tags.Add(tagToAdd);
	}

    //Ta bort en tagg
    public void RemoveTag(Tag tagToRemove)
	{
		_context.Tags.Remove(tagToRemove);
	}

	//Uppdatera en tagg
	public void UpdateTag(Tag tagToUpdate)
	{
		_context.Tags.Update(tagToUpdate);
	}


}

using JuleBeer.DB.Context;
using JuleBeer.DB.Entities;
using JuleBeer.Dto.Beer;
using JuleBeer.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JuleBeer.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BeerController : ControllerBase
{
    private readonly IDbContextFactory<JuleBeerContext> _dbContextFactory;

    public BeerController(IDbContextFactory<JuleBeerContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    [HttpGet]
    [Route(nameof(GetBeers))]
    public async Task<IActionResult> GetBeers(CancellationToken cancellationToken = default)
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var currentUser = await Authentication.GetCurrentUser(HttpContext, ctx, cancellationToken);

        var beers = await ctx.Beers.ToListAsync(cancellationToken);
        var myBeerReviews = await ctx.BeerReviews
            .Where(x => x.UserId == currentUser.Id)
            .ToListAsync(cancellationToken);


        var list = new List<BeerDto>();
        foreach(var beer in beers)
        {
            var stars = myBeerReviews
                .Where(x => x.BeerId == beer.Id)
                .Select(x => x.Stars)
                .FirstOrDefault();

            var b = new BeerDto
            {
                Id = beer.Id,
                Name = beer.Name,
                Description = beer.Description,
                ABV = beer.ABV,
                ImageUrl = beer.ImageUrl,
                MyStars = stars
            };

            list.Add(b);
        }

        return Ok(list);
    }

    [HttpGet]
    [Route(nameof(GetBeerById))]
    public async Task<IActionResult> GetBeerById(int id, CancellationToken cancellationToken = default)
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var currentUser = await Authentication.GetCurrentUser(HttpContext, ctx, cancellationToken);

        var beer = await ctx.Beers.FindAsync(id);
        if(beer == null)
        {
            return NotFound();
        }

        var myStars = await ctx.BeerReviews
            .Where(x => x.UserId == currentUser.Id)
            .Where(x => x.BeerId == beer.Id)
            .Select(x => x.Stars)
            .FirstOrDefaultAsync(cancellationToken);

        var beerDto = new BeerDto
        {
            Id = beer.Id,
            Name = beer.Name,
            Description = beer.Description,
            ABV = beer.ABV,
            ImageUrl = beer.ImageUrl,
            MyStars = myStars
        };

        return Ok(beerDto);
    }

    [HttpPost]
    [Route(nameof(GiveStars))]
    public async Task<IActionResult> GiveStars(ReviewDto dto, CancellationToken cancellationToken = default)
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var currentUser = await Authentication.GetCurrentUser(HttpContext, ctx, cancellationToken);

        var br = await ctx.BeerReviews
            .Where(x => x.UserId == currentUser.Id)
            .Where(x => x.BeerId == dto.BeerId)
            .FirstOrDefaultAsync(cancellationToken);

        // ef ekki til þá búa til annars update
        if(br == null)
        {
            br = new BeerReview();
            br.UserId = currentUser.Id;
            await ctx.BeerReviews.AddAsync(br, cancellationToken);
        }

        br.BeerId = dto.BeerId;
        br.Stars = dto.Stars;

        await ctx.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpGet]
    [Route(nameof(GetBeerResult))]
    public async Task<IActionResult> GetBeerResult(CancellationToken cancellationToken = default)
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var currentUser = await Authentication.GetCurrentUser(HttpContext, ctx, cancellationToken);

        var result = new ResultDto();

        var beers = await ctx.Beers.ToListAsync(cancellationToken);
        var allBeerReviews = await ctx.BeerReviews
            .ToListAsync(cancellationToken);

        var resultList = new List<BeerResultDto>();
        foreach (var beer in beers)
        {
            var totalStars = allBeerReviews
                .Where(x => x.BeerId == beer.Id)
                .Select(x => x.Stars)
                .Sum();

            var numberOfRatings = allBeerReviews
                .Where(x => x.BeerId == beer.Id)
                .Count();

            double rating = 0;
            if(numberOfRatings > 0)
            {
                rating = (double)totalStars / (double)numberOfRatings;
            }

            var b = new BeerResultDto
            {
                Id = beer.Id,
                Name = beer.Name,
                ImageUrl = beer.ImageUrl,
                AverageStars = Math.Round(rating, 1),
            };

            resultList.Add(b);
        }

        result.BeerResults = resultList.OrderByDescending(x => x.AverageStars).ToList();
        result.NumberOfUsers = await ctx.Users.CountAsync();
        result.NumberOfRatings = await ctx.BeerReviews.CountAsync();

        return Ok(result);
    }
}

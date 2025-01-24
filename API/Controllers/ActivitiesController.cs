
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistence;

namespace API.Controllers
{
    public class ActivitiesController :BaseAPIController
    {
        private readonly DataContext _context;
        
        public ActivitiesController(DataContext context) 
        {
            _context = context;
           
   
        }
      
      [HttpGet] //API/ACTIVITIES
      public async Task<ActionResult<List<Activity>>> GetActivities ()
      {
        return await _context.Activities.ToListAsync();
      }

      [HttpGet("{id}")] //API/ACTIVITIES
      public async Task<ActionResult<Activity>> GetActivitiy (Guid id)
      {
        return await _context.Activities.FindAsync(id);
      }
    }
}
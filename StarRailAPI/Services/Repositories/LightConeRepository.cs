using Microsoft.EntityFrameworkCore;
using StarRailAPI.Data;
using StarRailAPI.Helpers;
using StarRailAPI.Models;
using Supabase;

namespace StarRailAPI.Service.Repositories
{
    public class LightConeRepository : ILightConeRepository
    {
        private readonly StarRailContext _context;
        private readonly Client _client;
        private readonly MyHelpers _myHelpers;

        public LightConeRepository(StarRailContext context, Supabase.Client client, MyHelpers myHelpers)
        {
            _context = context;
            _client = client;
            _myHelpers = myHelpers;
        }
        public async Task<string> Add(LightConeAdd model)
        {
            var lightCone = new LightCone { Name = "" };

            lightCone.Name = model.Name;

            if (model.Image != null)
            {
                var uploadImage = await _myHelpers.UploadToSupabaseStorage(model.Image!, _client, "lightcone-images");
                lightCone.Logo = uploadImage.Value;
            }

            lightCone.Description = model.Description;

            var destiny = await _context.Destinies.SingleOrDefaultAsync(e => e.Id == model.DestinyId);

            if (destiny == null)
            {
                return "Not Found";
            }

            lightCone.DestinyId = model.DestinyId;

            _context.LightCones.Add(lightCone);
            _context.SaveChanges();

            return "Add Successful";
        }

        public List<LightCone> Get()
        {
            var lightCones = _context.LightCones.ToList();

            return lightCones;
        }

        // public async Task<string> Remove(int id)
        // {


        //     return "Remove Successful";
        // }

        public Task<string> Update(LightConeUpdate model)
        {
            throw new NotImplementedException();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using StarRailAPI.Common;
using StarRailAPI.Data;
using StarRailAPI.Helpers;
using StarRailAPI.Models;
using StarRailAPI.Service.Repositories;
using Supabase;

namespace StarRailAPI.Service.Repositories
{
    public class DestinyRepository : IDestinyRepository
    {
        private readonly StarRailContext _context;
        private readonly MyHelpers _myHelpers;
        private readonly Client _client;

        public DestinyRepository(StarRailContext context, MyHelpers myHelpers, Supabase.Client client)
        {
            _context = context;
            _myHelpers = myHelpers;
            _client = client;
        }

        public async Task<Result> Add(DestinyAdd model)
        {
            var existingDestiny = await _context.Destinies.SingleOrDefaultAsync(e => e.Name.Equals(model.Name));

            if (existingDestiny != null)
            {
                return Result.Failure(Errors.AlreadyName(model.Name));
            }

            var newDestiny = new Destiny()
            {
                Name = model.Name,
            };

            if (model.Image != null)
            {
                var imageLink = await _myHelpers.UploadToSupabaseStorage(model.Image, _client, "destiny-images");

                if (imageLink.Status)
                {
                    newDestiny.Logo = imageLink.Value;
                }
            }

            _context.Destinies.Add(newDestiny);
            _context.SaveChanges();

            return Result.Success("Add destiny successful");
        }

        public List<Destiny> Get()
        {
            var destinies = _context.Destinies.ToList();

            return destinies;
        }

        public async Task<string> Remove(int id)
        {
            var destiny = await _context.Destinies.SingleOrDefaultAsync(d => d.Id == id);

            if (destiny == null)
            {
                return "Not Found";
            }

            var deleteFile = await _myHelpers.DeleteFileInSupabaseStorage(_client, destiny.Logo!, "destiny-images");

            _context.Remove(destiny);
            _context.SaveChanges();

            return "Remove successful";
        }

        public async Task<string> Update(DestinyUpdate model)
        {
            var destiny = await _context.Destinies.SingleOrDefaultAsync(d => d.Id == model.Id);

            if (destiny == null)
            {
                return "Not Found";
            }

            if (model.Image != null)
            {
                await _myHelpers.DeleteFileInSupabaseStorage(_client, destiny.Logo!, "destiny-images");

                var newImage = await _myHelpers.UploadToSupabaseStorage(model.Image, _client, "destiny-images");
                destiny.Logo = newImage.Value;
            }

            destiny.Name = model.Name;

            _context.Update(destiny);
            _context.SaveChanges();

            return "Update successful";
        }
    }
}
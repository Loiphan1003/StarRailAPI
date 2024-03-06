using StarRailAPI.Models;
using Supabase;

namespace StarRailAPI.Helpers
{
    public class MyHelpers
    {
        public async Task<HelperResult> UploadToSupabaseStorage(IFormFile file, Supabase.Client client, string folderName)
        {
            try
            {
                using var memoryStream = new MemoryStream();

                await file.CopyToAsync(memoryStream);

                var lastIndexOfDot = file.FileName.LastIndexOf('.');

                string extension = file.FileName.Substring(lastIndexOfDot + 1);

                var result = await client.Storage.From(folderName).Upload(
                    memoryStream.ToArray(),
                    $"{folderName}-{file.FileName}"
                );

                return new HelperResult
                {
                    Status = true,
                    Value = $"https://xapzyvlwsqyohzkukjqx.supabase.co/storage/v1/object/public/{result}"
                };

            }
            catch (Exception ex)
            {
                return new HelperResult
                {
                    Status = false,
                    Value = ex.ToString()
                };
            }
        }

        public async Task<HelperResult> DeleteFileInSupabaseStorage(Supabase.Client client, string imageLink, string folder)
        {
            try
            {
                if (imageLink == null)
                {
                    return new HelperResult
                    {
                        Status = false,
                        Value = "image link is null"
                    };
                }

                var image = imageLink.Split($"{folder}/")[1];

                await client.Storage.From(folder).Remove(
                    new List<string> { image }
                );

                return new HelperResult
                {
                    Status = true,
                };
            }
            catch (Exception ex)
            {
                return new HelperResult
                {
                    Status = false,
                    Value = ex.ToString()
                };
            }
        }
    }
}
using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancistoCloneWeb.Services
{
    public class DropboxFileService: IFileService
    {
        public object Download(object file)
        {
            throw new NotImplementedException();
        }

        public void Upload(IFormFile file)
        {
            Task task = AsyncUpload("/folder", file.FileName, "texto");
            task.Wait();
        }

        private async Task AsyncUpload(string folder, string file, string content)
        {
            var token = "sl.AkCUo3v9ZbxqJVM6gG0X0N9ubSEfvWKywlS3ZeIaWRo0EoiEsw1c1BQ6FNhiAiBlhyIVXG8w5pcbKvjvloTsADNzh74p0udb50ngO8ZIPGKncJDs487zJfIHdHZxiRTunfW-4APhGS8";

            using (var dbx = new DropboxClient(token))
            {
                var full = await dbx.Users.GetCurrentAccountAsync();
                Console.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);

                using (var mem = new MemoryStream(Encoding.UTF8.GetBytes(content)))
                {
                    var updated = await dbx.Files.UploadAsync(
                        folder + "/" + file,
                        WriteMode.Overwrite.Instance,
                        body: mem);
                    Console.WriteLine("Saved {0}/{1} rev {2}", folder, file, updated.Rev);
                }
            }
        }
    }
}

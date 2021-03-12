using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace PhotoSharingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("StorageAccount");
            string containerName = "photos";

            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);

            container.CreateIfNotExists();

            string fileName = "docs-and-friends-selfie-stick.png";

            for (int i = 0; i < 3; i++)
            {
                Random randomGenerator = new Random();
                int randomNumber = randomGenerator.Next(999);
                string blobName = String.Format("docs-and-friends-selfie-stick-{0}", randomNumber);
            
                BlobClient blobClient = container.GetBlobClient(blobName);
                blobClient.Upload(fileName, true);
            }

            var blobs = container.GetBlobs();
            foreach (var blob in blobs)
            {
                Console.WriteLine($"{blob.Name} --> Created On: {blob.Properties.CreatedOn:yyyy-MM-dd HH:mm:ss}  Size: {blob.Properties.ContentLength}");
            }
        }
    }
}

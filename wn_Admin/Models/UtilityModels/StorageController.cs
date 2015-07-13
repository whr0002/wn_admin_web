using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.UtilityModels
{
    public class StorageController
    {
        private CloudStorageAccount account;
        private CloudBlobClient client;
        private CloudBlobContainer container;

        public StorageController()
        {
            this.account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            this.client = account.CreateCloudBlobClient();
            this.container = client.GetContainerReference("unknown");
        }

        public void setContainer(string containerName)
        {
            this.container = this.client.GetContainerReference(containerName);
        }

        public string upload(HttpPostedFileBase file)
        {

            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });


            string uniqueName = string.Format(@"{0}", Guid.NewGuid());
            CloudBlockBlob blob = container.GetBlockBlobReference(uniqueName + Path.GetExtension(file.FileName));
            blob.UploadFromStream(file.InputStream);

            return blob.Uri.ToString();
        } 
    }
}
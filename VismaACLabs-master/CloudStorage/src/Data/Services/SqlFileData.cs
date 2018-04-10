using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Services;
using System.Linq;

namespace Data.Services
{
    public class SqlFileData : IFileData
    {
        private readonly CloudStorageDbContext _context;

        public SqlFileData(CloudStorageDbContext context)
        {
            _context = context;
        }

        public FileInfo Add(FileInfo file)
        {
            _context.Add(file);
            return file;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Delete(FileInfo fileInfo)
        {
            if (fileInfo != null)
                _context.FileInfos.Remove(fileInfo);
        }

        public FileInfo Get(Guid id)
        {
            return _context.FileInfos.Find(id);
        }

        public IEnumerable<FileInfo> Search(string term, string companyId = null)
        {
            var tmp = _context.FileInfos.Where(_ => _.FileName.ToLower().Contains(term)
            || _.Description.ToLower().Contains(term)); 
            return string.IsNullOrWhiteSpace(companyId)
                ? tmp
                : ((IEnumerable<FileInfo>) tmp).Where(
                    _ => companyId.Equals(_.ContainerName, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<FileInfo> GetAll(string companyId = null)
        {
            return string.IsNullOrWhiteSpace(companyId) ? _context.FileInfos :
                _context.FileInfos.Where(_ => companyId.Equals(_.ContainerName, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<FileInfo> FilterBy(string term=null, string companyId=null)
        {
            var myItem = _context.FileInfos.Where(_ => _.ContainerName.Equals(companyId));

            switch (term)
            {
                case "Ascending By Name": return myItem.OrderBy(x => x.FileName);
                case "Descending By Name": return myItem.OrderByDescending(x => x.FileName);
                case "Ascending By Size": return myItem.OrderBy(x => x.FileSizeInBytes);
                case "Descending By Size":
                    return myItem.OrderByDescending(x => x.FileSizeInBytes);
                case "Ascending By ContentType": return myItem.OrderBy(x => x.ContentType);
                case "Descending By ContentType":
                    return myItem.OrderByDescending(x => x.ContentType);
                default:
                    return null;
            }

        }

        public IEnumerable<FileInfo> GetOwnerFiles(string owner = null, string companyId=null)
        {
            return !string.IsNullOrEmpty(owner) ?  _context.FileInfos.Where(_ => _.Owner.Equals(owner)) : 
                _context.FileInfos.Where(_ => companyId.Equals(_.ContainerName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
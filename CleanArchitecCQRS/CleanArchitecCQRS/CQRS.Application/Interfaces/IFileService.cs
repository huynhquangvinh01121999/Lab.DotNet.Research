using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Settings;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces
{ 
    public interface IFileService
    {
        Task<UploadResponse> CreateFolder(string fullPath);
        Task<UploadResponse> RemoveFolder(string rootPath,string fullPath);
        Task<UploadResponse> RenameFolder(string rootPath,string oldname,string newname);
        Task<UploadResponse> UploadFile(IFormFile file, string maNhanVien, string tenThuMuc);
        Task<UploadResponse> RemoveFile(string fileName, string maNhanVien, string tenThuMuc);
        Task<string> GetAllFolders();
        Task<string> GetAllFiles();
        Task<string> GetFoldersById(Guid NhanVienId);
        Task<string> GetFilesById(Guid NhanVienId);
        string JsonToFileInfor(FileInfor fileInfo);
    }
}

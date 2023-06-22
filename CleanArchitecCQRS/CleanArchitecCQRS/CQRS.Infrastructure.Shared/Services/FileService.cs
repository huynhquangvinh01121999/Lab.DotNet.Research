using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Settings;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using File = System.IO.File;

namespace CQRS.Infrastructure.Shared.Services
{
    public class FileService : IFileService
    {
        private ApplicationDbContext _dbContext;
        public FileService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UploadResponse> CreateFolder(string fullPath)
        {
            try
            {
                var folderName = Path.Combine(FileDirection.Resources, fullPath);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                    return await Task.FromResult(new UploadResponse(fullPath, null));
                }
                else
                {
                    return await Task.FromResult(new UploadResponse(null, "Folder exists"));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new UploadResponse(null, "Error: " + ex.Message.ToString()));
            }
        }

        public async Task<UploadResponse> RemoveFolder(string rootFolder, string removeFolder)
        {
            try
            {
                //Get folder file
                var folderFile = Path.Combine(FileDirection.Resources, rootFolder);
                //Get url folder file 
                var pathToFolder = Path.Combine(Directory.GetCurrentDirectory(), folderFile, removeFolder);
                //Get folder to move file
                var folderRecycle = Path.Combine(FileDirection.Recycle, rootFolder);
                if (!Directory.Exists(folderRecycle))
                {
                    Directory.CreateDirectory(folderRecycle);
                }
                //Get url folder move file
                var pathToMove = Path.Combine(Directory.GetCurrentDirectory(), folderRecycle, removeFolder + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                //Move
                Directory.Move(pathToFolder, pathToMove);
                return await Task.FromResult(new UploadResponse(rootFolder + "/" + removeFolder, null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new UploadResponse(null, "Error: " + ex.Message.ToString()));
            }
        }

        public async Task<UploadResponse> RenameFolder(string rootPath, string oldName, string newName)
        {
            try
            {
                //Get folder file
                var folderFile = Path.Combine(FileDirection.Resources, rootPath);
                //Get url folder file 
                var pathToFolder = Path.Combine(Directory.GetCurrentDirectory(), folderFile, oldName);
                if (!Directory.Exists(pathToFolder))
                {
                    return await Task.FromResult(new UploadResponse(null, "Renamed Folder Not Found."));
                }
                //Get folder to move file
                var folderRecycle = Path.Combine(FileDirection.Resources, rootPath);
                //Get url folder move file
                var pathToMove = Path.Combine(Directory.GetCurrentDirectory(), folderRecycle, newName);
                //Move
                Directory.Move(pathToFolder, pathToMove);
                return await Task.FromResult(new UploadResponse(rootPath + newName, null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new UploadResponse(null, "Error: " + ex.Message.ToString()));
            }
        }

        public async Task<UploadResponse> UploadFile(IFormFile file, string maNhanVien, string tenThuMuc)
        {
            try
            {
                if (maNhanVien == null)
                {
                    return await Task.FromResult(new UploadResponse(null, "MaNhanVien cannot be null."));
                }
                if (tenThuMuc == null)
                {
                    return await Task.FromResult(new UploadResponse(null, "TenThuMuc cannot be null."));
                }
                var folderName = Path.Combine(FileDirection.Resources, maNhanVien, tenThuMuc);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    //var getSplit = fileName.Split(".");
                    //var typeFile = "."+getSplit[getSplit.Length - 1];
                    var fullnewfileName = string.Format("{0}_{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), fileName);
                    var fullPath = Path.Combine(pathToSave, fullnewfileName);
                    var dbPath = Path.Combine(folderName, fullnewfileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return await Task.FromResult(new UploadResponse(string.Format("/{0}/{1}/{2}", maNhanVien, tenThuMuc, fullnewfileName), null));
                }
                else
                {
                    return await Task.FromResult(new UploadResponse(null, "File not Found."));
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new UploadResponse(null, "Error: " + ex.Message.ToString()));
            }
        }

        public async Task<UploadResponse> RemoveFile(string fileName, string maNhanVien, string tenThuMuc)
        {
            try
            {
                if (fileName == null)
                {
                    return await Task.FromResult(new UploadResponse(null, "TenFile cannot be null."));
                }
                if (maNhanVien == null)
                {
                    return await Task.FromResult(new UploadResponse(null, "MaNhanVien cannot be null."));
                }
                if (tenThuMuc == null)
                {
                    return await Task.FromResult(new UploadResponse(null, "TenThuMuc cannot be null."));
                }
                //Get folder file
                var folderFile = Path.Combine(FileDirection.Resources, maNhanVien, tenThuMuc);
                //Get url folder file 
                var pathToFile = Path.Combine(Directory.GetCurrentDirectory(), folderFile);
                //Get full path file
                var pathFileMove = Path.Combine(pathToFile, fileName);
                //File Not Found!
                if (!File.Exists(pathFileMove))
                {
                    return await Task.FromResult(new UploadResponse("File not Found.", null));
                }
                //Get folder to move file
                var folderRecycle = Path.Combine(FileDirection.Recycle, maNhanVien);
                //Get url folder move file
                var pathToMove = Path.Combine(Directory.GetCurrentDirectory(), folderRecycle);
                //Create if folder not exists
                if (!Directory.Exists(pathToMove))
                {
                    Directory.CreateDirectory(pathToMove);
                }
                //Get full path move file
                var fullPathMove = Path.Combine(pathToMove, fileName);
                //Move
                File.Move(pathFileMove, fullPathMove);
                return await Task.FromResult(new UploadResponse(string.Format("/{0}/{1}/{2}", maNhanVien, tenThuMuc, fileName), null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new UploadResponse(null, "Error: " + ex.Message.ToString()));
            }
        }

        public async Task<string> GetAllFolders()
        {
            var folderName = Path.Combine(FileDirection.Resources);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var data = await SettingResult(new DirectoryInfo(pathToSave), null, true);

            string result = "[" + JsonToFileInfor(data) + "]";
            return await Task.FromResult(result);
        }

        public async Task<string> GetAllFiles()
        {
            var folderName = Path.Combine(FileDirection.Resources);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var data = await SettingResult(new DirectoryInfo(pathToSave), null, false);

            string result = "[" + JsonToFileInfor(data) + "]";
            return await Task.FromResult(result);
        }

        public async Task<string> GetFoldersById(Guid nhanVienId)
        {
            var maNhanVien = _dbContext.NhanViens.Where(nv => nv.Id == nhanVienId && nv.Deleted != true)
                                                 .Select(nv => nv.MaNhanVien)
                                                 .FirstOrDefault();
            if (maNhanVien == null)
            {
                throw new ApiException("NhanVien Folder Not Found");
            }
            var folderName = Path.Combine(FileDirection.Resources, maNhanVien);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var data = await SettingResult(new DirectoryInfo(pathToSave), null, true);

            string result = "[" + JsonToFileInfor(data) + "]";
            return await Task.FromResult(result);
        }

        public async Task<string> GetFilesById(Guid nhanVienId)
        {
            var maNhanVien = _dbContext.NhanViens.Where(nv => nv.Id == nhanVienId && nv.Deleted != true)
                                                 .Select(nv => nv.MaNhanVien)
                                                 .FirstOrDefault();
            if (maNhanVien == null)
            {
                throw new ApiException("NhanVien Folder Not Found");
            }
            var folderName = Path.Combine(FileDirection.Resources, maNhanVien);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var data = await SettingResult(new DirectoryInfo(pathToSave), null, false);

            string result = "[" + JsonToFileInfor(data) + "]";
            return await Task.FromResult(result);
        }


        private async Task<FileInfor> SettingResult(FileSystemInfo dirInfo, string strKey, bool IsfolderOnly)
        {
            var fsi = new FileInfor();

            fsi.title = dirInfo.Name;
            fsi.CreateDate = dirInfo.CreationTime;
            fsi.UpdateDate = dirInfo.LastWriteTime;
            fsi.children = new List<FileInfor>();
            fsi.key = strKey != null && strKey != "" ? strKey : "100";

            if (dirInfo.Attributes == FileAttributes.Directory)
            {
                fsi.isFolder = true;
                fsi.expanded = true;
                fsi.fileType = "folder";
                fsi.fileUrl = dirInfo.FullName.ToString();
                fsi.isLeaf = true;
                var lists = (dirInfo as DirectoryInfo).GetFileSystemInfos().OrderBy(nv => nv.Attributes).ThenBy(nv => nv.CreationTime).ToList();
                for (int i = 0; i < lists.Count; i++)
                {
                    string stt = string.Format("{0}{1}", fsi.key, i.ToString());
                    if (IsfolderOnly == true)
                    {
                        if (lists[i].Attributes == FileAttributes.Directory)
                        {
                            fsi.isLeaf = false;
                            fsi.children.Add(await SettingResult(lists[i], stt, IsfolderOnly));
                        }
                    }
                    else
                    {
                        fsi.isLeaf = false;
                        fsi.children.Add(await SettingResult(lists[i], stt, IsfolderOnly));
                    }
                }
            }
            else
            {
                fsi.isFolder = false;
                fsi.expanded = false;
                fsi.fileUrl = dirInfo.FullName.ToString().Replace(Path.Combine(Directory.GetCurrentDirectory(), FileDirection.Resources), "");
                //Image check
                string[] listCheck = { ".jpg", ".gif", ".jpeg", ".png", ".bmp", ".tiff" };
                bool isImage = listCheck.Any(s => dirInfo.Name.ToLower().Contains(s));
                if (isImage)
                {
                    fsi.fileType = "image";
                    fsi.icon = "s2-files-image";
                }
                else
                {
                    fsi.fileType = "other";
                    fsi.icon = "s2-files-other";
                    if (dirInfo.Name.Contains(".docx"))
                    {
                        fsi.fileType = "word";
                        fsi.icon = "s2-files-word";
                    }
                    if (dirInfo.Name.Contains(".xlsx"))
                    {
                        fsi.fileType = "excel";
                        fsi.icon = "s2-files-excel";
                    }
                    if (dirInfo.Name.Contains(".pdf"))
                    {
                        fsi.fileType = "pdf";
                        fsi.icon = "s2-files-pdf";
                    }
                }
            }
            return await Task.FromResult(fsi);
        }

        public string JsonToFileInfor(FileInfor fileInfo)
        {
            return JsonConvert.SerializeObject(fileInfo, Formatting.Indented).Replace(@"\", "/");
        }
    }
}

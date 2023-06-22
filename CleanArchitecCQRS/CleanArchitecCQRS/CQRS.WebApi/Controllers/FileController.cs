using System;
using System.Threading.Tasks;
using EsuhaiHRM.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EsuhaiHRM.WebApi.Parameters;
using Microsoft.AspNetCore.Authorization;
using EsuhaiHRM.WebApi.Models;
using EsuhaiHRM.Application.Interfaces;
using EsuhaiHRM.Application.Wrappers;

namespace EsuhaiHRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpGet("GetListFolder")]
        [Authorize(Roles = Role.DBNS_VIEW)]
        public async Task<IActionResult> GetListFolder()
        {
            try
            {
                var result = await _fileService.GetAllFolders();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error: {ex}");
            }
        }

        [HttpGet("GetListFile")]
        [Authorize(Roles = Role.DBNS_VIEW)]
        public async Task<IActionResult> GetListFile()
        {
            try
            {
                var result = await _fileService.GetAllFiles();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error: {ex}");
            }
        }

        [HttpGet("GetListFolderById")]
        [Authorize(Roles = Role.DBNS_VIEW)]
        public async Task<IActionResult> GetListFolderById(Guid NhanVienId)
        {
            try
            {
                var result = await _fileService.GetFoldersById(NhanVienId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error: {ex}");
            }
        }

        [HttpGet("GetListFileById")]
        [Authorize(Roles = Role.DBNS_VIEW)]
        public async Task<IActionResult> GetListFileById(Guid NhanVienId)
        {
            try
            {
                var result = await _fileService.GetFilesById(NhanVienId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error: {ex}");
            }
        }

        [HttpPost("Upload")]
        [Authorize(Roles = Role.DBNS_ADD)]
        public async Task<IActionResult> Upload([FromQuery] UploadParameter upload ,IFormFile file)
        {
            try
            {
                var uploadResult = await _fileService.UploadFile(file, maNhanVien: upload.MaNhanVien, tenThuMuc: upload.TenThuMuc);
                if (uploadResult.Succeeded == true)
                {
                    return Ok(uploadResult);
                }
                else
                {
                    throw new ApiException(uploadResult.Message);
                }
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error: {ex}");
            }
        }

        [HttpPost("Remove")]
        [Authorize(Roles = Role.DBNS_DELETE)]
        public async Task<IActionResult> Remove([FromQuery] UploadParameter upload)
        {
            try
            {
                var removeResult = await _fileService.RemoveFile(fileName: upload.TenFile, maNhanVien: upload.MaNhanVien, tenThuMuc: upload.TenThuMuc);
                if (removeResult.Succeeded == true)
                {
                    return Ok(removeResult);
                }
                else
                {
                    throw new ApiException(removeResult.Message);
                }
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error: {ex}");
            }
        }

        [HttpPost("CreateFolder")]
        [Authorize(Roles = Role.DBNS_ADD)]
        public async Task<IActionResult> CreateFolder(string fullPathToFolder)
        {
            //try
            //{
            //    var uploadResult = await _fileService.CreateFolder(fullPathToFolder);
            //    if (uploadResult.Succeeded == true)
            //    {
            //        return Ok(uploadResult);
            //    }
            //    else
            //    {
            //        throw new ApiException(uploadResult.Message);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new ApiException($"Error: {ex}");
            //}
            try
            {
                var uploadResult = await _fileService.CreateFolder(fullPathToFolder);
                return Ok(uploadResult);
            }
            catch (Exception ex)
            {
                return Ok(new UploadResponse(null, ex.Message.ToString()));
            }
        }

        [HttpPost("RemoveFolder")]
        [Authorize(Roles = Role.DBNS_DELETE)]
        public async Task<IActionResult> RemoveFolder(string folderParentPath,string folderName)
        {
            try
            {
                var removeResult = await _fileService.RemoveFolder(folderParentPath, folderName);
                if (removeResult.Succeeded == true)
                {
                    return Ok(removeResult);
                }
                else
                {
                    throw new ApiException(removeResult.Message);
                }
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error: {ex}");
            }
        }

        [HttpPost("RenameFolder")]
        [Authorize(Roles = Role.DBNS_DELETE)]
        public async Task<IActionResult> RenameFolder(string folderParentPath, string oldFolder, string newFolder)
        {
            try
            {
                var removeResult = await _fileService.RenameFolder(folderParentPath, oldFolder, newFolder);
                if (removeResult.Succeeded == true)
                {
                    return Ok(removeResult);
                }
                else
                {
                    throw new ApiException(removeResult.Message);
                }
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error: {ex}");
            }
        }
    }
}
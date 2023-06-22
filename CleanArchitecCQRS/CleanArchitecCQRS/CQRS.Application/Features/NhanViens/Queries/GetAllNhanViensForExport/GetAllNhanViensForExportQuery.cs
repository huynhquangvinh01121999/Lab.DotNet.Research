using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViens;
using System;

namespace EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViensForExport
{
    public class GetAllNhanViensForExportQuery : IRequest<PagedResponse<IEnumerable<GetAllNhanViensForExportViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortParam { get; set; }
        public string FilterParams { get; set; }
        public string SearchParam { get; set; }
    }
    public class GetAllNhanViensForExportQueryHandler : IRequestHandler<GetAllNhanViensForExportQuery, PagedResponse<IEnumerable<GetAllNhanViensForExportViewModel>>>
    {
        private readonly INhanVienRepositoryAsync _nhanvienRepository;
        private readonly IMapper _mapper;
        public GetAllNhanViensForExportQueryHandler(INhanVienRepositoryAsync nhanvienRepository, IMapper mapper)
        {
            _nhanvienRepository = nhanvienRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllNhanViensForExportViewModel>>> Handle(GetAllNhanViensForExportQuery request, CancellationToken cancellationToken)
        {
            var totalItem = 0;
            var validFilter = _mapper.Map<GetAllNhanViensParameter>(request);
            
            var nhanviens = await _nhanvienRepository.S2_GetPagedReponseAsync(validFilter.PageNumber
                                                                             ,validFilter.PageSize
                                                                             ,validFilter.SortParam
                                                                             ,validFilter.FilterParams
                                                                             ,validFilter.SearchParam);
            //get count all iteam after GET request

            totalItem = await _nhanvienRepository.GetToTalItem();

            var nhanvienViewModel = _mapper.Map<IEnumerable<GetAllNhanViensForExportViewModel>>(nhanviens);

            int i = 0;
            foreach (var item in nhanvienViewModel)
            {
                var list = "";
                for(int j = 0; j < nhanviens[i].NhanVien_GroupMails.Count; j++)
                {
                    if(list == "")
                    {
                        list = nhanviens[i].NhanVien_GroupMails[j].GroupMail.Ten;
                    }
                    else
                    {
                        list += "," + nhanviens[i].NhanVien_GroupMails[j].GroupMail.Ten;
                    }
                }
                item.ListGroupMails = list;
                i = i + 1;
            }
            return new PagedResponse<IEnumerable<GetAllNhanViensForExportViewModel>>(nhanvienViewModel, validFilter.PageNumber, validFilter.PageSize, totalItem);
        }
    }
}

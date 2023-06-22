using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViens;

namespace EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViensForPublic
{
    public class GetAllNhanViensForPublicQuery : IRequest<PagedResponse<IEnumerable<GetAllNhanViensForPublicViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortParam { get; set; }
        public string FilterParams { get; set; }
        public string SearchParam { get; set; }
    }
    public class GetAllNhanViensForPublicQueryHandler : IRequestHandler<GetAllNhanViensForPublicQuery, PagedResponse<IEnumerable<GetAllNhanViensForPublicViewModel>>>
    {
        private readonly INhanVienRepositoryAsync _nhanvienRepository;
        private readonly IMapper _mapper;
        public GetAllNhanViensForPublicQueryHandler(INhanVienRepositoryAsync nhanvienRepository, IMapper mapper)
        {
            _nhanvienRepository = nhanvienRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllNhanViensForPublicViewModel>>> Handle(GetAllNhanViensForPublicQuery request, CancellationToken cancellationToken)
        {
            var totalItem = 0;
            var validFilter = _mapper.Map<GetAllNhanViensParameter>(request);
            
            var nhanviens = await _nhanvienRepository.S2_GetPagedReponseAsyncForPublic(validFilter.PageNumber
                                                                                     , validFilter.PageSize
                                                                                     , validFilter.SortParam
                                                                                     , validFilter.FilterParams
                                                                                     , validFilter.SearchParam);
            //get count all iteam after GET request

            totalItem = await _nhanvienRepository.GetToTalItem();

            var nhanvienViewModel = _mapper.Map<IEnumerable<GetAllNhanViensForPublicViewModel>>(nhanviens);

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
                item.GroupMails = list;
                i = i + 1;
            }
            return new PagedResponse<IEnumerable<GetAllNhanViensForPublicViewModel>>(nhanvienViewModel, validFilter.PageNumber, validFilter.PageSize, totalItem);
        }
    }
}

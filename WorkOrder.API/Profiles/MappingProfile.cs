using WorkOrder.Domain.Contracts.Requests;
using WorkOrder.Domain.Contracts.Responses;
using WorkOrder.Domain.Models;
using AutoMapper;

namespace WorkOrder.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Model to Request

            CreateMap<UserRequest, UserModel>();
            CreateMap<CompanyRequest, CompanyModel>();
            CreateMap<FinalizationRequest, FinalizationModel>();
            CreateMap<WorkOrderRequest, WorkOrderModel>();
            CreateMap<WorkRequest, WorkModel>();
            CreateMap<ProfileRequest, ProfileModel>();

            #endregion

            #region Response to Model

            CreateMap<UserModel, UserResponse>();
            CreateMap<CompanyModel, CompanyResponse>();
            CreateMap<FinalizationModel, FinalizationResponse>();
            CreateMap<WorkOrderModel, WorkOrderResponse>();
            CreateMap<WorkModel, WorkResponse>();
            CreateMap<ProfileModel, ProfileResponse>();

            #endregion
        }
    }
}

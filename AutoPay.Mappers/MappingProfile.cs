using AutoMapper;
using AutoPay.Dtos.Batch;
using AutoPay.Dtos.Customer;
using AutoPay.Dtos.RemoteDbConfig;
using AutoPay.Utilities;
using AutoPay.ViewModels.Batch;
using AutoPay.ViewModels.Customer;
using AutoPay.ViewModels.RemoteDbConfig;
using System;

namespace AutoPay.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapRemoteDbConfigVm();

            MapBatchVm();
            MapBatchDetailVm();
            MapBatchListItemVm();
            MapBatchCustomerVm();
            MapBatchCustomerDetailVm();
            MapBatchCustomerDtoToAddCustomerVm();
            MapBatchCustomerDueDetailListItemVm();

            MapCustomerDetailVm();
            MapCustomerEditVm();
        }

        #region remoteDb config mapper

        private void MapRemoteDbConfigVm()
        {
            CreateMap<RemoteDbConfigDto, RemoteDbUpsertVm>();
        }

        #endregion

        #region batch mapper

        private void MapBatchVm()
        {
            CreateMap<BatchDto, BatchVm>();
        }

        private void MapBatchDetailVm()
        {
            CreateMap<BatchDetailDto, BatchDetailVm>();
        }

        private void MapBatchListItemVm()
        {
            CreateMap<BatchListItemDto, BatchListItemVm>();
        }

        private void MapBatchCustomerVm()
        {
            CreateMap<BatchCustomerDto, BatchCustomerVm>()
                .ForMember(x => x.AmountDue, source => source.MapFrom(y => Convert.ToDecimal(y.AmountDue)))
                .ForMember(x => x.IsExistsInLocalDb, source => source.MapFrom(y => Convert.ToBoolean(y.IsExistsInLocalDb)))
                .ForMember(x => x.PaymentStatus, source => source.MapFrom(y => Enum.Parse<PaymentStatus>(y.PaymentStatus)));
        }

        private void MapBatchCustomerDetailVm()
        {
            CreateMap<BatchCustomerDetailDto, BatchCustomerDetailVm>()
                .ForMember(x => x.Amount, source => source.MapFrom(y => Convert.ToDecimal(y.Amount)))
                .ForMember(x => x.PaymentStatus, source => source.MapFrom(y => Enum.Parse<PaymentStatus>(y.PaymentStatus)));
        }

        private void MapBatchCustomerDtoToAddCustomerVm()
        {
            CreateMap<BatchCustomerDto, AddCustomerVm>()
                .ForMember(x => x.Code, source => source.MapFrom(y => y.CustomerId))
                .ForMember(x => x.Name, source => source.MapFrom(y => y.CustomerName));
        }

        private void MapBatchCustomerDueDetailListItemVm()
        {
            CreateMap<BatchCustomerDueDetailListItemDto, BatchCustomerDueDetailListItemVm>()
                .ForMember(x => x.RecType, source => source.MapFrom(y => Convert.ToInt32(y.RecType)))
                .ForMember(x => x.AmountDue, source => source.MapFrom(y => Convert.ToDecimal(y.AmountDue)))
                .ForMember(x => x.Description, source => source.MapFrom(y => y.Description));
        }

        #endregion

        #region customer mapper

        private void MapCustomerEditVm()
        {
            CreateMap<CustomerDetailDto, EditCustomerVm>()
                .ForMember(x => x.CountryId, source => source.MapFrom(y => Convert.ToInt32(y.CountryId)))
                .ForMember(x => x.CardType, source => source.MapFrom(y => Convert.ToInt32(y.CardType)))
                .ForMember(x => x.ExpiryMonth, source => source.MapFrom(y => Convert.ToInt32(y.ExpiryMonth)))
                .ForMember(x => x.ExpiryYear, source => source.MapFrom(y => Convert.ToInt32(y.ExpiryYear)));
        }

        private void MapCustomerDetailVm()
        {
            CreateMap<CustomerDetailDto, CustomerDetailVm>()
                .ForMember(x => x.Country, source => source.MapFrom(y => y.CountryName));
        }

        #endregion

    }
}
